import {
  FooterColStyled,
  FooterHeaderStyled,
  FooterLinkStyled,
  FooterListItemStyled,
  FooterListStyled,
} from "../footer.styled";

export const AboutMe = () => {
  return (
    <FooterColStyled>
      <FooterHeaderStyled>About me</FooterHeaderStyled>
      <FooterListStyled>
        <FooterListItemStyled>
          <FooterLinkStyled href="#">Github</FooterLinkStyled>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLinkStyled href="#">CV</FooterLinkStyled>
        </FooterListItemStyled>
        <FooterListItemStyled>
          <FooterLinkStyled href="#">Portfolio</FooterLinkStyled>
        </FooterListItemStyled>
      </FooterListStyled>
    </FooterColStyled>
  );
};
