import {
  Box,
  IconButton,
  Modal,
  Paper,
  ToggleButton,
  ToggleButtonGroup,
} from "@mui/material";
import { LoginForm } from "./LoginForm";
import { useAuthModal } from "../lib/useAuthModal";
import { AuthModalType } from "../config/authModalContext";
import CloseIcon from "@mui/icons-material/Close";
import { SignUpForm } from "./SignUpForm";

export const AuthModal = () => {
  const {
    isOpen,
    closeModal,
    selectedModalType: openedModalType,
    setSelectedModalType: setOpenedModalType,
  } = useAuthModal();

  const handleNewFormSelected = (
    _: React.MouseEvent<HTMLElement>,
    newModalType: AuthModalType | null
  ) => {
    if (newModalType !== null && newModalType !== openedModalType) {
      setOpenedModalType(newModalType);
    }
  };

  return (
    <Modal
      open={isOpen}
      onClose={() => {
        closeModal();
      }}
    >
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          height: "100vh",
          backgroundColor: "rgba(0, 0, 0, 0.5)",
        }}
      >
        <Paper
          sx={{
            width: 400,
            padding: 4,
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            borderRadius: 2,
            boxShadow: 3,
            position: "relative",
          }}
        >
          <IconButton
            aria-label="close"
            sx={{ position: "absolute", top: "0", right: "0" }}
            onClick={closeModal}
          >
            <CloseIcon />
          </IconButton>
          <ToggleButtonGroup
            color="primary"
            value={openedModalType}
            exclusive
            onChange={handleNewFormSelected}
            aria-label="Platform"
          >
            <ToggleButton value={AuthModalType.Login}>Login</ToggleButton>
            <ToggleButton value={AuthModalType.SignUp}>Sign up</ToggleButton>
          </ToggleButtonGroup>
          {openedModalType === AuthModalType.Login ? (
            <LoginForm />
          ) : (
            <SignUpForm />
          )}
        </Paper>
      </Box>
    </Modal>
  );
};
