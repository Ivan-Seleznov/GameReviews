import {
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";
import { CategoryFilterProps } from "./gameFilterProps";

const CATEGORIES = [
  "All",
  "MainGame",
  "DlcAddon",
  "Expansion",
  "Bundle",
  "StandaloneExpansion",
  "Mod",
  "Episode",
  "Season",
  "Remake",
  "Remaster",
  "ExpandedGame",
  "Port",
  "Fork",
  "Pack",
  "Update",
];

export const GameCategoryFilter = ({
  category,
  setGameFilters,
}: CategoryFilterProps) => (
  <FormControl fullWidth>
    <InputLabel id="category-select-label">Category</InputLabel>
    <Select
      labelId="category-select-label"
      id="category-select"
      value={category}
      label="Category"
      onChange={(event: SelectChangeEvent<string>) =>
        setGameFilters((prev) => {
          return {
            ...prev,
            category: event.target.value,
          };
        })
      }
    >
      {CATEGORIES.map((category) => (
        <MenuItem key={category} value={category}>
          {category}
        </MenuItem>
      ))}
    </Select>
  </FormControl>
);
