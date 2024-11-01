import {
  Card,
  CardMedia,
  CardContent,
  styled as styledMui,
} from "@mui/material";

export const StyledCard = styledMui(Card)(({ theme }) => ({
  width: "100%",
  display: "flex",
  flexDirection: "row",
  justifyContent: "flex-start",
  position: "relative",
}));

export const StyledCardMedia = styledMui(CardMedia)({
  height: "110px",
  width: "100px",
  borderRadius: "4px",
  marginLeft: "16px",
  marginTop: "16px",
  marginBottom: "16px",
});
export const StyledCardContent = styledMui(CardContent)({
  display: "flex",
  flexDirection: "column",
  alignItems: "flex-start",
});
