import { useContext } from "react";
import { AuthModalContext } from "../config/authModalContext";

export const useAuthModal = () => {
  const context = useContext(AuthModalContext);
  if (!context) {
    throw new Error("AuthModal must be used withing an AuthModalProvider");
  }

  return context;
};
