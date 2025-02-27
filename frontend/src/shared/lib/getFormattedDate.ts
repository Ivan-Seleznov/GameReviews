export const getFormattedDate = (date: string) => {
  const dateObject = new Date(date);
  return dateObject.toDateString();
};
