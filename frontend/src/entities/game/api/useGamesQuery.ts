import { GameInfoDto, GamesFilter, PagedList } from "@/shared/api";
import httpClient from "@/shared/api/httpClient";
import { buildQueryString } from "@/shared/lib";
import { useQuery } from "@tanstack/react-query";

const GAMES_SLATE_TIME = 60000;

const fetchGames = async (filters: GamesFilter, signal: AbortSignal) => {
  const queryString = buildQueryString(filters);
  console.log("quueryString: " + queryString);
  const { data } = await httpClient.get<PagedList<GameInfoDto>>(
    `/games?${queryString}`,
    { signal }
  );
  console.log("games data: " + data.items);
  return data;
};

export const useGamesQuery = (filters: GamesFilter) => {
  return useQuery<PagedList<GameInfoDto>>({
    queryKey: ["games", filters],
    queryFn: async ({ signal }) => await fetchGames(filters, signal),
    enabled: true,
    refetchOnWindowFocus: false,
    staleTime: GAMES_SLATE_TIME,
  });
};
