import { GameDetailsDto } from "@/shared/api";
import { Search } from "@/shared/search";
import { useNavigate } from "react-router-dom";

export const GamesSearch = () => {
  const navigate = useNavigate();

  return (
    <Search<GameDetailsDto>
      endpoint="/games/search"
      getOptionLabel={(option) =>
        typeof option === "string" ? option : option.name
      }
      getOptionKey={(option) => `${option.id}`}
      label="Game"
      queryKey="search/search"
      onEnterKeyDown={(inputValue) =>
        navigate(`search/games/?name=${inputValue}`)
      }
      onOptionChanged={(option) => navigate(`game?id=${option.id}`)}
    />
  );
};
