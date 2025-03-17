export interface GameDetailsDto {
  id: number;
  name: string;
  description: string;
  category?: string;
  gammeStatus?: string;
  releaseDate?: string;
  cover?: ImageDto;
  screenshots?: ImageDto[];
}
export interface GameInfoDto {
  id: number;
  name: string;
  description: string;
  cover?: ImageDto;
}
export interface PagedList<T> {
  items: T[];
  page: number;
  pageSize: number;
  totalCount: number;
  hasNextPage: boolean;
  hasPreviousPage: boolean;
}
interface ImageDto {
  url: string;
  width?: number;
  height?: number;
}
interface EntityFilter {
  page?: number;
  pageSize?: number;
  sortColumn?: string;
  sortOrder?: string;
}
export interface GamesFilter extends EntityFilter {
  category?: string;
  startYear?: string;
  endYear?: string;
  genres?: string[];
  platforms?: string[];
}

export interface GamesFilter extends EntityFilter {
  Type?: string;
  Status?: string;
  StartYear?: string;
  EndYear?: string;
}
export interface GenreDto {
  id: number;
  name: string;
}
export interface PlatformDto {
  id: number;
  name: string;
  description: string;
  ImageDto?: ImageDto;
}
export interface UserDetailsDto {
  id: number;
  username: string;
  email: string;
}
export interface AuthUserDto {
  user: UserDetailsDto;
  accessToken: string;
  refreshToken: string;
}
export interface LoginUserCommand {
  username: string;
  password: string;
}
export interface RegisterUserCommand {
  username: string;
  email: string;
  password: string;
}
export type ValidationErrors = {
  type: string;
  title: string;
  status: number;
  errors: Record<string, string[]>;
};
export type ApiError = {
  type: string;
  title: string;
  status: number;
  error: {
    code: string;
    message: string;
  };
};

//TODO: Add auto-generated types
