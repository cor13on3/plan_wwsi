import styled from "styled-components";

export const KadraEdycjaStyle = styled.div`
  width: 440px;
  display: grid;
  grid-template-rows: 48px 1fr;
  row-gap: 24px;
  padding: 32px 16px;

  form {
    display: grid;
    grid-template-rows: auto auto auto auto auto auto 1fr;
    row-gap: 32px;
  }

  .buttons {
    display: grid;
    grid-template-columns: auto auto;
    column-gap: 16px;
    justify-content: end;
  }
`;
