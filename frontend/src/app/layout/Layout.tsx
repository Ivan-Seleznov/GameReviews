import { SkeletonTheme } from "react-loading-skeleton";
import { Outlet, ScrollRestoration } from "react-router-dom";
import { Header } from "../../widgets/header";
import { Footer } from "../../widgets/footer";
import { LayoutContent, LayoutWrapper } from "./Layout.styled";
import { LoadingPage } from "@/pages/loading";
import { Suspense } from "react";

export const Layout = () => {
  return (
    <SkeletonTheme baseColor="#313131" highlightColor="#525252">
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
    </SkeletonTheme>
  );
};
