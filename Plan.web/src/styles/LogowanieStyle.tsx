import styled from "styled-components";

const LogowanieStyle = styled.div`
  align-self: center;
  justify-self: center;
  display: grid;
  grid-template-rows: 48px 1fr 48px;
  row-gap: 16px;

  .panel {
    width: 400px;
    background-color: #fff;
    padding: 24px 32px;
    border-radius: 3px;
    box-shadow: rgba(0, 0, 0, 0.1) 1px 2px 3px 0px;
    display: grid;
    grid-template-rows: auto auto auto auto 1fr;
    row-gap: 24px;
  }

  button {
    width: 128px;
    justify-self: end;
  }
`;

export default LogowanieStyle;
