import { GameFiltersListProps } from "./gameFilterProps";
import { usePlatformsQuery } from "@/entities/game";
import { VirtualizedMultiSelect } from "@/shared/ui";
import { memo, useCallback } from "react";

//const PLATFORMS = new Set<string>(["PS5", "PS4", "XBOX 360", "XBOX ONE", "PC"]);

export const PlatformFilters = memo(
  ({ list: gamePlatforms, setGameFilters }: GameFiltersListProps) => {
    const { data: platforms, isFetching } = usePlatformsQuery();

    const handleSetGenres = useCallback(
      (newGenres: Set<string>) =>
        setGameFilters((prev) => {
          return { ...prev, platforms: newGenres };
        }),
      [setGameFilters]
    );

    return (
      <VirtualizedMultiSelect
        label="Platforms"
        options={platforms?.items?.map((g) => g.name) || []}
        selectedValues={gamePlatforms}
        setSelectedValues={handleSetGenres}
        isLoading={isFetching}
      />
    );
  }
);
