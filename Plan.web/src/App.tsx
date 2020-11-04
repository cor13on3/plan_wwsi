import React, { useEffect } from "react";
import { connect } from "react-redux";
import { BrowserRouter, Link, Redirect, Route, Switch } from "react-router-dom";
import "./App.css";
import Grupy from "./containers/Grupy";
import Kadra from "./containers/Kadra/Kadra";
import Kalendarium from "./containers/Kalendarium/Kalendarium";
import Logowanie from "./containers/Logowanie";
import Plan from "./containers/Plan/Plan";
import Pulpit from "./containers/pulpit";
import { httpClient } from "./helpers/httpClient";
import { PlanStore } from "./redux/store";

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
      <div className="window">
        {zalogowano ? (
          <header>
            <div>
              Zalogowano jako {imie} {nazwisko}
            </div>
            <button onClick={wyloguj}>WYLOGUJ</button>
            <Link to="/plan">Zarządzanie planem zajęć</Link>
            <Link to="/kadra">Zarządzanie kadrą</Link>
            <Link to="/kalendarium">Zarządzanie kalendarium</Link>
            <Link to="/grupy">Zarządzanie grupami</Link>
          </header>
        ) : (
          <header>
            <div>Zaloguj się</div>
          </header>
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
                <Redirect to="/pulpit" />
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
