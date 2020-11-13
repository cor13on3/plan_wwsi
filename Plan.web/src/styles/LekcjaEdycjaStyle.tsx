import styled from "styled-components";

const LekcjaEdycjaStyle = styled.div`
  width: 440px;
  display: grid;
  grid-template-rows: 48px 1fr;
  row-gap: 24px;
  padding: 32px 16px;

  form {
    display: grid;
    grid-template-rows: auto auto auto auto auto auto 1fr;
    row-gap: 32px;

    .zapiszBtn {
      width: 128px;
      justify-self: end;
    }
  }
`;

export default LekcjaEdycjaStyle;