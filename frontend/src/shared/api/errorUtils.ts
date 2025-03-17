import axios, { AxiosError } from "axios";
import { ApiError, ValidationErrors } from "./types";

export const isValidationError = (
  error: unknown
): error is AxiosError<ValidationErrors> => {
  return (
    axios.isAxiosError(error) &&
    error.response?.status === 400 &&
    error.response?.data?.errors !== undefined
  );
};

export const isApiError = (error: unknown): error is AxiosError<ApiError> => {
  return axios.isAxiosError(error) && error.response?.data?.error !== undefined;
};
