import {
  FooterColStyled,
  FooterHeaderStyled,
  FooterListStyled,
  FooterListItemStyled,
} from "../Footer.styled";
import { FooterLink } from "./FooterLink";

export const UsefulLinks = () => {
  return (
    <FooterColStyled>
      <FooterHeaderStyled>Useful links</FooterHeaderStyled>
      <FooterListStyled>
        <FooterListItemStyled>
          <FooterLink external={false} href="/games">
            Games
          </FooterLink>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLink external={false} href="/reviews">
            Reviews
          </FooterLink>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLink external={false} href="/profile">
            Profile
          </FooterLink>
        </FooterListItemStyled>
      </FooterListStyled>
    </FooterColStyled>
  );
};
