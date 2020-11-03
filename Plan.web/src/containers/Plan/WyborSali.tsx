import React, { useEffect, useState } from "react";
import { SalaWidok } from "../../helpers/enums";
import { httpClient } from "../../helpers/httpClient";

interface WyborSaliProps {
  onWybierz: (wybrany: SalaWidok) => void;
}

function WyborSali(props: WyborSaliProps) {
  const [lista, setLista] = useState([] as SalaWidok[]);
  const [nazwa, setNazwa] = useState("");

  function odswiezListe() {
    httpClient.GET("/api/sala").then((res: SalaWidok[]) => {
      setLista(res);
    });
  }

  useEffect(() => {
    odswiezListe();
  }, []);

  function onWybierz(wybrany: SalaWidok) {
    props.onWybierz(wybrany);
  }

  function dodaj() {
    httpClient.POST("/api/sala", { Nazwa: nazwa }).then(() => {
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

export default WyborSali;
