import { useAuth } from "@/entities/auth";
import { AuthModalType } from "@/features/auth/config/authModalContext";
import { useAuthModal } from "@/features/auth/lib/useAuthModal";
import { Box, Button } from "@mui/material";

export const AuthButtons = () => {
  const { user } = useAuth();
  const { openModal } = useAuthModal();

  const handleLoginClick = () => {
    openModal(AuthModalType.Login);
  };
  const handleSignUpClick = () => {
    openModal(AuthModalType.SignUp);
  };

  return (
    <Box>
      {user ? (
        <Button>Account</Button>
      ) : (
        <>
          <Button onClick={handleLoginClick}>Login</Button>
          <Button onClick={handleSignUpClick}>Sign Up</Button>
        </>
      )}
    </Box>
  );
};
