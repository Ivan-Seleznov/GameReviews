import styled from "styled-components";
import { CardListWrapperProps } from "./props";

export const CardsListWrapper = styled.div<CardListWrapperProps>`
  ${({ type = "grid" }) =>
    type === "grid"
      ? `
    display: grid;
    grid-template-columns: repeat(auto-fit, 345px);
    gap: 20px;`
      : `
    display:flex;
    flex-direction:column;
    justify-content:flex-start;
    gap: 20px;`}

  width: 100%;
  justify-content: center;
`;
