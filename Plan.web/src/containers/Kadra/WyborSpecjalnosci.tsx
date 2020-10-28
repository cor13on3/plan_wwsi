import React, { useEffect, useState } from "react";
import { httpClient } from "../../helpers/httpClient";

export interface Specjalnosc {
  id: number;
  nazwa: string;
}

interface WyborSpecjalnosciProps {
  specjalnosci: Specjalnosc[];
  onWybrano: (lista: Specjalnosc[]) => void;
}

function WyborSpecjalnosci(props: WyborSpecjalnosciProps) {
  const [lista, setLista] = useState([] as Specjalnosc[]);
  const [wybrane, setWybrane] = useState(props.specjalnosci);
  const [nowa, setNowa] = useState("");

  function odswiezListe() {
    httpClient
      .GET("/api/specjalnosc")
      .then((res: Specjalnosc[]) => setLista(res));
  }

  useEffect(() => {
    odswiezListe();
  }, []);

  function zmienWybrane(element: Specjalnosc, checked: boolean) {
    const copy = [...wybrane];
    if (checked) {
      copy.push(element);
    } else {
      const i = copy.indexOf(element);
      copy.splice(i, 1);
    }
    setWybrane(copy);
  }

  function dodaj() {
    httpClient.POST("/api/specjalnosc", nowa).then(() => {
      setNowa("");
      odswiezListe();
    });
  }

  return (
    <div className="edycja-spec">
      {lista.map((x, i) => (
        <div key={i}>
          <input
            type="checkbox"
            onChange={(e) => zmienWybrane(x, e.target.checked)}
            checked={wybrane.some((y) => y.id === x.id)}
          />
          <span>{x.nazwa}</span>
        </div>
      ))}
      <div>
        <input value={nowa} onChange={(e) => setNowa(e.target.value)} />
        <button onClick={dodaj}>DODAJ</button>
      </div>
      <button onClick={() => props.onWybrano(wybrane)}>WYBIERZ</button>
    </div>
  );
}

export default WyborSpecjalnosci;
