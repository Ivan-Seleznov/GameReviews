import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { ThemeProvider } from "@/entities/theme/ui/ThemeProvider";
import { GlobalStyles } from "../global/Global";
import { AuthProvider } from "@/entities/auth/lib/AuthProvider";
import { AuthModalProvider } from "@/features/auth/lib/AuthModalProvider";

const queryClient = new QueryClient();

interface Providers {
  readonly children: JSX.Element;
}

export const Providers = ({ children }: Providers) => {
  return (
    <QueryClientProvider client={queryClient}>
      <GlobalStyles />
      <AuthProvider>
        <AuthModalProvider>
          <ThemeProvider>{children}</ThemeProvider>
        </AuthModalProvider>
      </AuthProvider>
    </QueryClientProvider>
  );
};
