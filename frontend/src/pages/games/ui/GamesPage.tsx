import { GamesList } from "@/features/gamesList";
import { GamesFilter } from "@/shared/api";
import { Typography, Pagination, Skeleton, Box } from "@mui/material";
import { GameFilters } from "./Filters/GameFilters";
import {
  GamesListContainer,
  GamesListInfo,
  GamesPageContainer,
  GamesPageWrapper,
} from "./GamesPage.styled";
import { useGameFilters, useGamesQuery } from "@/entities/game";

export const GamesPage = () => {
  const { filters, setFilters } = useGameFilters();

  const yearToDate = (year?: number): string | undefined => {
    if (!year) {
      return undefined;
    }

    const date = new Date(year, 0, 1);
    return date.toISOString();
  };
  const queryFilters: GamesFilter = {
    category: filters.category,
    startYear: yearToDate(filters.startYear),
    endYear: yearToDate(filters.endYear),
    genres: filters.genres ? Array.from(filters.genres) : undefined,
    platforms: filters.platforms ? Array.from(filters.platforms) : undefined,
    page: filters.page,
  };

  const { data: gamesList, isFetching } = useGamesQuery(queryFilters);

  console.log("data gamesList: " + gamesList);
  return (
    <GamesPageWrapper>
      <GamesListInfo>
        <Typography variant="h4">{`All games: ${
          (!gamesList || gamesList.totalCount === 0) && !isFetching
            ? `No results`
            : `${gamesList ? gamesList.totalCount : 0}`
        }`}</Typography>
        <Typography variant="h4">{`${
          gamesList ? gamesList.items.length : 0
        } on page`}</Typography>
      </GamesListInfo>
      <GamesPageContainer>
        <GameFilters />
        <GamesListContainer>
          {isFetching || !gamesList ? (
            <>
              {[...Array(6)].map((_, index) => (
                <Skeleton
                  key={index}
                  variant="rounded"
                  width="100%"
                  height={150}
                />
              ))}
              <Skeleton
                variant="rounded"
                width={300}
                height={30}
                sx={{ margin: "20px auto" }}
              />
            </>
          ) : gamesList.totalCount > 0 ? (
            <>
              <GamesList games={gamesList.items} type="grid" />
              <Pagination
                count={Math.ceil(gamesList.totalCount / gamesList.pageSize)}
                onChange={(_, number) =>
                  setFilters({ ...filters, page: number })
                }
                page={filters.page}
              />
            </>
          ) : (
            "No results"
          )}
        </GamesListContainer>
      </GamesPageContainer>
    </GamesPageWrapper>
  );
};
