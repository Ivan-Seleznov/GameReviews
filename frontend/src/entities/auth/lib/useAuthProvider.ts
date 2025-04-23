import {
  useCallback,
  useEffect,
  useLayoutEffect,
  useRef,
  useState,
} from "react";
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
import httpClient from "@/shared/api/httpClient";
import axios from "axios";

export const useAuthProvieder = (): AuthContextType => {
  const initialized = useRef(false);

  const [user, setUser] = useState<UserDetailsDto | undefined>(undefined);

  //TODO: Use Redux instead of states
  const [accessToken, setAccessTokenState] = useState(
    localStorage.getItem("accessToken")
  );
  const [refreshToken, setRefreshTokenState] = useState(
    localStorage.getItem("refreshToken")
  );

  console.log(user);

  const setAuth = useCallback((data: AuthUserDto | null) => {
    console.log("setAuth: ", data?.refreshToken);
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
      console.warn("autoLogiiUserMuttion");
      return await refresh(refreshToken, accessToken);
    },
    onSuccess: (data) => {
      setAuth(data);
      console.log("Auth success");
    },
    onError: (error) => {
      console.error("auto Auth error: ", error);

      setAuth(null);
    },
  });

  useLayoutEffect(() => {
    if (refreshToken && accessToken && !initialized.current) {
      initialized.current = true;
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

  useLayoutEffect(() => {
    const authInterceptor = httpClient.interceptors.request.use((config) => {
      config.headers.Authorization =
        !config._retry && accessToken
          ? `Bearer ${accessToken}`
          : config.headers.Authorization;

      return config;
    });

    return () => {
      httpClient.interceptors.request.eject(authInterceptor);
    };
  }, [accessToken]);

  useLayoutEffect(() => {
    const refreshInterceptor = httpClient.interceptors.response.use(
      (response) => response,
      async (error) => {
        if (
          axios.isAxiosError(error) &&
          error.response?.status === 401 &&
          refreshToken &&
          accessToken
        ) {
          try {
            console.log("t228 accessToken: ", accessToken);
            console.log("t228 refreshToen: ", refreshToken);

            const data = await refresh(refreshToken, accessToken);
            setAuth(data);

            if (error.config) {
              error.config._retry = true;
              error.config.headers.Authorization = `Bearer ${data.accessToken}`;

              console.warn("t228 token refreshed");
              return httpClient(error.config);
            }
          } catch (refreshError) {
            console.warn("t228 error token refr: ", refreshError);

            setAuth(null);
            return Promise.reject(refreshError);
          }
        }

        return Promise.reject(error);
      }
    );

    return () => {
      httpClient.interceptors.response.eject(refreshInterceptor);
    };
  }, [accessToken, refreshToken]);

  const logout = () => {
    setAuth(null);
  };

  return {
    user,
    loginUser: loginUserMutation.mutateAsync,
    signUpUser: signUpUserMutation.mutateAsync,
    logout,
  };
};
