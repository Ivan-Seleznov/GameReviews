import SportsEsportsIcon from "@mui/icons-material/SportsEsports";
import { LogoLink, LogoWrapper, LogoTitle } from "./Logo.styled";

interface LogoProps {
  link?: string;
  className?: string;
}

export const Logo = ({ link, className = "" }: LogoProps) => {
  const logoContent = (
    <>
      <SportsEsportsIcon fontSize="large" />
      <LogoTitle></LogoTitle>
      <h2 className="logo-title">GameReviews</h2>
    </>
  );

  if (link) {
    return (
      <LogoWrapper className={className}>
        <LogoLink href={link}>{logoContent}</LogoLink>
      </LogoWrapper>
    );
  }

  return <LogoWrapper className={className}>{logoContent}</LogoWrapper>;
};
