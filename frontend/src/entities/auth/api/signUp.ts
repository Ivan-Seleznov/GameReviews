import { RegisterUserCommand, AuthUserDto } from "@/shared/api";
import httpClient from "@/shared/api/httpClient";

export const signUp = async (registerUsercommand: RegisterUserCommand) => {
  const { data } = await httpClient.post<AuthUserDto>(
    "/auth/register",
    registerUsercommand
  );
  return data;
};
