import { withTheme } from "@material-ui/core/styles";
import styled from "styled-components";

export const ErrorStyle = withTheme(styled.div`
  background-color: ${(props) => props.theme.palette.error.main};
  padding: 4px 32px;
  color: ${(props) => props.theme.palette.error.contrastText};
  align-content: center;
  display: grid;
  border-radius: 3px;
`);
