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
