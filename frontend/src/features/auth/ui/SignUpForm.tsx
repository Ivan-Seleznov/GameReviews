import { useAuth } from "@/entities/auth";
import { RegisterUserCommand } from "@/shared/api/types";
import { Button, CircularProgress, TextField, Typography } from "@mui/material";
import { useState } from "react";
import { useAuthModal } from "../lib/useAuthModal";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { signUpFormSchema, SignUpFormData } from "../config/formData";
import { isValidationError } from "@/shared/api";

export const SignUpForm = () => {
  const { signUpUser } = useAuth();
  const { closeModal } = useAuthModal();

  const [loading, setLoading] = useState<boolean>(false);

  const {
    control,
    handleSubmit,
    setError,
    formState: { errors },
  } = useForm<SignUpFormData>({
    resolver: zodResolver(signUpFormSchema),
    defaultValues: {
      username: "",
      email: "",
      password: "",
    },
    mode: "onChange",
  });

  const onSubmit: SubmitHandler<SignUpFormData> = async (data) => {
    console.log(data);

    const registerUserCommand: RegisterUserCommand = {
      username: data.username,
      email: data.email,
      password: data.password,
    };

    try {
      setLoading(true);
      await signUpUser(registerUserCommand);
      closeModal();
    } catch (error) {
      if (
        !isValidationError(error) ||
        !error.response ||
        Object.keys(error.response.data.errors).length === 0
      ) {
        setError("root", { type: "server", message: "Something went wrong" });
        return;
      }

      Object.entries(error.response.data.errors).forEach(([key, messages]) => {
        const errorMessage = Array.isArray(messages)
          ? messages.join(". ")
          : String(messages);

        setError(key.toLowerCase() as keyof SignUpFormData, {
          type: "server",
          message: errorMessage,
        });
      });
    } finally {
      setLoading(false);
    }
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} style={{ width: "100%" }}>
      <Controller
        name="username"
        control={control}
        render={({ field }) => (
          <TextField
            label="Username"
            required
            fullWidth
            margin="normal"
            onChange={(e) => field.onChange(e.target.value)}
            error={!!errors.username}
            helperText={errors.username?.message}
          />
        )}
      />
      <Controller
        name="email"
        control={control}
        render={({ field }) => (
          <TextField
            label="Email"
            type="email"
            required
            fullWidth
            margin="normal"
            onChange={(e) => field.onChange(e.target.value)}
            error={!!errors.email}
            helperText={errors.email?.message}
          />
        )}
      />
      <Controller
        name="password"
        control={control}
        render={({ field }) => (
          <TextField
            label="Password"
            type="password"
            required
            fullWidth
            margin="normal"
            onChange={(e) => field.onChange(e.target.value)}
            error={!!errors.password}
            helperText={errors.password?.message}
          />
        )}
      />
      {errors.root?.message && (
        <Typography variant="body2" color="error" sx={{ marginTop: 1 }}>
          {errors.root.message}
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
        {loading ? <CircularProgress size={24} /> : "Register"}
      </Button>
    </form>
  );
};
