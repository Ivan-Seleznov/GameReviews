import { Typography } from "@mui/material";

interface GameInfoLabelProps {
  title: string;
  text: string;
}

export const GameInfoLabel = ({ title, text }: GameInfoLabelProps) => {
  return (
    <Typography>
      <Typography fontWeight="700" component="span">
        {`${title} `}
      </Typography>
      {text}
    </Typography>
  );
};
