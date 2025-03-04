export const buildQueryString = <T extends object>(
  params: T,
  changeNameRecord?: Record<string, string>
): string => {
  const searchParams = new URLSearchParams();

  Object.entries(params).forEach(([key, value]) => {
    let keyToAdd = changeNameRecord?.[key] ?? key;

    if (Array.isArray(value)) {
      value.forEach((item) => {
        if (item !== null && item !== undefined && item !== "") {
          searchParams.append(keyToAdd, String(item));
        }
      });
    } else if (value !== null && value !== undefined && value !== "") {
      searchParams.append(keyToAdd, String(value));
    }
  });

  return searchParams.toString();
};
