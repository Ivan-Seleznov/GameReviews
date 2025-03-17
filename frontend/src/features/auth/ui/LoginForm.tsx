import { useAuth } from "@/entities/auth";
import { LoginUserCommand } from "@/shared/api/types";
import { Button, CircularProgress, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { useAuthModal } from "../lib/useAuthModal";
import { isApiError } from "@/shared/api";

export const LoginForm = () => {
  const { loginUser } = useAuth();
  const { closeModal } = useAuthModal();

  const [username, setUsername] = useState<string>("");
  const [password, setPassword] = useState<string>("");
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();

    if (!username || !password) {
      setError("Please fill in both fields.");
      return;
    }

    const loginUserCommand: LoginUserCommand = { username, password };

    try {
      setLoading(true);
      setError(null);

      await loginUser(loginUserCommand);
      closeModal();
    } catch (error) {
      let errorMessage = "An error occurred during login.";

      if (
        isApiError(error) &&
        error.response?.data.error.code === "Error.InvalidCredentials"
      ) {
        errorMessage = "Invalid Credentials";
      }

      setError(errorMessage);
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit} style={{ width: "100%" }}>
      <TextField
        label="Username"
        variant="outlined"
        fullWidth
        margin="normal"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
        autoComplete="username"
        required
      />
      <TextField
        label="Password"
        variant="outlined"
        type="password"
        fullWidth
        margin="normal"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
        autoComplete="current-password"
        required
      />

      {error && (
        <Typography variant="body2" color="error" sx={{ marginTop: 1 }}>
          {error}
        </Typography>
      )}

      <Button
        type="submit"
        variant="contained"
        color="primary"
        fullWidth
        disabled={loading}
        sx={{ marginTop: 2 }}
      >
        {loading ? <CircularProgress size={24} /> : "Login"}
      </Button>
    </form>
  );
};
