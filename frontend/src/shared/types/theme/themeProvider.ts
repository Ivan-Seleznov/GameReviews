export interface AppTheme {
  body: string;
  text: string;
}
declare module "styled-components" {
  // eslint-disable-next-line @typescript-eslint/no-empty-object-type
  export interface DefaultTheme extends AppTheme {}
}

export const lightTheme: AppTheme = {
  body: "#FFFFFF",
  text: "#000000",
};
export const darkTheme: AppTheme = {
  body: "#000000",
  text: "#FFFFFF",
};
