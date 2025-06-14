import { useContext } from "react";

import {
  LOCAL_STORAGE_THEME_KEY,
  ThemeModes,
  ThemeContext,
} from "../config/themeContext";

interface UseThemeResults {
  readonly theme?: ThemeModes;
  readonly toggleTheme: () => void;
}

export const useTheme = (): UseThemeResults => {
  const { theme, setTheme } = useContext(ThemeContext);

  const toggleTheme = () => {
    const newTheme =
      theme === ThemeModes.DARK ? ThemeModes.LIGHT : ThemeModes.DARK;
    setTheme?.(newTheme);
    localStorage.setItem(LOCAL_STORAGE_THEME_KEY, newTheme);

    console.log("toggleTheme=", newTheme);
  };

  return { theme, toggleTheme };
};
