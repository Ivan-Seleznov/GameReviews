import { ErrorBoundary } from "react-error-boundary";
import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Fallback } from "@/shared/ui";
import { ThemeProvider } from "@/entities/theme/ui/ThemeProvider";
import { GlobalStyles } from "../global/Global";

const queryClient = new QueryClient();

interface Providers {
  readonly children: JSX.Element;
}

export const Providers = ({ children }: Providers) => {
  return (
    <QueryClientProvider client={queryClient}>
      <GlobalStyles />
      <ThemeProvider>{children}</ThemeProvider>
    </QueryClientProvider>
  );
};
