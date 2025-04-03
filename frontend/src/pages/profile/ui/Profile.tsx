import { useAuth } from "@/entities/auth";
import { UserDetailsDto } from "@/shared/api";
import httpClient from "@/shared/api/httpClient";

import { Box, Skeleton, Typography } from "@mui/material";
import { useQuery } from "@tanstack/react-query";

const fetchUser = async () => {
  const { data } = await httpClient.get<UserDetailsDto>("/users/me");
  return data;
};

export const ProfilePage = () => {
  const { user } = useAuth();

  const {
    data: detailedUser,
    isLoading,
    isError,
  } = useQuery<UserDetailsDto>({
    queryFn: fetchUser,
    queryKey: ["user", user?.id],
  });

  if (isError) {
    return <Typography variant="h6">Error loading user data</Typography>;
  }

  if (isLoading || !detailedUser) {
    return (
      <Box sx={{ padding: 2 }}>
        <Skeleton variant="text" width={200} height={30} />
        <Skeleton
          variant="text"
          width={300}
          height={30}
          sx={{ marginTop: 1 }}
        />
        <Skeleton
          variant="text"
          width={250}
          height={30}
          sx={{ marginTop: 1 }}
        />
      </Box>
    );
  }

  return (
    <Box sx={{ padding: 3 }}>
      <Typography variant="h4" gutterBottom>
        Profile Information
      </Typography>
      <Typography variant="body1">
        <strong>ID:</strong> {detailedUser.id}
      </Typography>
      <Typography variant="body1">
        <strong>Username:</strong> {detailedUser.username}
      </Typography>
      <Typography variant="body1">
        <strong>Email:</strong> {detailedUser.email}
      </Typography>
    </Box>
  );
};
