import { CardActionArea, Typography } from "@mui/material";
import { ProductCardProps } from "./ProductCard.props";
import {
  StyledCard,
  StyledCardMedia,
  StyledCardContent,
  ProductCardContainer,
} from "./ProductCard.styled";

export const ProductCard = ({
  type = "flex",
  title,
  imageUrl,
  description,
  onClick,
}: ProductCardProps) => {
  return (
    <ProductCardContainer>
      <CardActionArea onClick={onClick ?? undefined}>
        <StyledCard type={type}>
          {imageUrl && <StyledCardMedia image={imageUrl} type={type} />}

          <StyledCardContent sx={{ width: "inherit" }}>
            <Typography gutterBottom variant="h5">
              {title}
            </Typography>

            {description && (
              <Typography
                variant="body2"
                sx={{ color: "text.secondary", maxHeight: "100%" }}
              >
                {description}
              </Typography>
            )}
          </StyledCardContent>
        </StyledCard>
      </CardActionArea>
    </ProductCardContainer>
  );
};
