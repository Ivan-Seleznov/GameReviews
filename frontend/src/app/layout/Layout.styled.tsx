import styled from "styled-components";

export const LayoutWrapper = styled.div({
  display: "flex",
  flexDirection: "column",
  minHeight: "100vh",
});

export const LayoutContent = styled.div(({ theme }) => ({
  flex: 1,
  width: "100%",
  color: theme.text,
  backgroundColor: theme.body,

  display: "flex",
  alignItems: "flex-start",
  justifyContent: "flex-start",

  "& > *": {
    width: "100%",
  },
}));
