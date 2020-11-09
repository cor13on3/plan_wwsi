import { Button, TextField } from "@material-ui/core";
import React, { useState } from "react";
import { connect } from "react-redux";
import { Blad, httpClient } from "../helpers/httpClient";
import "./Logowanie.css";
import { ErrorStyle } from "../styles/ErrorStyle";

interface LogowanieProps {
  onZalogowano: Function;
}

function Logowanie({ onZalogowano }: LogowanieProps) {
  const [email, setEmail] = useState("admin");
  const [haslo, setHaslo] = useState("Dupa1234!");
  const [blad, setBlad] = useState("");

  function zaloguj() {
    httpClient
      .POST("/api/uzytkownik/zaloguj", {
        Email: email,
        Haslo: haslo,
      })
      .then((res: any) => onZalogowano(res.email, res.imie, res.nazwisko))
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  return (
    <div className="logowanie">
      {blad ? <ErrorStyle>{blad}</ErrorStyle> : <div />}
      <div className="panel">
        <p className="xl">Zaloguj się</p>
        <TextField
          variant="outlined"
          color="secondary"
          label="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <TextField
          variant="outlined"
          color="secondary"
          label="Hasło"
          value={haslo}
          onChange={(e) => setHaslo(e.target.value)}
        />
        <Button variant="contained" color="secondary" onClick={zaloguj}>
          ZALOGUJ
        </Button>
      </div>
      <div />
    </div>
  );
}

function mapDispatchToProps(dispatch: Function) {
  return {
    onZalogowano: (email: string, imie: string, nazwisko: string) =>
      dispatch({ type: "ZALOGOWANO", payload: { email, imie, nazwisko } }),
  };
}

export default connect(null, mapDispatchToProps)(Logowanie);
