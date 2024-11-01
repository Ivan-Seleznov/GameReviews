import {
  createBrowserRouter,
  RouterProvider,
  Navigate,
} from "react-router-dom";
import { Layout } from "../layout";
import { Fallback } from "../../shared/ui/fallback";
import { useTheme } from "../../entities/theme/lib/useTheme";
import { SearchGamesPage } from "@/pages/game/ui/searchGamesPage/searchGamesPage";
import { GamePage } from "@/pages/game/ui/gamePage/gamePage";

const router = createBrowserRouter([
  {
    element: <Layout />,
    errorElement: <Fallback />,
    children: [
      {
        path: "/",
        element: <Navigate to="/games" />,
      },
      {
        path: "/games",
        element: <h1>Games Page</h1>,
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
