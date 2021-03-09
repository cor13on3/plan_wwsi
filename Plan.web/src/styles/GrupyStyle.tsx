import styled from "styled-components";

export const GrupyStyle = styled.div`
  max-width: 1200px;

  .grupy_header {
    max-width: 100%;
    display: grid;
    grid-template-columns: 1fr 128px;
    padding: 12px 16px;
    align-items: center;
  }

  button {
    height: 40px;
  }

  .lista {
    padding: 16px;
    height: 496px;
    overflow: auto;
  }

  .lista_header {
    padding: 8px 32px;
    display: grid;
    grid-template-columns: 128px 1fr 128px 1fr 86px;
  }

  .grupa {
    max-width: 100%;
    margin-bottom: 8px;
    background-color: white;
    padding: 8px 16px;
    display: grid;
    grid-template-columns: 128px 1fr 128px 1fr 64px;
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
