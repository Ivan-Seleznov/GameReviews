import { useNavigate, useSearchParams } from "react-router-dom";
import {
  GameFiltersContainer,
  GamesListContainer,
  GamesPageContainer,
} from "./GamesPage.styled";

export const GamesPage = () => {
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams();

  return (
    <GamesPageContainer>
      <GameFiltersContainer></GameFiltersContainer>
      <GamesListContainer></GamesListContainer>
    </GamesPageContainer>
  );
};
