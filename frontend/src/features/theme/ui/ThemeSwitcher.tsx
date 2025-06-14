import { useTheme } from "@/entities/theme/lib/useTheme";

import { ThemeModes } from "@/entities/theme/config/themeContext";
import { ThemeSwitcherStyled } from "./ThemeSwitcher.styled";

export const ThemeSwitcher = () => {
  const { theme, toggleTheme } = useTheme();
  const isThemeDark = theme === ThemeModes.DARK;

  return <ThemeSwitcherStyled onClick={toggleTheme} checked={isThemeDark} />;
};
