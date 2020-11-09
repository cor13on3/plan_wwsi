import React from "react";
import ReactDOM from "react-dom";
import { Provider } from "react-redux";
import { applyMiddleware, combineReducers, createStore } from "redux";
import { composeWithDevTools } from "redux-devtools-extension";
import App from "./App";
import { PlanStore } from "./redux/store";
import uzytkownikReducer from "./redux/uzytkownik.reducer";
import * as serviceWorker from "./serviceWorker";
import "./index.css";
import "./styles.css";
import { MuiThemeProvider, ThemeProvider } from "@material-ui/core";
import { theme } from "./theme";

const init = {} as PlanStore;
const daneLocalStg = localStorage.getItem("uzytkownik");
if (daneLocalStg) init.uzytkownik = JSON.parse(daneLocalStg);

const store = createStore(
  combineReducers({ uzytkownik: uzytkownikReducer }),
  init,
  composeWithDevTools(applyMiddleware())
);

store.subscribe(() => {
  const state = store.getState();
  localStorage.setItem("uzytkownik", JSON.stringify(state.uzytkownik));
});

ReactDOM.render(
  <React.StrictMode>
    <Provider store={store}>
      <ThemeProvider theme={theme}>
        <MuiThemeProvider theme={theme}>
          <App />
        </MuiThemeProvider>
      </ThemeProvider>
    </Provider>
  </React.StrictMode>,
  document.getElementById("root")
);

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
