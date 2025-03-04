import {
  Box,
  Checkbox,
  CircularProgress,
  FormControlLabel,
  Typography,
} from "@mui/material";
import { useRef } from "react";
import { VirtualizedMultiSelectProps } from "./multiSelect.props";
import { FixedSizeList as List, ListChildComponentProps } from "react-window";

export const VirtualizedMultiSelect = ({
  label,
  options = [],
  selectedValues,
  setSelectedValues,
  isLoading = false,
  maxVisibleItems = 10,
  itemHeight = 50,
}: VirtualizedMultiSelectProps) => {
  const listRef = useRef(null);

  const Row = ({ index, style }: ListChildComponentProps) => {
    const option = options[index];

    return (
      <div style={style}>
        <FormControlLabel
          key={option}
          control={<Checkbox name={option} />}
          label={option}
          checked={selectedValues?.has(option)}
          onChange={(e, checked) => {
            const newValues = new Set(selectedValues);
            checked ? newValues.add(option) : newValues.delete(option);
            setSelectedValues(newValues);
          }}
        />
      </div>
    );
  };

  return (
    <Box>
      <Box display="flex" alignItems="center" mb={1}>
        <Typography variant="body1" mr={1}>
          {label}:
        </Typography>
        {isLoading && <CircularProgress size={20} />}
      </Box>
      {options.length > 0 && (
        <List
          ref={listRef}
          height={Math.min(options.length, maxVisibleItems) * itemHeight}
          itemCount={options.length}
          itemSize={itemHeight}
          width="100%"
        >
          {Row}
        </List>
      )}
    </Box>
  );
};
