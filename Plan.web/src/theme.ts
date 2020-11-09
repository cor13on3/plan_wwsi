import { createMuiTheme } from "@material-ui/core";
import { blue, blueGrey } from "@material-ui/core/colors";

export const theme = createMuiTheme({
  palette: {
    primary: {
      main: blueGrey[500],
    },
    secondary: {
      main: blue[500],
    },
    error: { main: "#b00020" },
  },
});
