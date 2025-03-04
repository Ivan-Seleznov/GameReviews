import { VirtualizedMultiSelect } from "@/shared/ui/multiSelects/VirtualizedMultiSelect";
import { GameFiltersListProps, GameFiltersProps } from "./gameFilterProps";
import { useGenresQuery } from "@/entities/game";
import { memo, useCallback } from "react";

export const GenreFilters = memo(
  ({ list: gameGenres, setGameFilters }: GameFiltersListProps) => {
    const { data: genres, isFetching } = useGenresQuery();

    const handleSetGenres = useCallback(
      (newGenres: Set<string>) =>
        setGameFilters((prev) => {
          return { ...prev, genres: newGenres };
        }),
      [setGameFilters]
    );

    return (
      <VirtualizedMultiSelect
        label="Genres"
        options={genres?.items?.map((g) => g.name) || []}
        selectedValues={gameGenres}
        setSelectedValues={handleSetGenres}
        isLoading={isFetching}
      />
    );
  }
);
