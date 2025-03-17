import { z } from "zod";

const usernameSchema = z
  .string()
  .min(5, "Username must be at least 5 characters long")
  .max(12, "Username must be no more than 12 characters long");

const emailSchema = z.string().email({ message: "Invalid email address" });

const passwordSchema = z
  .string()
  .min(8, "Password must be at least 8 characters.")
  .max(48, "Password must be at most 48 characters.")
  .regex(/[A-Za-z]/, "Password must contain at least one letter.")
  .regex(/\d/, "Password must contain at least one number.")
  .regex(/[\W]/, "Password must contain at least one special character.");

export const signUpFormSchema = z.object({
  username: usernameSchema,
  email: emailSchema,
  password: passwordSchema,
});

export type SignUpFormData = z.infer<typeof signUpFormSchema>;
