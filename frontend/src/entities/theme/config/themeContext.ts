import { createContext } from "react";

export enum ThemeModes {
  LIGHT = "light",
  DARK = "dark",
}
export interface ThemeContext {
  theme?: ThemeModes;
  setTheme?: (mode: ThemeModes) => void;
}
export const ThemeContext = createContext<ThemeContext>({});
export const LOCAL_STORAGE_THEME_KEY = "theme";
