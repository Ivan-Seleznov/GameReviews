import { PagedList, PlatformDto } from "@/shared/api";
import httpClient from "@/shared/api/httpClient";
import { useQuery } from "@tanstack/react-query";

const fetchPlatforms = async (signal: AbortSignal) => {
  const { data } = await httpClient.get<PagedList<PlatformDto>>(
    `/platforms/game?page=1&pageSize=500`,
    { signal }
  );
  console.log("data: " + data.items);
  return data;
};

export const usePlatformsQuery = () => {
  return useQuery<PagedList<PlatformDto>>({
    queryKey: ["platforms/game"],
    queryFn: async ({ signal }) => await fetchPlatforms(signal),
    refetchOnWindowFocus: false,
    staleTime: Infinity,
    gcTime: Infinity,
  });
};
