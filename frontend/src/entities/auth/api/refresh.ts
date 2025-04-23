import { AuthUserDto } from "@/shared/api";
import httpClient from "@/shared/api/httpClient";

export const refresh = async (
  refreshToken: string,
  accessToken: string,
  signal?: AbortSignal
) => {
  const { data } = await httpClient.post<AuthUserDto>("/auth/refresh", {
    refreshToken: refreshToken,
    accessToken: accessToken,
    signal: signal,
  });
  return data;
};
