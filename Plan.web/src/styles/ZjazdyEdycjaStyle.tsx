import styled from "styled-components";

const ZjazdyEdycjaStyle = styled.div`
  width: 480px;
  display: grid;
  grid-template-rows: 48px auto 64px;
  row-gap: 24px;
  padding: 32px 16px;

  .element {
    display: grid;
    grid-template-columns: 36px 116px 32px 128px auto;
    align-items: center;

    button {
      justify-self: end;
    }
  }

  .element-dodaj {
    display: grid;
    grid-template-columns: 160px 32px 160px auto;
    align-items: baseline;

    button {
      justify-self: end;
    }
  }
`;

export default ZjazdyEdycjaStyle;
