import { GamesFilter } from "@/shared/api";
import { useCallback, useMemo } from "react";
import { useSearchParams } from "react-router-dom";

export interface ActualGameFilters {
  category?: string;
  startYear?: number;
  endYear?: number;
  genres?: Set<string>;
  platforms?: Set<string>;
  page?: number;
}

export const useGameFilters = () => {
  const [searchParams, setSearchParams] = useSearchParams();

  const getParam = (key: string): string | undefined => {
    const value = searchParams.get(key);
    return value && value.trim() !== "" ? value : undefined;
  };

  const getNumberParam = (key: string) =>
    isNaN(Number(getParam(key))) ? undefined : Number(getParam(key));
  const getParamSet = (key: string) => {
    const paramsArray = searchParams.getAll(key);
    return paramsArray === null ? undefined : new Set<string>(paramsArray);
  };

  const filters: ActualGameFilters = useMemo(
    () => ({
      category: getParam("category") ?? "All",
      startYear: getNumberParam("startYear"),
      endYear: getNumberParam("endYear"),
      genres: getParamSet("genres"),
      platforms: getParamSet("platforms"),
      page: getNumberParam("page") ?? 1,
    }),
    [searchParams]
  );

  console.log("useGameFilters");
  const setFilters = useCallback((filters: ActualGameFilters) => {
    setSearchParams((params) => {
      if (filters.category !== undefined) {
        params.set("category", filters.category);
      }
      if (filters.startYear !== undefined) {
        params.set("startYear", filters.startYear.toString());
      } else {
        params.delete("startYear");
      }
      if (filters.endYear !== undefined) {
        params.set("endYear", filters.endYear.toString());
      } else {
        params.delete("endYear");
      }
      if (filters.genres !== undefined) {
        params.delete("genres");
        filters.genres.forEach((genre) => params.append("genres", genre));
      }
      if (filters.platforms !== undefined) {
        params.delete("platforms");
        filters.platforms.forEach((platform) =>
          params.append("platforms", platform)
        );
      }
      if (filters.page !== undefined && !isNaN(filters.page)) {
        params.set("page", filters.page.toString());
      }
      return params;
    });
  }, []);

  return {
    filters,
    setFilters,
  };
};
