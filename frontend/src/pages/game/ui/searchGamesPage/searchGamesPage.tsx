import { SearchGamesListWidget } from "@/widgets/game/ui/searchGamesList";
import { GamesPageWrapper } from "./searchGamesPage.styled";
export const SearchGamesPage = () => {
  return (
    <GamesPageWrapper>
      <SearchGamesListWidget />
    </GamesPageWrapper>
  );
};
