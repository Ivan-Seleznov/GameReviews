import styled from "styled-components";

export const LogoStyled = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
`;

export interface ILogoLinkStyledProps {
  href: string;
}

export const LogoLinkStyled = styled.a.attrs<ILogoLinkStyledProps>(
  ({ href }) => ({
    href: href,
  })
)`
  display: flex;
  align-items: center;
  text-decoration: none;
`;

export const LogoTitleStyled = styled.h2`
  padding-left: 5px;
`;
