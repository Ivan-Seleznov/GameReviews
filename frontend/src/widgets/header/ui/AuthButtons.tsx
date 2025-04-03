import { useAuth } from "@/entities/auth";
import { AuthModalType } from "@/features/auth/config/authModalContext";
import { useAuthModal } from "@/features/auth/lib/useAuthModal";
import { Box, Button } from "@mui/material";
import { useNavigate } from "react-router-dom";

export const AuthButtons = () => {
  const navigate = useNavigate();

  const { user, logout } = useAuth();
  const { openModal } = useAuthModal();

  const handleProfileClick = () => {
    navigate("/profile");
  };

  const handleLoginClick = () => {
    openModal(AuthModalType.Login);
  };
  const handleSignUpClick = () => {
    openModal(AuthModalType.SignUp);
  };
  const handleLogoutClick = () => {
    logout();
    navigate("/");
  };

  return (
    <Box>
      {user ? (
        <>
          <Button onClick={handleProfileClick}>Profile</Button>
          <Button onClick={handleLogoutClick}>Logout</Button>
        </>
      ) : (
        <>
          <Button onClick={handleLoginClick}>Login</Button>
          <Button onClick={handleSignUpClick}>Sign Up</Button>
        </>
      )}
    </Box>
  );
};
