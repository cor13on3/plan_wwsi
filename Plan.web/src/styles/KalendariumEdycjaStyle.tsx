import styled from "styled-components";

const KalendariumEdycjaStyle = styled.div`
  width: 440px;
  display: grid;
  grid-template-rows: 48px 1fr;
  row-gap: 24px;
  padding: 32px 16px;

  form {
    display: grid;
    grid-template-rows: auto auto auto auto;
    row-gap: 32px;
  }

  .data {
    display: grid;
    grid-template-columns: 1fr 64px;
    align-items: center;
    column-gap: 16px;
  }

  .checkbox {
    padding: 0 8px 0 0;
  }

  .zapiszBtn {
    width: 128px;
    justify-self: end;
  }
`;

export default KalendariumEdycjaStyle;
