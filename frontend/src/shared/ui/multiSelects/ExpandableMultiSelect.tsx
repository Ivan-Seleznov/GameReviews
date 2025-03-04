import {
  Box,
  Typography,
  CircularProgress,
  FormGroup,
  FormControlLabel,
  Checkbox,
  Button,
} from "@mui/material";
import { useState, useEffect } from "react";
import { ExpandableMultiSelectProps } from "./multiSelect.props";

export const ExpandableMultiSelect = ({
  label,
  options = [],
  selectedValues,
  setSelectedValues,
  isLoading = false,
  initialVisibleCount = 5,
}: ExpandableMultiSelectProps) => {
  const [showMore, setShowMore] = useState(false);

  const handleShowMoreClick = () => setShowMore((prev) => !prev);

  useEffect(() => {
    console.warn(selectedValues);
  }, [selectedValues]);
  return (
    <Box>
      <Box display="flex" alignItems="center" mb={1}>
        <Typography variant="body1" mr={1}>
          {label}:
        </Typography>
        {isLoading && <CircularProgress size={20} />}
      </Box>
      {options.length > 0 && (
        <>
          <FormGroup sx={{ display: "flex", flexWrap: "wrap", gap: "2px" }}>
            {Array.from(options)
              .slice(0, showMore ? options.length : initialVisibleCount)
              .map((option) => (
                <FormControlLabel
                  key={option}
                  control={<Checkbox name={option} />}
                  label={option}
                  checked={selectedValues?.has(option)}
                  onChange={(e, checked) => {
                    const newValues = new Set(selectedValues);
                    if (checked) {
                      newValues.add(option);
                    } else {
                      newValues.delete(option);
                    }
                    setSelectedValues(newValues);
                  }}
                />
              ))}
          </FormGroup>
          {options.length > initialVisibleCount && (
            <Button onClick={handleShowMoreClick} variant="text">
              {showMore ? "Show Less" : "Show More"}
            </Button>
          )}
        </>
      )}
    </Box>
  );
};
