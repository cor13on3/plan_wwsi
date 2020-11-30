import styled from "styled-components";

const WyborLekcjiStyle = styled.div`
  width: 480px;
  display: grid;
  grid-template-rows: 48px auto;
  row-gap: 24px;
  padding: 32px 16px;
  background-color: rgb(228, 228, 232);
  height: 100%;

  .element {
    display: grid;
    grid-template-columns: 1fr 64px;
    margin-top: 12px;
    background-color: white;
    padding: 4px;

    .element_opis {
      display: grid;
      grid-template-rows: auto auto auto auto;
    }
  }
`;

export default WyborLekcjiStyle;
