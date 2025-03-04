import { GenreDto, PagedList } from "@/shared/api";
import httpClient from "@/shared/api/httpClient";
import { useQuery } from "@tanstack/react-query";

const fetchGenres = async (signal: AbortSignal) => {
  const { data } = await httpClient.get<PagedList<GenreDto>>(
    `/genres/game?page=1&pageSize=100`,
    { signal }
  );
  console.log("data: " + data.items);
  return data;
};

export const useGenresQuery = () => {
  return useQuery<PagedList<GenreDto>>({
    queryKey: ["genres/game"],
    queryFn: async ({ signal }) => await fetchGenres(signal),
    refetchOnWindowFocus: false,
    staleTime: Infinity,
    gcTime: Infinity,
  });
};
