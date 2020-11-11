import styled from "styled-components";

const WyborPrzedmiotuStyle = styled.div`
  width: 480px;
  display: grid;
  grid-template-rows: 48px auto 64px;
  row-gap: 24px;
  padding: 32px 16px;

  .element {
    display: grid;
    grid-template-columns: 1fr auto;
    align-items: baseline;

    button {
      justify-self: end;
    }
  }

  .element-dodaj {
    display: grid;
    grid-template-columns: 1fr auto;
    align-items: baseline;
    column-gap: 32px;

    button {
      justify-self: end;
    }
  }
`;

export default WyborPrzedmiotuStyle;
