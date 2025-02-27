import styled from "styled-components";
import { CardListWrapperProps } from "./CardsList.props";

export const CardsListWrapper = styled.div<CardListWrapperProps>`
  ${({ type = "grid" }) =>
    type === "grid"
      ? `
    display: grid;
    grid-template-columns: repeat(auto-fit, 345px);
    gap: 20px;
    justify-content: start;`
      : `
    display:flex;
    flex-direction:column;
    justify-content:flex-start;
    gap: 20px;
    justify-content: center;`}

  width: 100%;
`;
