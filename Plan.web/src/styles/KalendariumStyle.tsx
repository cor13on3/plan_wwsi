import styled from "styled-components";

export const KalendariumStyle = styled.div`
  max-width: 1200px;

  .kalendarium_header {
    max-width: 100%;
    display: grid;
    grid-template-columns: 1fr 156px;
    padding: 12px 16px;
    align-items: center;
  }

  .kalendarium_main {
    display: grid;
    grid-template-rows: auto 1fr;
  }

  .filters {
    padding: 12px 16px;
    display: grid;
    grid-template-columns: auto auto auto 1fr;
    column-gap: 12px;
  }

  .zjazd_lista_header {
    display: grid;
    grid-template-columns: 160px 1fr;
    align-items: center;
    padding: 12px 16px;
  }

  .zjazd_lista {
    padding: 16px;
  }

  .zjazd {
    max-width: 100%;
    margin-bottom: 8px;
    background-color: white;
    padding: 8px 16px;
    display: grid;
    grid-template-columns: 160px 128px 32px 1fr 64px;
    align-items: center;
    box-shadow: 0px 1px 2px -1px rgba(0, 0, 0, 0.2),
      0px 2px 2px 0px rgba(0, 0, 0, 0.14), 0px 1px 5px 0px rgba(0, 0, 0, 0.12);
  }
`;
