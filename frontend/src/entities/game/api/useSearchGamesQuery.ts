import { GameDetailsDto, PagedList } from "@/shared/api";
import httpClient from "@/shared/api/httpClient";
import { useQuery } from "@tanstack/react-query";

const SEARCH_GAME_SLATE_TIME = 180000;
const DEFAULT_SEARCH_PAGE_SIZE = 20;

const fetchGames = async (value: string, page: number, signal: AbortSignal) => {
  const { data } = await httpClient.get<PagedList<GameDetailsDto>>(
    `/games/search?searchTerm=${value}&pageSize=${DEFAULT_SEARCH_PAGE_SIZE}&page=${page}`,
    {
      signal: signal,
    }
  );

  return data;
};

export const useSearchGamesQuery = (gameName: string | null, page?: number) => {
  return useQuery<PagedList<GameDetailsDto>>({
    queryKey: ["games", gameName, page],
    queryFn: async ({ signal }) => await fetchGames(gameName!, page!, signal),
    enabled: !!gameName?.trim() && !!page,
    staleTime: SEARCH_GAME_SLATE_TIME,
    refetchOnWindowFocus: false,
  });
};
