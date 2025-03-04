import { GameListProps } from "./GamesList.props";
import { useNavigate } from "react-router-dom";
import { CardsList, ProductCard } from "@/shared/ui";
import { GameInfoDto } from "@/shared/api";

export const GamesList = ({ games, type = "flex" }: GameListProps) => {
  const navigate = useNavigate();
  const onGameClicked = (game: GameInfoDto) => {
    navigate(`/game/?id=${game.id}`);
  };

  return (
    <CardsList type={type}>
      {games.map((value) => (
        <ProductCard
          type={type}
          title={value.name}
          description={value.description}
          imageUrl={value.cover?.url}
          key={value.id}
          onClick={() => onGameClicked(value)}
        />
      ))}
    </CardsList>
  );
};
