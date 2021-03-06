import styled from "styled-components";

export const KadraStyle = styled.div`
  max-width: 1200px;

  .kadra_header {
    max-width: 100%;
    display: grid;
    grid-template-columns: 1fr 128px;
    padding: 12px 16px;
    align-items: center;
  }

  .lista {
    padding: 16px;
    height: 496px;
    overflow: auto;
  }

  .kadra_lista_header {
    padding: 8px 32px;
    display: grid;
    grid-template-columns: 512px 1fr;
  }

  .wykladowca {
    max-width: 100%;
    margin-bottom: 8px;
    background-color: white;
    padding: 8px 16px;
    display: grid;
    grid-template-columns: 512px 1fr 64px;
    align-items: center;
    box-shadow: 0px 1px 2px -1px rgba(0, 0, 0, 0.2),
      0px 2px 2px 0px rgba(0, 0, 0, 0.14), 0px 1px 5px 0px rgba(0, 0, 0, 0.12);
  }

  .szukajka {
    padding: 4px 16px;
    display: grid;
    max-width: 100%;
  }

  .blad {
    color: red;
  }
`;
