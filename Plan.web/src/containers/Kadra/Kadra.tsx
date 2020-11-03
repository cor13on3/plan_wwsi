import React, { useEffect, useState } from "react";
import { Blad, httpClient } from "../../helpers/httpClient";
import { WykladowcaWidok } from "../../helpers/types";
import "./Kadra.css";
import KadraEdycja from "./KadraEdycja";

function Kadra() {
  const [lista, setLista] = useState([] as WykladowcaWidok[]);
  const [blad, setBlad] = useState("");
  const [czyEdycja, setCzyEdycja] = useState(false);
  const [edytowany, setEdytowany] = useState(null as number | null);

  useEffect(() => {
    odswiezListe();
  }, []);

  function odswiezListe() {
    httpClient
      .GET("/api/wykladowca")
      .then((res: WykladowcaWidok[]) => setLista(res))
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function onZapisz() {
    setCzyEdycja(false);
    setEdytowany(null);
    odswiezListe();
  }

  function onAnuluj() {
    setCzyEdycja(false);
    setEdytowany(null);
  }

  function usun(id: number) {
    httpClient
      .DELETE("/api/wykladowca/" + id)
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  return (
    <div>
      <h1>KADRA</h1>
      {blad && <p className="blad">{blad}</p>}
      <button onClick={() => setCzyEdycja(true)}>DODAJ</button>
      <div className="lista">
        {lista.map((x, i) => (
          <div className="wykladowca" key={i}>
            <span>{x.nazwa}</span>
            <span>{x.email}</span>
            <button
              onClick={() => {
                setEdytowany(x.id);
                setCzyEdycja(true);
              }}
            >
              EDYTUJ
            </button>
            <button onClick={() => usun(x.id)}>USUŃ</button>
          </div>
        ))}
      </div>
      {czyEdycja && (
        <KadraEdycja id={edytowany} onZapisz={onZapisz} onAnuluj={onAnuluj} />
      )}
    </div>
  );
}

export default Kadra;
