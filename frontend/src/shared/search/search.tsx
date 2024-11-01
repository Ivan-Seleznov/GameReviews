import { useState } from "react";
import { useDebounce } from "use-debounce";
import { useQuery } from "@tanstack/react-query";
import Autocomplete from "@mui/material/Autocomplete";
import TextField from "@mui/material/TextField";
import { AutocompleteInputChangeReason } from "@mui/material";
import httpClient from "@/shared/lib/httpClient";
import { FetchGamesProps, SearchProps } from "./props";
import { PagedList } from "@/shared/api";

const defaultDebounceTimeout = 200;

const fetchGames = async <T,>({
  searchValue,
  apiUrl,
  queryParam = "searchTerm",
  signal,
}: FetchGamesProps) => {
  const response = await httpClient.get<PagedList<T>>(
    `${apiUrl}?${queryParam}=${searchValue}`,
    {
      signal: signal,
    }
  );

  return (response.data as PagedList<T>) ?? null;
};

export const Search = <T,>({
  endpoint: apiUrl,
  getOptionLabel,
  getOptionKey,
  onEnterKeyDown,
  onOptionChanged,
  label,
  queryKey,
  queryParam,
  debounceTimeout,
}: SearchProps<T>) => {
  const [inputValue, setInputValue] = useState("");
  const [debouncedSearchValue, { isPending }] = useDebounce(
    inputValue,
    debounceTimeout ?? defaultDebounceTimeout
  );

  const { data, isFetching } = useQuery<PagedList<T>>({
    queryKey: [queryKey, debouncedSearchValue],
    queryFn: ({ signal }) =>
      fetchGames<T>({
        searchValue: debouncedSearchValue,
        apiUrl,
        queryParam,
        signal,
      }),
    enabled: debouncedSearchValue.length >= 1,
  });

  const onKeyDown = (key: string) => {
    if (onEnterKeyDown && key == "Enter") {
      onEnterKeyDown(inputValue);
    }
  };
  const onComboBoxInputChange = (value: T | null | string) => {
    if (typeof value !== "string" && value !== null && onOptionChanged) {
      onOptionChanged(value);
    } else {
      setInputValue(getOptionLabel(value ?? ""));
    }
  };

  const onInputValueChanged = (
    value: string,
    reason: AutocompleteInputChangeReason
  ) => {
    if (reason === "reset") {
      setInputValue("");
      return;
    } else {
      setInputValue(value);
    }
  };

  return (
    <Autocomplete
      selectOnFocus
      handleHomeEndKeys
      freeSolo
      options={data ? data.items : []}
      sx={{ width: 300 }}
      id="search"
      size="small"
      loading={isPending() || isFetching}
      inputValue={inputValue}
      getOptionLabel={(option) => getOptionLabel(option)}
      onKeyDown={({ key }) => onKeyDown(key)}
      onChange={(_, value) => onComboBoxInputChange(value)}
      onInputChange={(_, value, reason) => onInputValueChanged(value, reason)}
      renderOption={(props, option) => {
        return (
          <li {...props} key={getOptionKey(option)}>
            {getOptionLabel(option)}
          </li>
        );
      }}
      renderInput={(params) => {
        return <TextField {...params} label={label} />;
      }}
    />
  );
};
