import { ReactNode, useState } from "react";
import { AuthModalContext, AuthModalType } from "../config/authModalContext";
import { AuthModal } from "../ui/AuthModal";

interface AuthModalProviderProps {
  children: ReactNode;
}

export const AuthModalProvider = ({ children }: AuthModalProviderProps) => {
  const [isOpen, setIsOpen] = useState(false);
  const [selectedModalType, setSelectedModalType] = useState<AuthModalType>(
    AuthModalType.Login
  );

  const openModal = (selectedType?: AuthModalType) => {
    if (selectedType !== undefined) {
      setSelectedModalType(selectedType);
    }
    setIsOpen(true);
  };
  const closeModal = () => setIsOpen(false);

  return (
    <AuthModalContext.Provider
      value={{
        isOpen,
        selectedModalType,
        openModal,
        closeModal,
        setSelectedModalType: setSelectedModalType,
      }}
    >
      {children}
      <AuthModal />
    </AuthModalContext.Provider>
  );
};
