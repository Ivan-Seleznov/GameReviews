import { NavLink } from "react-router-dom";
import { ThemeSwitcher } from "@/features/theme/themeSwitcher";
import { Logo } from "@/shared/ui/logo/logo";
import { GamesSearch } from "@/features/game";

import {
  HeaderContainer,
  HeaderRight,
  HeaderWrapper,
  LogoContainer,
  NavMenu,
  NavMenuItem,
  NavMenuList,
} from "./header.styled";

export const Header = () => {
  return (
    <HeaderContainer>
      <HeaderWrapper>
        <NavMenu>
          <LogoContainer>
            <Logo link="/" />
          </LogoContainer>
          <NavMenuList>
            <NavMenuItem>
              <NavLink
                to="/games"
                className={({ isActive }) =>
                  isActive
                    ? "header-nav-menu-link"
                    : "header-nav-menu-link_active"
                }
              >
                Games
              </NavLink>
            </NavMenuItem>
            <NavMenuItem>
              <NavLink
                to="/reviews"
                className={({ isActive }) =>
                  isActive
                    ? "header-nav-menu-link"
                    : "header-nav-menu-link_active"
                }
              >
                Reviews
              </NavLink>
            </NavMenuItem>
            <NavMenuItem>
              <NavLink
                to="/about"
                className={({ isActive }) =>
                  isActive
                    ? "header-nav-menu-link"
                    : "header-nav-menu-link_active"
                }
              >
                Contacts
              </NavLink>
            </NavMenuItem>
          </NavMenuList>
        </NavMenu>
        <HeaderRight>
          <GamesSearch />
          <a>Account</a>
          <ThemeSwitcher />
        </HeaderRight>
      </HeaderWrapper>
    </HeaderContainer>
  );
};
