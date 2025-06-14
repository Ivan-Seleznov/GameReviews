import {
  FooterColStyled,
  FooterHeaderStyled,
  FooterListStyled,
  FooterListItemStyled,
} from "../Footer.styled";
import { FooterLink } from "./FooterLink";

export const Contacts = () => {
  return (
    <FooterColStyled>
      <FooterHeaderStyled>Contacts</FooterHeaderStyled>
      <FooterListStyled>
        <FooterListItemStyled>
          <FooterLink href="#">Telegram</FooterLink>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLink href="mailto:ivan.selezniov228333@gmail.com">
            Email
          </FooterLink>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLink href="#">Instagram</FooterLink>
        </FooterListItemStyled>
      </FooterListStyled>
    </FooterColStyled>
  );
};
