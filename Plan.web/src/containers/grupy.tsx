import React, { useEffect, useState } from "react";
import { Blad, httpClient } from "../helpers/httpClient";
import "./Grupy.css";

interface GrupaWidok {
  numer: string;
}

enum TrybStudiow {
  Stacjonarne,
  Niestacjonarne,
}

enum StopienStudiow {
  Inzynierskie,
  Magisterskie,
  Podyplomowe,
}

interface Grupa {
  nrGrupy: string;
  semestr: number;
  trybStudiow: TrybStudiow;
  stopienStudiow: StopienStudiow;
}

function Grupy() {
  const [lista, setLista] = useState([] as GrupaWidok[]);
  const [blad, setBlad] = useState("");
  const [czyEdycja, setCzyEdycja] = useState(false);
  const [grupa, setGrupa] = useState({
    trybStudiow: TrybStudiow.Niestacjonarne,
    stopienStudiow: StopienStudiow.Inzynierskie,
  } as Grupa);

  function odswiezListe() {
    httpClient
      .GET("/api/grupa")
      .then((res: any) => setLista(res))
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  useEffect(() => {
    odswiezListe();
  }, []);

  function pokazEdycje(numer?: string) {
    setCzyEdycja(true);
  }

  function zapisz() {
    if (czyEdycja)
      httpClient
        .POST("/api/grupa", {
          NrGrupy: grupa.nrGrupy,
          Semestr: grupa.semestr,
          TrybStudiow: grupa.trybStudiow,
          StopienStudiow: grupa.stopienStudiow,
        })
        .then(() => {
          setCzyEdycja(false);
          setGrupa({} as Grupa);
          odswiezListe();
        })
        .catch((err: Blad) => setBlad(err.Tresc));
  }

  function usun(numer: string) {
    httpClient
      .DELETE(`/api/grupa/${numer}`)
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  return (
    <div className="grupy">
      <h1>GRUPY</h1>
      {blad && <p className="blad">{blad}</p>}
      <button onClick={() => pokazEdycje()}>DODAJ</button>
      <div className="lista">
        {lista.map((grupa) => (
          <div className="grupa">
            <span>{grupa.numer}</span>
            <button onClick={() => usun(grupa.numer)}>Usuń</button>
          </div>
        ))}
      </div>
      {czyEdycja && (
        <div className="edycja">
          <input
            placeholder="Numer grupy"
            value={grupa.nrGrupy}
            onChange={(e) => setGrupa({ ...grupa, nrGrupy: e.target.value })}
          />
          <input
            placeholder="Semestr"
            type="number"
            value={grupa.semestr}
            onChange={(e) =>
              setGrupa({ ...grupa, semestr: e.target.valueAsNumber })
            }
          />
          <select placeholder="Tryb studiów">
            <option value={TrybStudiow.Niestacjonarne}>Niestacjonarne</option>
            <option value={TrybStudiow.Stacjonarne}>Stacjonarne</option>
          </select>
          <select placeholder="Stopień studiów">
            <option value={StopienStudiow.Inzynierskie}>Inżynierskie</option>
            <option value={StopienStudiow.Magisterskie}>Magisterskie</option>
          </select>
          <button onClick={zapisz}>ZAPISZ</button>
        </div>
      )}
    </div>
  );
}

export default Grupy;
