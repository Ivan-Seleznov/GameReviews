interface MultiSelectProps {
  label: string;
  options?: string[];
  selectedValues?: Set<string>;
  setSelectedValues: (values: Set<string>) => void;
  isLoading?: boolean;
}

export interface VirtualizedMultiSelectProps extends MultiSelectProps {
  maxVisibleItems?: number;
  itemHeight?: number;
}

export interface ExpandableMultiSelectProps extends MultiSelectProps {
  initialVisibleCount?: number;
}
