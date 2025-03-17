import { ReactNode } from "react";
import { AuthContext } from "../config/authContext";
import { useAuthProvieder } from "./useAuthProvider";

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const auth = useAuthProvieder();
  return <AuthContext.Provider value={auth}>{children}</AuthContext.Provider>;
};
