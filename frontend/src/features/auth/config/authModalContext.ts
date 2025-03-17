import { createContext } from "react";

export enum AuthModalType {
  Login,
  SignUp,
}

export interface AuthModalContextType {
  isOpen: boolean;
  selectedModalType: AuthModalType;
  setSelectedModalType: (newSelectedModalType: AuthModalType) => void;
  openModal: (selectedType?: AuthModalType) => void;
  closeModal: () => void;
}

export const AuthModalContext = createContext<AuthModalContextType | undefined>(
  undefined
);
