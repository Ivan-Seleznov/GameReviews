import { Box, Button, Slider, Typography } from "@mui/material";
import { GameYearFilterProps } from "./gameFilterProps";
import { useMemo } from "react";

const MIN_YEAR = 1947;

export const GameYearFilter = ({
  startYear,
  endYear,
  setGameFilters,
}: GameYearFilterProps) => {
  const maxYear = useMemo(() => new Date().getFullYear() + 1, []);

  const handleChange = (_: Event, newValue: number | number[]) => {
    const [startYear, endYear] = newValue as number[];
    setGameFilters((prev) => {
      return { ...prev, startYear, endYear };
    });
  };

  const handleSetMin = () =>
    setGameFilters((prev) => {
      return { ...prev, startYear: MIN_YEAR };
    });
  const handleSetMax = () =>
    setGameFilters((prev) => {
      return { ...prev, endYear: maxYear };
    });
  const handleReset = () =>
    setGameFilters((prev) => {
      return { ...prev, endYear: undefined, startYear: undefined };
    });

  return (
    <Box sx={{ width: 320 }}>
      <Typography gutterBottom>Release Year:</Typography>
      <Slider
        getAriaLabel={() => "Release Year"}
        value={[startYear ?? MIN_YEAR, endYear ?? maxYear]}
        onChange={handleChange}
        valueLabelDisplay="auto"
        getAriaValueText={(value) => `Year: ${value}`}
        min={MIN_YEAR}
        max={maxYear}
      />
      <Box sx={{ display: "flex", justifyContent: "space-between" }}>
        <Typography
          variant="body2"
          onClick={handleSetMin}
          sx={{ cursor: "pointer", userSelect: "none" }}
        >
          {MIN_YEAR} min
        </Typography>
        <Typography
          variant="body2"
          onClick={handleSetMax}
          sx={{ cursor: "pointer", userSelect: "none" }}
        >
          {maxYear} max
        </Typography>
      </Box>
      <Button onClick={handleReset} variant="text">
        Reset
      </Button>
    </Box>
  );
};
