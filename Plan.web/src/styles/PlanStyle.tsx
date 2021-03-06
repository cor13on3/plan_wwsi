import styled from "styled-components";

export const PlanStyle = styled.div`
  max-width: 1600px;

  .plan-header {
    max-width: 100%;
    padding: 12px 16px;
  }

  .plan_filters {
    padding: 4px 16px;
    display: grid;
    grid-template-columns: auto auto auto 1fr;
    column-gap: 12px;
  }

  .plan_filters_odpr {
    display: grid;
    grid-template-columns: 96px auto;
    column-gap: 12px;
    align-items: baseline;
  }

  .tydzien3 {
    display: grid;
    grid-template-columns: auto auto auto;
    column-gap: 16px;
    justify-content: left;
    padding: 16px;
  }

  .tydzien5 {
    display: grid;
    grid-template-columns: auto auto auto auto auto;
    column-gap: 16px;
    justify-content: left;
    padding: 16px;
  }

  .dzien {
    min-width: 196px;
    background-color: white;
    padding: 12px;
    display: grid;
    grid-template-rows: 24px 1fr 36px;
    box-shadow: 0px 1px 2px -1px rgba(0, 0, 0, 0.2),
      0px 2px 2px 0px rgba(0, 0, 0, 0.14), 0px 1px 5px 0px rgba(0, 0, 0, 0.12);
  }

  .dodajBtn {
    width: 48px;
    justify-self: end;
    margin-top: 8px;
  }

  .lekcja {
    display: grid;
    grid-template-rows: auto auto auto auto auto;
    margin-top: 8px;
  }

  .lekcja-sala {
    display: grid;
    grid-template-columns: 1fr 36px;
    align-items: center;
    height: 24px;

    button {
      padding: 0;
    }
  }
`;
