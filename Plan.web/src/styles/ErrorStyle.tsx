import { withTheme } from "@material-ui/core/styles";
import styled from "styled-components";

export const ErrorStyle = withTheme(styled.div`
  background-color: rgba(176, 0, 20, 0.1);
  padding: 12px 16px;
  color: ${(props) => props.theme.palette.error.main};
  align-content: center;
  display: grid;
  border: 1px solid ${(props) => props.theme.palette.error.main};
  border-radius: 3px;
`);
