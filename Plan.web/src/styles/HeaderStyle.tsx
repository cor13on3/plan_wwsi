import { withTheme } from "@material-ui/core/styles";
import styled from "styled-components";

export const HeaderStyle = withTheme(styled.header`
  background-color: ${(props) => props.theme.palette.primary.main};
  box-shadow: 0px 2px 4px -1px rgba(0, 0, 0, 0.2),
    0px 4px 5px 0px rgba(0, 0, 0, 0.14), 0px 1px 10px 0px rgba(0, 0, 0, 0.12);
  padding: 12px 32px;

  .zalogowany {
    display: grid;
    grid-template-columns: 1fr 94px;
    column-gap: 12px;
    justify-items: end;
    color: ${(props) => props.theme.palette.primary.contrastText};

    button {
      width: 94px;
    }
  }
`);
