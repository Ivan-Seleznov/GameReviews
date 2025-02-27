import { GameDetailsDto } from "@/shared/api";

export interface GameListStyledProps {
  type?: "flex" | "grid";
}
export interface GameListProps extends GameListStyledProps {
  games: GameDetailsDto[];
}
