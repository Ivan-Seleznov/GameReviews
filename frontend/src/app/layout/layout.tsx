import { SkeletonTheme } from "react-loading-skeleton";
import { Outlet, ScrollRestoration } from "react-router-dom";
import { Header } from "../../widgets/header";
import { Footer } from "../../widgets/footer";
import { LayoutContent, LayoutWrapper } from "./layout.styled";

export const Layout = () => {
  return (
    <LayoutWrapper>
      <SkeletonTheme baseColor="#313131" highlightColor="#525252">
        <Header />
        <LayoutContent>
          <Outlet />
        </LayoutContent>
        <Footer />
        <ScrollRestoration />
      </SkeletonTheme>
    </LayoutWrapper>
  );
};
