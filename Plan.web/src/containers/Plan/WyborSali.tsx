import React, { useEffect, useState } from "react";
import { SalaWidok } from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";

interface WyborSaliProps {
  onWybierz: (wybrany: SalaWidok) => void;
}

function WyborSali(props: WyborSaliProps) {
  const [lista, setLista] = useState([] as SalaWidok[]);
  const [nazwa, setNazwa] = useState("");
  const [blad, setBlad] = useState("");

  useEffect(() => {
    odswiezListe();
  }, []);

  function odswiezListe() {
    httpClient
      .GET("/api/sala")
      .then((res: SalaWidok[]) => {
        setLista(res);
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function dodaj() {
    httpClient
      .POST("/api/sala", { Nazwa: nazwa })
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function onWybierz(wybrany: SalaWidok) {
    props.onWybierz(wybrany);
  }

  return (
    <div>
      {blad && <p className="blad">{blad}</p>}
      {lista.map((x) => (
        <div>
          <span>{x.nazwa}</span>
          <button onClick={() => onWybierz(x)}>WYBIERZ</button>
        </div>
      ))}
      <input
        value={nazwa}
        onChange={(e) => setNazwa(e.target.value)}
        placeholder="Nazwa"
      />
      <button onClick={dodaj}>DODAJ</button>
    </div>
  );
}

export default WyborSali;
