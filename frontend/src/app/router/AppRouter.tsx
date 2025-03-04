import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { Layout } from "../layout";
//import { GamesPage } from "@/pages/games/ui/GamesPage";
//import { GamePage } from "@/pages/game";
//import { SearchGamesPage } from "@/pages/search";
import { useTheme } from "styled-components";
import { ErrorPage } from "@/pages/error";
import { lazy } from "react";

const GamesPage = lazy(() =>
  import("@/pages/games").then((mod) => ({
    default: mod.GamesPage,
  }))
);

const SearchGamesPage = lazy(() =>
  import("@/pages/search").then((mod) => ({
    default: mod.SearchGamesPage,
  }))
);

const GamePage = lazy(() =>
  import("@/pages/game").then((mod) => ({
    default: mod.GamePage,
  }))
);

const router = createBrowserRouter([
  {
    element: <Layout />,
    errorElement: <ErrorPage />,
    children: [
      {
        path: "/",
        element: <h1>Home page</h1>,
      },
      {
        path: "/games",
        element: <GamesPage />,
      },
      {
        path: "/game",
        element: <GamePage />,
      },
      {
        path: "/reviews",
        element: <h1>Reviews page</h1>,
      },
      {
        path: "search/games/",
        element: <SearchGamesPage />,
      },
    ],
  },
]);
export const AppRouter = () => {
  const { theme } = useTheme();
  console.log("theme " + theme);

  return <RouterProvider router={router} />;
};
