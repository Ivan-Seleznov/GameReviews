import { useAuth } from "@/entities/auth";
import { AddReviewProps } from "./addReviewProps";
import { useState } from "react";
import { Alert, Box, Button, Link, TextField, Typography } from "@mui/material";
import { useAuthModal } from "@/features/auth/lib/useAuthModal";
import { AuthModalType } from "@/features/auth/config/authModalContext";

const AddReview = ({ gameId }: AddReviewProps) => {
  const { user } = useAuth();
  const { openModal } = useAuthModal();

  const [title, setTitle] = useState<string>("");
  const [content, setContent] = useState<string>("");
  const [error, setError] = useState<string | null>(null);

  return (
    <Box
      sx={{
        display: "flex",
        flexDirection: "column",
        maxWidth: "600px",
        gap: "2px",
      }}
    >
      {user ? (
        <h1>Add review</h1>
      ) : (
        <Typography sx={{ whiteSpace: "pre-wrap" }}>
          Please{" "}
          <Link
            sx={{ fontWeight: "bold" }}
            onClick={() => openModal(AuthModalType.Login)}
            underline="hover"
          >
            log in
          </Link>{" "}
          to add a review.
        </Typography>
      )}
    </Box>
  );
};

export default AddReview;
