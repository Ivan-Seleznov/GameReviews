import { GameInfo } from "@/entities/game/gameInfo";
import {
  GameDetailsContent,
  GameDetailsWrapper,
  GameInfoContainer,
} from "./gameDetails.styled";
import { useSearchParams } from "react-router-dom";
import httpClient from "@/shared/lib/httpClient";
import { GameDetailsDto } from "@/shared/api";
import { useQuery } from "@tanstack/react-query";
import { Skeleton, Typography } from "@mui/material";

const fetchGame = async (value: number | null, signal: AbortSignal) => {
  if (value === null) {
    return null;
  }

  const response = await httpClient.get<GameDetailsDto>(`/games/${value}`, {
    signal: signal,
  });
  return (response.data as GameDetailsDto) ?? null;
};

export const GameDetailsWidget = () => {
  const [search] = useSearchParams();
  const id = search.get("id");

  const gameId = !Number.isNaN(id) ? Number(id) : null;

  console.log("gameId: " + gameId);

  const { data, isFetching } = useQuery<GameDetailsDto | null>({
    queryKey: ["game", gameId],
    queryFn: async ({ signal }) => await fetchGame(gameId, signal),
    enabled: true,
  });

  return (
    <GameDetailsWrapper>
      <GameDetailsContent>
        <GameInfoContainer>
          {isFetching ? (
            <div>
              <Skeleton variant="rectangular" width="50%" height={400} />
              <Skeleton variant="rectangular" width="50%" height={400} />
            </div>
          ) : (
            data && <GameInfo game={data} />
          )}
        </GameInfoContainer>
        <Typography>{data?.description}</Typography>
      </GameDetailsContent>
    </GameDetailsWrapper>
  );
};
