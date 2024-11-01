import {
  FooterColStyled,
  FooterHeaderStyled,
  FooterListStyled,
  FooterListItemStyled,
  FooterLinkStyled,
} from "../footer.styled";

export const UsefulLinks = () => {
  return (
    <FooterColStyled>
      <FooterHeaderStyled>Useful links</FooterHeaderStyled>
      <FooterListStyled>
        <FooterListItemStyled>
          <FooterLinkStyled href="#">Games</FooterLinkStyled>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLinkStyled href="#">Reviews</FooterLinkStyled>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLinkStyled href="#">Profile</FooterLinkStyled>
        </FooterListItemStyled>
      </FooterListStyled>
    </FooterColStyled>
  );
};
