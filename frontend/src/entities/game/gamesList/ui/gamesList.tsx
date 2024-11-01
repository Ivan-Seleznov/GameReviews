import { CardsList } from "@/shared/ui/cardsList";
import { ProductCard } from "@/shared/ui/productCard";
import { FC } from "react";
import { GameListProps } from "./props";
import { GameDetailsDto } from "@/shared/api";
import { useNavigate } from "react-router-dom";

export const GamesList: FC<GameListProps> = ({ games }) => {
  const navigate = useNavigate();
  const onGameClicked = (game: GameDetailsDto) => {
    navigate(`/game/?id=${game.id}`);
  };

  return (
    <CardsList type="flex">
      {games.map((value) => (
        <ProductCard
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
