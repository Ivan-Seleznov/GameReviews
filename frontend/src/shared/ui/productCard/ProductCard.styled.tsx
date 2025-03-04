import {
  Card,
  CardMedia,
  CardContent,
  styled as styledMui,
} from "@mui/material";
import { ProductCardStyleProps } from "./ProductCard.props";
import styled from "styled-components";

const sizeVariants = {
  flex: {
    imageHeight: "110px",
    imageWidth: "100px",
    cardHeight: "fit-content",
  },
  grid: {
    imageHeight: "110px",
    imageWidth: "150px",
    cardHeight: "136px",
  },
};
export const ProductCardContainer = styled.div({});

export const StyledCard = styledMui(Card)<ProductCardStyleProps>(({ type }) => {
  const sizeVariant = sizeVariants[type];

  return {
    width: "100%",
    display: "flex",
    flexDirection: "row",
    justifyContent: "flex-start",
    position: "relative",
    height: sizeVariant.cardHeight,
  };
});

export const StyledCardMedia = styledMui(CardMedia)<ProductCardStyleProps>(
  ({ type }) => {
    const sizeVariant = sizeVariants[type];

    return {
      height: sizeVariant.imageHeight,
      width: sizeVariant.imageWidth,
      borderRadius: "4px",
      marginLeft: "16px",
      marginTop: "16px",
      marginBottom: "16px",
    };
  }
);

export const StyledCardContent = styledMui(CardContent)({
  display: "flex",
  flexDirection: "column",
  alignItems: "flex-start",
});
