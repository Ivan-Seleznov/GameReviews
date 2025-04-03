import {
  AuthUserDto,
  LoginUserCommand,
  RegisterUserCommand,
  UserDetailsDto,
} from "@/shared/api/types";
import { createContext } from "react";

export interface AuthContextType {
  user?: UserDetailsDto;
  loginUser: (loginUserCommand: LoginUserCommand) => Promise<AuthUserDto>;
  signUpUser: (
    registerUserCommand: RegisterUserCommand
  ) => Promise<AuthUserDto>;
  logout: () => void;
}
export const AuthContext = createContext<AuthContextType | undefined>(
  undefined
);
