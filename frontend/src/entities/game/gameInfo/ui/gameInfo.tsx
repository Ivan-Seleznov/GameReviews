import { Typography } from "@mui/material";
import {
  GameInfoContent,
  GameInfoImageContainer,
  GameInfoTextContainer,
  GameInfoWrapper,
  GamePlatform,
  GamePlatforms,
} from "./gameInfo.styled";
import { GameInfoProps } from "./props";

const logos = [
  "https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/PlayStation_4_logo_and_wordmark.svg/2560px-PlayStation_4_logo_and_wordmark.svg.png",
  "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSqXHzZgonqBRA6YUsD7bmWoiVuMK7DSF0S1A&s",
  "https://upload.wikimedia.org/wikipedia/commons/d/d9/PlayStation_3_Logo.svg",
  "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8c/XBOX_logo_2012.svg/2560px-XBOX_logo_2012.svg.png",
];

interface GameFieldProps {
  title: string;
  text: string;
}
export const GameField = ({ text, title }: GameFieldProps) => {
  return (
    <Typography>
      <Typography fontWeight="700" component="span">
        {`${title} `}
      </Typography>
      {text}
    </Typography>
  );
};

export const GameInfo = ({ game }: GameInfoProps) => {
  const getImageUrl = (imageType: "screenshot" | "cover"): string => {
    if (
      imageType === "screenshot" &&
      game.screenshots &&
      0 in game.screenshots
    ) {
      return game.screenshots[0].url;
    } else if (game.cover) {
      return game.cover.url;
    }

    return "https://shared.akamai.steamstatic.com/store_item_assets/steam/apps/292030/ss_90a33d7764a2d23306091bfcb52265c3506b4fdb.1920x1080.jpg?t=1726045366";
  };
  const getReleaseDate = (): string => {
    if (!game.releaseDate) {
      return "none";
    }

    const jsonDateString = game.releaseDate;
    const dateObject = new Date(jsonDateString);

    return dateObject.toDateString();
  };
  return (
    <GameInfoWrapper>
      <GameInfoImageContainer>
        <img src={getImageUrl("screenshot")} />
      </GameInfoImageContainer>
      <GameInfoContent>
        <Typography variant="h3">{game.name}</Typography>
        <GamePlatforms>
          {logos.map((logo) => (
            <GamePlatform id={logo} src={logo} loading="lazy" />
          ))}
        </GamePlatforms>
        <GameInfoTextContainer>
          {game.releaseDate && (
            <GameField title="Released on:" text={getReleaseDate()} />
          )}
          {game.gammeStatus && (
            <GameField title="Status: " text={game.gammeStatus} />
          )}
          {game.category && (
            <GameField title="Category:" text={game.category} />
          )}
        </GameInfoTextContainer>
      </GameInfoContent>
    </GameInfoWrapper>
  );
};
