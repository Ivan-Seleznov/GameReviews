export interface ProductCardStyleProps {
  type: "flex" | "grid";
}

export interface ProductCardProps extends ProductCardStyleProps {
  title: string;
  imageUrl?: string;
  description?: string;
  onClick?: () => void;
}
