import styled from "styled-components";

export const GamesPageWrapper = styled.div({
  padding: "0px 50px",
  width: "100%",
});
export const GamesPageContainer = styled.div`
  display: flex;
  flex-direction: row;
  align-items: flex-start;
  gap: 20px;
  width: 100%;
`;
export const GamesListContainer = styled.div`
  display: flex;
  flex-direction: column;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
  width: 100%;
  gap: 20px;
`;
export const GamesListInfo = styled.div({
  alignSelf: "flex-start",
});
