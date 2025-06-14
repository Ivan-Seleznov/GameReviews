import { Outlet, ScrollRestoration } from "react-router-dom";
import { Header } from "../../widgets/header";
import { Footer } from "../../widgets/footer";
import { LayoutContent, LayoutWrapper } from "./Layout.styled";
import { LoadingPage } from "@/pages/loading";
import { Suspense } from "react";

export const Layout = () => {
  return (
    <LayoutWrapper>
      <Header />
      <LayoutContent>
        <Suspense fallback={<LoadingPage />}>
          <Outlet />
        </Suspense>
      </LayoutContent>
      <Footer />
      <ScrollRestoration />
    </LayoutWrapper>
  );
};
