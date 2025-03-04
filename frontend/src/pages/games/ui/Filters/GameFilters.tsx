import { useGameFilters, ActualGameFilters } from "@/entities/game";
import { useState, useEffect } from "react";
import { useDebounce } from "use-debounce";
import { GameCategoryFilter } from "./GameCategoryFilter";
import { GameFiltersContainer } from "./GameFilters.styled";
import { GameYearFilter } from "./GameYearFilter";
import { GenreFilters } from "./GenreFilters";
import { PlatformFilters } from "./PlatformFilters";
import { Divider } from "@mui/material";

export const GameFilters = () => {
  const { filters, setFilters } = useGameFilters();
  const [localFilters, setLocalFilters] = useState<ActualGameFilters>(filters);
  const [debouncedFilters] = useDebounce(localFilters, 500);

  useEffect(() => {
    setFilters(debouncedFilters);
  }, [debouncedFilters, setFilters]);

  //const setGameFilters = (filters: ActualGameFilters) =>
  //  setLocalFilters(filters);

  return (
    <GameFiltersContainer>
      <GameCategoryFilter
        category={localFilters.category}
        setGameFilters={setLocalFilters}
      />
      <Divider flexItem />
      <GameYearFilter
        startYear={localFilters.startYear}
        endYear={localFilters.endYear}
        setGameFilters={setLocalFilters}
      />
      <Divider flexItem />
      <GenreFilters
        list={localFilters.genres}
        setGameFilters={setLocalFilters}
      />
      <Divider flexItem />
      <PlatformFilters
        list={localFilters.platforms}
        setGameFilters={setLocalFilters}
      />
    </GameFiltersContainer>
  );
};
