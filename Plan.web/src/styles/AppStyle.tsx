import { withTheme } from "@material-ui/core/styles";
import styled from "styled-components";

export const AppStyle = withTheme(styled.div<any>`
  height: 100vh;
  background-color: rgb(228, 228, 232);
  display: grid;
  grid-template-rows: 64px 1fr;

  .window {
    display: grid;
    grid-template-columns: ${(props) => (props.logged ? "220px 1fr" : "1fr")};
    column-gap: 72px;

    nav {
      background-color: ${(props) => props.theme.palette.primary.main};
      width: 100%;
      margin: 16px;
      padding: 64px 16px;
      box-shadow: 0px 2px 4px -1px rgba(0, 0, 0, 0.2),
        0px 4px 5px 0px rgba(0, 0, 0, 0.14),
        0px 1px 10px 0px rgba(0, 0, 0, 0.12);

      a {
        color: ${(props) => props.theme.palette.primary.contrastText};
        text-decoration: none;
        font-size: 20px;
      }

      display: grid;
      row-gap: 20px;
      grid-template-rows: auto auto auto auto 1fr;
    }

    main {
      display: grid;
      height: 100%;
    }
  }
`);
