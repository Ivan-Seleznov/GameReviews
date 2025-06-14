import { createTheme } from "@mui/material";
import { Theme as MuiTheme } from "@mui/material/styles";

declare module "styled-components" {
  export interface DefaultTheme extends MuiTheme {}
}

export const lightTheme = createTheme({
  palette: {
    primary: { main: "#1976d2" },
    background: { default: "#f5f5f5", paper: "#fff" },
  },
  typography: { fontFamily: '"Inter", sans-serif' },
});

export const darkTheme = createTheme({
  palette: {
    mode: "dark",
    primary: { main: "#90caf9" },
    background: { default: "#121212", paper: "#1d1d1d" },
  },
  typography: { fontFamily: '"Inter", sans-serif' },
});
