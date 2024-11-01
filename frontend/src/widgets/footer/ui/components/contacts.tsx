import {
  FooterColStyled,
  FooterHeaderStyled,
  FooterListStyled,
  FooterListItemStyled,
  FooterLinkStyled,
} from "../footer.styled";

export const Contacts = () => {
  return (
    <FooterColStyled>
      <FooterHeaderStyled>Contacts</FooterHeaderStyled>
      <FooterListStyled>
        <FooterListItemStyled>
          <FooterLinkStyled href="#">Telegram</FooterLinkStyled>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLinkStyled href="#">Email</FooterLinkStyled>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLinkStyled href="#">Instagram</FooterLinkStyled>
        </FooterListItemStyled>
      </FooterListStyled>
    </FooterColStyled>
  );
};
