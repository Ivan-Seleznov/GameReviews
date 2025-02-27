import { CardsListWrapper } from "./CardsList.styled";
import { CardsListProps } from "./CardsList.props";

export const CardsList = ({ children, type }: CardsListProps) => {
  return <CardsListWrapper type={type}>{children}</CardsListWrapper>;
};
