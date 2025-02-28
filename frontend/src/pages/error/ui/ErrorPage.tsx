import { useRouteError } from "react-router-dom";
import { ErrorPageContainer } from "./ErrorPage.styled.";

export const ErrorPage = () => {
  const error = useRouteError() as Error;

  return (
    <ErrorPageContainer>
      <h1>An error occurred</h1>
      <h2>Name: {error.name}</h2>
      <p>Message: {error.message}</p>
      {error.stack && <p>{error.stack}</p>}
    </ErrorPageContainer>
  );
};
