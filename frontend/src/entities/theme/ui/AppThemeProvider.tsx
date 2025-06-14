import React, { useMemo, useState } from "react";
import {
  LOCAL_STORAGE_THEME_KEY,
  ThemeModes,
  ThemeContext,
} from "../config/themeContext.ts";
import { ThemeProvider as StyledThemeProvider } from "styled-components";
import { darkTheme, lightTheme } from "@/shared/types/theme/themeProvider.ts";
import { CssBaseline, ThemeProvider as MuiThemeProvider } from "@mui/material";

interface ThemeProviderProps {
  readonly children: React.ReactNode;
}

const defaultTheme =
  (localStorage.getItem(LOCAL_STORAGE_THEME_KEY) as ThemeModes) ||
  ThemeModes.LIGHT;

export const AppThemeProvider = ({ children }: ThemeProviderProps) => {
  const [mode, setMode] = useState<ThemeModes>(defaultTheme);

  const contextValue = useMemo(
    () => ({ theme: mode, setTheme: setMode }),
    [mode]
  );

  const currentTheme = useMemo(
    () => (mode === ThemeModes.LIGHT ? lightTheme : darkTheme),
    [mode]
  );

  return (
    <ThemeContext.Provider value={contextValue}>
      <MuiThemeProvider theme={currentTheme}>
        <StyledThemeProvider theme={currentTheme}>
          <CssBaseline />
          {children}
        </StyledThemeProvider>
      </MuiThemeProvider>
    </ThemeContext.Provider>
  );
};
