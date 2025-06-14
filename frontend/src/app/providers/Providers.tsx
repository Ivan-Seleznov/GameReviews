import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { AppThemeProvider } from "@/entities/theme/ui/AppThemeProvider";
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
        <AppThemeProvider>
          <AuthModalProvider>{children}</AuthModalProvider>
        </AppThemeProvider>
      </AuthProvider>
    </QueryClientProvider>
  );
};
