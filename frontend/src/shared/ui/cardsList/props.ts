export interface CardListWrapperProps {
  type?: "flex" | "grid";
}

export interface CardsListProps extends CardListWrapperProps {
  children: React.ReactNode;
}
