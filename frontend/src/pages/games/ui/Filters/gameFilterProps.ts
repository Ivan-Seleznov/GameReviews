import { ActualGameFilters } from "@/entities/game";
interface GameFilterProps {
  setGameFilters: React.Dispatch<React.SetStateAction<ActualGameFilters>>;
}
export interface CategoryFilterProps extends GameFilterProps {
  category: string;
  setGameFilters: React.Dispatch<React.SetStateAction<ActualGameFilters>>;
}
export interface GameYearFilterProps extends GameFilterProps {
  startYear?: number;
  endYear?: number;
  setGameFilters: React.Dispatch<React.SetStateAction<ActualGameFilters>>;
}

export interface GameFiltersListProps {
  list?: Set<string>;
  setGameFilters: React.Dispatch<React.SetStateAction<ActualGameFilters>>;
}
