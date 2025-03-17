import { useCallback, useEffect, useState } from "react";
import { AuthContextType } from "../config/authContext";
import {
  AuthUserDto,
  LoginUserCommand,
  RegisterUserCommand,
  UserDetailsDto,
} from "@/shared/api/types";
import { useMutation } from "@tanstack/react-query";
import { login } from "../api/login";
import { refresh } from "../api/refresh";
import { signUp } from "../api/signUp";

export const useAuthProvieder = (): AuthContextType => {
  const [user, setUser] = useState<UserDetailsDto | undefined>(undefined);

  const [accessToken, setAccessTokenState] = useState(
    localStorage.getItem("accessToken")
  );
  const [refreshToken, setRefreshTokenState] = useState(
    localStorage.getItem("refreshToken")
  );

  console.log(user);

  const setAuth = useCallback((data: AuthUserDto | null) => {
    console.log("setAuth");
    if (data) {
      localStorage.setItem("accessToken", data.accessToken);
      localStorage.setItem("refreshToken", data.refreshToken);
    } else {
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");
    }

    setAccessTokenState(data?.accessToken ?? null);
    setRefreshTokenState(data?.refreshToken ?? null);
    setUser(data?.user);
  }, []);

  const autoLoginUserMutation = useMutation<AuthUserDto, Error, void>({
    mutationFn: async () => {
      if (!refreshToken || !accessToken) {
        throw new Error("Missing tokens");
      }
      return await refresh(refreshToken, accessToken);
    },
    onSuccess: (data) => {
      setAuth(data);
      console.log("Auth success");
    },
    onError: (error) => {
      console.error("Auth error: ", error);
      localStorage.removeItem("accessToken");
      localStorage.removeItem("refreshToken");
    },
  });

  useEffect(() => {
    if (refreshToken && accessToken) {
      autoLoginUserMutation.mutate();
    }
  }, []);

  const loginUserMutation = useMutation<AuthUserDto, Error, LoginUserCommand>({
    mutationFn: async (params: LoginUserCommand) => login(params),
    onSuccess: setAuth,
    onError: (error) => console.error("Login failed:", error),
  });

  const signUpUserMutation = useMutation<
    AuthUserDto,
    Error,
    RegisterUserCommand
  >({
    mutationFn: async (params: RegisterUserCommand) => signUp(params),
    onSuccess: setAuth,
    onError: (error) => console.error("Sign Up failed:", error),
  });

  return {
    user,
    loginUser: loginUserMutation.mutateAsync,
    signUpUser: signUpUserMutation.mutateAsync,
  };
};
