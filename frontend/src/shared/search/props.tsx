export interface SearchProps<T> {
  endpoint: string;
  queryKey: string;
  getOptionLabel: (option: T | string) => string;
  getOptionKey: (option: T) => string;
  onEnterKeyDown?: (inputValue: string) => void;
  onOptionChanged?: (option: T) => void;
  label?: string;
  queryParam?: string;
  debounceTimeout?: number;
}
export interface FetchGamesProps {
  searchValue: string;
  apiUrl: string;
  queryParam?: string;
  signal: AbortSignal;
}
