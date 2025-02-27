import styled from "styled-components";

export const LayoutWrapper = styled.div``;
export const LayoutContent = styled.div`
  width: 100%;
  color: ${({ theme }) => theme.text};
  background-color: ${({ theme }) => theme.body};
`;
