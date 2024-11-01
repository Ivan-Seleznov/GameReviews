import styled from "styled-components";

export const FooterStyled = styled.div`
  width: 100%;
  background-color: #f0f0f0;
`;
export const FooterContentStyled = styled.div`
  display: flex;
  justify-content: space-between;
  padding: 20px 60px;
  flex-wrap: wrap;
  gap: 30px;
`;
export const FooterColStyled = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
`;
export const FooterHeaderStyled = styled.h2`
  margin: 0 0 10px 0;
  font-size: 1.5rem;
`;
export const FooterListStyled = styled.ul`
  list-style-type: none;
  padding: 0;
  margin: 0;
`;

export const FooterListItemStyled = styled.li`
  margin: 5px 0;
`;

export const FooterLinkStyled = styled.a`
  text-decoration: none;
  color: inherit;
  &:hover {
    text-decoration: underline;
  }
`;
