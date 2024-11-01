import { CardActionArea, Typography } from "@mui/material";
import { FC } from "react";
import { ProductCardProps } from "./props";
import {
  StyledCard,
  StyledCardMedia,
  StyledCardContent,
} from "./productCard.styled";

export const ProductCard: FC<ProductCardProps> = ({
  title,
  imageUrl,
  description,
  onClick,
}) => {
  return (
    <CardActionArea onClick={onClick ?? undefined}>
      <StyledCard>
        {imageUrl && <StyledCardMedia image={imageUrl} />}

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
  );
};
