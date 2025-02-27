import { createBrowserRouter, RouterProvider } from "react-router-dom";
import { Layout } from "../layout";
import { Fallback } from "@/shared/ui";
import { GamesPage } from "@/pages/games/ui/GamesPage";
import { GamePage } from "@/pages/game";
import { SearchGamesPage } from "@/pages/search";
import { useTheme } from "styled-components";

const router = createBrowserRouter([
  {
    element: <Layout />,
    errorElement: <Fallback />,
    children: [
      {
        path: "/",
        element: <h1>Main Page</h1>,
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
        element: <SearchGamesPage></SearchGamesPage>,
      },
    ],
  },
]);
export const AppRouter = () => {
  const { theme } = useTheme();
  console.log("theme " + theme);

  return <RouterProvider router={router} />;
};
