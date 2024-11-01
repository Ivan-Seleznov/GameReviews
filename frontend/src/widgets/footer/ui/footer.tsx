import { Contacts } from "./components/contacts";
import { AboutMe } from "./components/aboutMe";
import { UsefulLinks } from "./components/usefulLinks";
import { FooterContentStyled, FooterStyled } from "./footer.styled";

export const Footer = () => {
  return (
    <FooterStyled>
      <FooterContentStyled>
        <UsefulLinks />
        <AboutMe />
        <Contacts />
      </FooterContentStyled>
    </FooterStyled>
  );
};
