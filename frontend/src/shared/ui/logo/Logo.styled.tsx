import styled from "styled-components";
import { LogoLinkStyleProps } from "./logo.props";

export const LogoWrapper = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
`;

export const LogoLink = styled.a.attrs<LogoLinkStyleProps>(({ href }) => ({
  href: href,
}))`
  display: flex;
  align-items: center;
  text-decoration: none;
`;

export const LogoTitle = styled.h2`
  padding-left: 5px;
`;
