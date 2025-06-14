import { useNavigate, useSearchParams } from "react-router-dom";
import {
  GamesPageWrapper,
  SearchGamesContent,
  SearchGamesTopContent,
} from "./SearchGamesPage.styled";
import { Pagination, Skeleton, Typography } from "@mui/material";
import { GamesList } from "@/features/gamesList";
import { useSearchGamesQuery } from "@/entities/game";
import { CardsList } from "@/shared/ui";

export const SearchGamesPage = () => {
  const navigate = useNavigate();
  const [params] = useSearchParams();

  const gameName = params.get("name");
  const page = Math.max(Number(params.get("page")) || 1, 1);

  const {
    data: gamesList,
    isFetching,
    error,
  } = useSearchGamesQuery(gameName, page);

  if (error) {
    return <h1>Error</h1>;
  }

  if (isFetching || !gamesList) {
    const skeletonCount = 8;

    return (
      <GamesPageWrapper>
        <SearchGamesContent>
          <SearchGamesTopContent>
            <Skeleton variant="text" width="30%" />
            <Skeleton variant="text" width="25%" />
          </SearchGamesTopContent>
          <CardsList type="flex">
            {Array.from({ length: skeletonCount }).map((_, index) => (
              <Skeleton key={index} variant="rounded" height={100} />
            ))}
          </CardsList>
        </SearchGamesContent>
      </GamesPageWrapper>
    );
  }
  return (
    <GamesPageWrapper>
      <SearchGamesContent>
        <SearchGamesTopContent>
          <Typography variant="h4">{`Results for "${gameName}": ${
            (!gamesList || gamesList.totalCount === 0) && !isFetching
              ? `No results`
              : `${gamesList ? gamesList.totalCount : 0}`
          }`}</Typography>
          <Typography variant="h4">{`${
            gamesList ? gamesList.items.length : 0
          } on page`}</Typography>
        </SearchGamesTopContent>
        <GamesList type="flex" games={gamesList ? gamesList.items : []} />
        <Pagination
          count={
            gamesList ? Math.ceil(gamesList.totalCount / gamesList.pageSize) : 0
          }
          onChange={(_, number) => navigate(`?name=${gameName}&page=${number}`)}
          page={page}
          sx={{ my: "20px" }}
        />
      </SearchGamesContent>
    </GamesPageWrapper>
  );
};
