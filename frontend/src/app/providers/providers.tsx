import { FC } from "react";
import { ErrorBoundary } from "react-error-boundary";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Fallback } from "@/shared/ui/fallback";
import { ThemeProvider } from "@/entities/theme/ui/themeProvider";
import { GlobalStyles } from "../global/global";

const queryClient = new QueryClient();

interface IProviders {
  readonly children: JSX.Element;
}

export const Providers: FC<IProviders> = ({ children }) => {
  return (
    <ErrorBoundary FallbackComponent={Fallback}>
      <QueryClientProvider client={queryClient}>
        <GlobalStyles />
        <ThemeProvider>{children}</ThemeProvider>
      </QueryClientProvider>
    </ErrorBoundary>
  );
};
