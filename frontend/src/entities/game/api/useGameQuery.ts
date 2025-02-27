import { GameDetailsDto } from "@/shared/api";
import httpClient from "@/shared/api/httpClient";
import { useQuery } from "@tanstack/react-query";

const GAME_SLATE_TIME = 180000;

const fetchGame = async (value: number, signal: AbortSignal) => {
  const { data } = await httpClient.get<GameDetailsDto>(`/games/${value}`, {
    signal,
  });

  return data;
};

export const useGameQuery = (gameId: number | null) => {
  return useQuery<GameDetailsDto>({
    queryKey: ["game", gameId],
    queryFn: async ({ signal }) => await fetchGame(gameId!, signal),
    enabled: gameId != null,
    staleTime: GAME_SLATE_TIME, //3 minutes
  });
};
