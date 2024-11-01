import { FC } from "react";
import SportsEsportsIcon from "@mui/icons-material/SportsEsports";
import { LogoLinkStyled, LogoStyled, LogoTitleStyled } from "./logo.styled";

interface ILogoProps {
  link?: string;
  className?: string;
}

export const Logo: FC<ILogoProps> = ({ link, className = "" }) => {
  const logoContent = (
    <>
      <SportsEsportsIcon fontSize="large" />
      <LogoTitleStyled></LogoTitleStyled>
      <h2 className="logo-title">GameReviews</h2>
    </>
  );

  if (link) {
    return (
      <LogoStyled className={className}>
        <LogoLinkStyled href={link}>{logoContent}</LogoLinkStyled>
      </LogoStyled>
    );
  }

  return <LogoStyled className={className}>{logoContent}</LogoStyled>;
};
