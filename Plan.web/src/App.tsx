import { Button } from "@material-ui/core";
import React, { useEffect } from "react";
import { connect } from "react-redux";
import { BrowserRouter, Link, Redirect, Route, Switch } from "react-router-dom";
import Grupy from "./containers/Grupy/Grupy";
import Kadra from "./containers/Kadra/Kadra";
import Kalendarium from "./containers/Kalendarium/Kalendarium";
import Logowanie from "./containers/Logowanie";
import Plan from "./containers/Plan/Plan";
import Pulpit from "./containers/pulpit";
import { httpClient } from "./helpers/httpClient";
import { PlanStore } from "./redux/store";
import { HeaderStyle } from "./styles/HeaderStyle";
import { AppStyle } from "./styles/AppStyle";

interface AppProps {
  zalogowano: boolean;
  imie: string;
  nazwisko: string;
  onWyloguj: Function;
}

function App({ zalogowano, imie, nazwisko, onWyloguj }: AppProps) {
  useEffect(() => {
    httpClient
      .GET("/api/uzytkownik/czy-zalogowany")
      .then((res: boolean) => {
        if (!res) onWyloguj();
      })
      .catch(() => onWyloguj());
    // eslint-disable-next-line
  }, []);

  function wyloguj() {
    httpClient.POST("/api/uzytkownik/wyloguj", null).then(() => onWyloguj());
  }

  return (
    <BrowserRouter>
      <AppStyle logged={zalogowano}>
        <HeaderStyle>
          {zalogowano && (
            <div className="zalogowany">
              <p>
                Zalogowano jako {imie} {nazwisko}
              </p>
              <Button color="inherit" onClick={wyloguj}>
                WYLOGUJ
              </Button>
            </div>
          )}
        </HeaderStyle>
        <div className="window">
          {zalogowano && (
            <nav>
              <Link to="/plan">Plan zajęć</Link>
              <Link to="/kadra">Kadra</Link>
              <Link to="/kalendarium">Kalendarium</Link>
              <Link to="/grupy">Grupy</Link>
            </nav>
          )}
          <main>
            <Switch>
              {zalogowano ? (
                <>
                  <Route exact path="/pulpit">
                    <Pulpit />
                  </Route>
                  <Route exact path="/plan">
                    <Plan />
                  </Route>
                  <Route exact path="/kadra">
                    <Kadra />
                  </Route>
                  <Route exact path="/kalendarium">
                    <Kalendarium />
                  </Route>
                  <Route exact path="/grupy">
                    <Grupy />
                  </Route>
                </>
              ) : (
                <>
                  <Route exact path="/">
                    <Logowanie />
                  </Route>
                  <Redirect to="/" />
                </>
              )}
            </Switch>
          </main>
        </div>
      </AppStyle>
    </BrowserRouter>
  );
}

function mapStateToProps(store: PlanStore) {
  return {
    zalogowano: store.uzytkownik.zalogowano,
    imie: store.uzytkownik.imie,
    nazwisko: store.uzytkownik.nazwisko,
  };
}

function mapDispatchToProps(dispatch: Function) {
  return {
    onWyloguj: () => dispatch({ type: "WYLOGOWANO" }),
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(App);
