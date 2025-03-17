import { LoginUserCommand, AuthUserDto } from "@/shared/api";
import httpClient from "@/shared/api/httpClient";

export const login = async (loginUserCommand: LoginUserCommand) => {
  const { data } = await httpClient.post<AuthUserDto>(
    "/auth/login",
    loginUserCommand
  );
  return data;
};
