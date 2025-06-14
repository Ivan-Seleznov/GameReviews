import {
  FooterColStyled,
  FooterHeaderStyled,
  FooterListItemStyled,
  FooterListStyled,
} from "../Footer.styled";
import { FooterLink } from "./FooterLink";

export const AboutMe = () => {
  return (
    <FooterColStyled>
      <FooterHeaderStyled>About me</FooterHeaderStyled>
      <FooterListStyled>
        <FooterListItemStyled>
          <FooterLink href="https://github.com/Ivan-Seleznov">
            Github
          </FooterLink>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLink href="https://github.com/Ivan-Seleznov/GameReviews">
            Project Repository
          </FooterLink>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLink href="https://github.com/Ivan-Seleznov">
            CV / Portfolio
          </FooterLink>
        </FooterListItemStyled>
      </FooterListStyled>
    </FooterColStyled>
  );
};
