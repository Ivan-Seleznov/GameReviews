import { CardsListWrapper } from "./cardsList.styled";
import { CardsListProps } from "./props";

export const CardsList: React.FC<CardsListProps> = ({ children, type }) => {
  return <CardsListWrapper type={type}>{children}</CardsListWrapper>;
};
