import { AuthUserDto } from "@/shared/api";
import httpClient from "@/shared/api/httpClient";

export const refresh = async (refreshToken: string, accessToken: string) => {
  const { data } = await httpClient.post<AuthUserDto>("/auth/refresh", {
    refreshToken: refreshToken,
    accessToken: accessToken,
  });
  return data;
};
