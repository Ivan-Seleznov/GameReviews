import { useSearchParams } from "react-router-dom";
import {
  GameContent,
  GameContentWrapper,
  GameImagesContainer,
  GamePageWrapper,
  InformationSection,
  PlatformImage,
  PlatformsContainer,
} from "./GamePage.styled";
import { Skeleton, Typography } from "@mui/material";
import { getFormattedDate, getFormattedImageUrl } from "@/shared/lib";
import { useGameQuery } from "@/entities/game";
import { GameInfoLabel } from "./GameInfoLabel";

const logos = [
  "https://upload.wikimedia.org/wikipedia/commons/thumb/8/87/PlayStation_4_logo_and_wordmark.svg/2560px-PlayStation_4_logo_and_wordmark.svg.png",
  "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSqXHzZgonqBRA6YUsD7bmWoiVuMK7DSF0S1A&s",
  "https://upload.wikimedia.org/wikipedia/commons/d/d9/PlayStation_3_Logo.svg",
  "https://upload.wikimedia.org/wikipedia/commons/thumb/8/8c/XBOX_logo_2012.svg/2560px-XBOX_logo_2012.svg.png",
];

export const GamePage = () => {
  const [search] = useSearchParams();
  const id = search.get("id");

  const gameId = !isNaN(Number(id)) ? Number(id) : null;
  const { data: game, isFetching, error } = useGameQuery(gameId);

  console.log("gameId: " + gameId);
  if (gameId === null) {
    return <h1>Incorrect URL</h1>;
  }

  if (error) {
    return <h1>Error fetching</h1>;
  }

  if (isFetching || !game) {
    return (
      <GamePageWrapper>
        <GameContentWrapper>
          <Skeleton variant="rounded" width="100%" height={500} />
          <GameContent>
            <Skeleton variant="text" width="60%" />
            <Skeleton variant="text" width="40%" />
            <Skeleton variant="text" width="80%" />
            <Skeleton variant="text" width="50%" />
          </GameContent>
        </GameContentWrapper>
        <Skeleton variant="text" width="100%" height={100} />
      </GamePageWrapper>
    );
  }

  console.log("imageUrl: " + game?.screenshots?.at(0)?.url);

  return (
    <GamePageWrapper>
      <GameContentWrapper>
        {game.screenshots && game.screenshots[0] && (
          <GameImagesContainer>
            <img src={getFormattedImageUrl(game.screenshots[0].url, "1080p")} />
          </GameImagesContainer>
        )}
        <GameContent>
          <Typography variant="h3">{game.name}</Typography>
          <PlatformsContainer>
            {logos.map((logo) => (
              <PlatformImage id={logo} src={logo} loading="lazy" />
            ))}
          </PlatformsContainer>
          <InformationSection>
            {game.releaseDate && (
              <GameInfoLabel
                title="Released on:"
                text={getFormattedDate(game.releaseDate)}
              />
            )}
            {game.gammeStatus && (
              <GameInfoLabel title="Status: " text={game.gammeStatus} />
            )}
            {game.category && (
              <GameInfoLabel title="Category:" text={game.category} />
            )}
          </InformationSection>
        </GameContent>
      </GameContentWrapper>
      <Typography>{game.description}</Typography>
    </GamePageWrapper>
  );
};
