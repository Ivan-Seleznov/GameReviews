import styled from "styled-components";

export const GamePageWrapper = styled.div`
  display: flex;
  flex-direction: column;
  padding: 0px 50px;
  gap: 20px;
`;

export const GameContentWrapper = styled.div`
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 20px;
  align-items: center;
  width: 100%;
`;

export const GameContent = styled.div`
  display: flex;
  flex-direction: column;
  height: 100%;
  gap: 20px;
`;
export const PlatformsContainer = styled.div`
  display: flex;
  flex-direction: row;
  flex-wrap: wrap;
  gap: 20px;
`;
export const PlatformImage = styled.img`
  display: block;
  height: 20px;
`;
export const GameImagesContainer = styled.div`
  display: flex;

  img {
    height: 100%;
    width: 100%;
    border-radius: 4px;
  }
`;
export const InformationSection = styled.div`
  display: flex;
  flex-direction: column;
  gap: 5px;
`;
