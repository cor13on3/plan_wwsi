import React, { useEffect, useState } from "react";
import { PrzedmiotWidok } from "../../helpers/enums";
import { httpClient } from "../../helpers/httpClient";

interface WyborPrzedmiotuProps {
  onWybierz: (wybrany: PrzedmiotWidok) => void;
}

function WyborPrzedmiotu(props: WyborPrzedmiotuProps) {
  const [lista, setLista] = useState([] as PrzedmiotWidok[]);
  const [nazwa, setNazwa] = useState("");

  function odswiezListe() {
    httpClient.GET("/api/przedmiot").then((res: PrzedmiotWidok[]) => {
      setLista(res);
    });
  }

  useEffect(() => {
    odswiezListe();
  }, []);

  function onWybierz(wybrany: PrzedmiotWidok) {
    props.onWybierz(wybrany);
  }

  function dodaj() {
    httpClient.POST("/api/przedmiot", nazwa).then(() => {
      odswiezListe();
    });
  }

  return (
    <div>
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

export default WyborPrzedmiotu;
