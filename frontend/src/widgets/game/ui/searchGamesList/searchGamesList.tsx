import { GamesList } from "@/entities/game/gamesList";
import {
  SearchGamesContent,
  SearchGamesTopContent,
  SearchGamesWrapper,
} from "./searchGamesList.styled";
import { GameDetailsDto, PagedList } from "@/shared/api";
import { useNavigate, useSearchParams } from "react-router-dom";
import { useQuery } from "@tanstack/react-query";
import httpClient from "@/shared/lib/httpClient";
import { Pagination, Typography } from "@mui/material";

const fetchGames = async (
  signal: AbortSignal,
  value: string | null,
  page?: number
) => {
  if (value == null || value.length <= 0) {
    return null;
  }

  const response = await httpClient.get<PagedList<GameDetailsDto>>(
    `/games/search?searchTerm=${value}&pageSize=20&page=${page ?? 1}`,
    {
      signal: signal,
    }
  );
  console.log("gamedata " + response.data);
  return (response.data as PagedList<GameDetailsDto>) ?? null;
};

export const SearchGamesListWidget = () => {
  const navigate = useNavigate();
  const [params] = useSearchParams();
  const gameName = params.get("name");

  let page = Number(params.get("page"));
  if (page < 1) {
    page = 1;
  }

  console.log("page: " + page);
  const { data, isFetching } = useQuery<PagedList<GameDetailsDto> | null>({
    queryKey: ["games", gameName, page],
    queryFn: async ({ signal }) => await fetchGames(signal, gameName, page),
    enabled: true,
  });

  console.log(
    "pages count: " + (data ? Math.ceil(data.totalCount / data.pageSize) : 0)
  );
  console.log("length: " + data?.items.length);
  return (
    <SearchGamesWrapper>
      <SearchGamesContent>
        <SearchGamesTopContent>
          <Pagination
            count={data ? Math.ceil(data.totalCount / data.pageSize) : 0}
            onChange={(_, number) =>
              navigate(`?name=${gameName}&page=${number}`)
            }
            page={page}
          />
          <Typography variant="h4">{`Results for "${gameName}": ${
            (!data || data.totalCount === 0) && !isFetching
              ? `No results`
              : `${data ? data.totalCount : 0}`
          }`}</Typography>
          <Typography variant="h4">{`${
            data ? data.items.length : 0
          } on page`}</Typography>
        </SearchGamesTopContent>
        <GamesList games={data ? data.items : []} />
      </SearchGamesContent>
    </SearchGamesWrapper>
  );
};
