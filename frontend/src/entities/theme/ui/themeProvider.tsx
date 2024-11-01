import { FC, useMemo, useState } from "react";
import {
  LOCAL_STORAGE_THEME_KEY,
  Theme,
  ThemeContext,
} from "../config/themeContext.ts";
import { ThemeProvider as StyledThemeProvider } from "styled-components";
import { darkTheme, lightTheme } from "@/shared/types/theme/themeProvider.ts";

interface IThemeProviderProps {
  readonly children: JSX.Element;
}

const defaultTheme =
  (localStorage.getItem(LOCAL_STORAGE_THEME_KEY) as Theme) || Theme.LIGHT;

export const ThemeProvider: FC<IThemeProviderProps> = ({ children }) => {
  const [theme, setTheme] = useState<Theme>(defaultTheme);

  const defaultValue = useMemo(
    () => ({
      theme,
      setTheme,
    }),
    [theme]
  );

  return (
    <ThemeContext.Provider value={defaultValue}>
      <StyledThemeProvider
        theme={theme === Theme.LIGHT ? lightTheme : darkTheme}
      >
        {children}
      </StyledThemeProvider>
    </ThemeContext.Provider>
  );
};
