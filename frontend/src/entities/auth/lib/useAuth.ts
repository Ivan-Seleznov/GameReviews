import { useContext } from "react";
import { AuthContext } from "../config/authContext";

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error("Auth context must be used withing AuthContextProvider");
  }

  return context;
};
