import styled from "styled-components";
export const GameInfoWrapper = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 20px;
  align-items: center;
  width: 100%;
`;

export const GameInfoContent = styled.div`
  display: flex;
  flex-direction: column;
  height: 100%;
  gap: 20px;
`;
export const GamePlatforms = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  gap: 20px;
`;
export const GamePlatform = styled.img`
  display: block;
  height: 20px;
`;
export const GameInfoImageContainer = styled.div`
  display: flex;

  img {
    height: 100%;
    width: 100%;
    border-radius: 4px;
  }
`;
export const GameInfoTextContainer = styled.div`
  display: flex;
  flex-direction: column;
  gap: 5px;
`;
