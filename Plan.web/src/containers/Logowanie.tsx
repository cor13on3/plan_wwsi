import React, { useState } from "react";
import { connect } from "react-redux";
import { Blad, httpClient } from "../helpers/httpClient";
import "./Logowanie.css";

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
      <p className="error">{blad && blad}</p>
      <div className="controls">
        <input
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          type="text"
        />
        <input
          placeholder="Hasło"
          value={haslo}
          onChange={(e) => setHaslo(e.target.value)}
          type="password"
        />
        <button onClick={zaloguj}>ZALOGUJ</button>
      </div>
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
