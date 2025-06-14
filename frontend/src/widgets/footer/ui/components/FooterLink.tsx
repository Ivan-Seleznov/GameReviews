import { Link } from "@mui/material";
import { Link as RouterLink } from "react-router-dom";

interface FooterLinkProps {
  children: React.ReactNode;
  href: string;
  external?: boolean;
}
export const FooterLink = ({
  href,
  children,
  external = true,
}: FooterLinkProps) => {
  return external ? (
    <Link underline="hover" color="inherit" href={href}>
      {children}
    </Link>
  ) : (
    <Link underline="hover" color="inherit" component={RouterLink} to={href}>
      {children}
    </Link>
  );
};
