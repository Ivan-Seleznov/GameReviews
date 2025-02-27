import { Contacts } from "./components/Contacts";
import { AboutMe } from "./components/AboutMe";
import { UsefulLinks } from "./components/UsefulLinks";
import { FooterContentStyled, FooterStyled } from "./Footer.styled";

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
