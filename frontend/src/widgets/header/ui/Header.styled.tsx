import styled from "styled-components";

export const HeaderContainer = styled.header`
  width: 100%;
  height: 55px;
  background-color: ${({ theme }) => theme.body};
  color: ${({ theme }) => theme.text};
  user-select: none;
`;

export const HeaderWrapper = styled.div`
  height: 100%;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: space-between;
  padding: 0px 60px;
`;

export const NavMenu = styled.nav`
  display: flex;
  align-items: center;
`;

export const NavMenuList = styled.ul`
  padding-left: 20px;
  display: flex;
  gap: 20px;
`;

export const NavMenuItem = styled.li`
  display: inline-block;
`;

export const HeaderRight = styled.div`
  margin-left: 20px;
  width: 100%;
  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: flex-end;
  gap: 20px;
`;

export const LogoContainer = styled.div`
  display: flex;
  justify-content: center;
  align-items: center;
`;
