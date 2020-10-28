import React, { useEffect, useState } from "react";
import { Blad, httpClient } from "../../helpers/httpClient";
import WyborSpecjalnosci, { Specjalnosc } from "./WyborSpecjalnosci";

interface Wykladowca {
  id: number;
  tytul: string;
  imie: string;
  nazwisko: string;
  email: string;
  specjalnosci: Specjalnosc[];
}

interface Props {
  id: number | null;
  onZapisz: () => void;
  onAnuluj: () => void;
}

function KadraEdycja(props: Props) {
  const [edycjaSpecjalnosci, setEdycjaSpecjalnosci] = useState(false);
  const [blad, setBlad] = useState("");
  const [edytowany, setEdytowany] = useState({
    id: -1,
    imie: "",
    nazwisko: "",
    email: "",
    specjalnosci: [],
    tytul: "",
  } as Wykladowca);

  useEffect(() => {
    if (props.id) {
      httpClient
        .GET("/api/wykladowca/" + props.id)
        .then((dto: Wykladowca) => setEdytowany(dto))
        .catch((err: Blad) => setBlad(err.Tresc));
    }
  }, [props.id]);

  function dajSpecjalnosci() {
    return edytowany.specjalnosci.map((x) => x.nazwa).join(", ");
  }

  function onWybranoSpecjalnosci(lista: Specjalnosc[]) {
    setEdytowany({ ...edytowany, specjalnosci: lista });
    setEdycjaSpecjalnosci(false);
  }

  function zapisz() {
    const dto = {
      ...edytowany,
      specjalnosci: edytowany.specjalnosci.map((x) => x.id),
    };
    if (props.id) {
      httpClient
        .PUT("/api/wykladowca/" + props.id, dto)
        .then(() => props.onZapisz())
        .catch((err: Blad) => setBlad(err.Tresc));
    } else {
      httpClient
        .POST("/api/wykladowca", dto)
        .then(() => props.onZapisz())
        .catch((err: Blad) => setBlad(err.Tresc));
    }
  }

  return (
    <div>
      {blad && <p className="blad">{blad}</p>}
      <div className="edycja">
        <input
          placeholder="Nazwisko"
          value={edytowany.nazwisko}
          onChange={(e) =>
            setEdytowany({ ...edytowany, nazwisko: e.target.value })
          }
        />
        <input
          placeholder="Imie"
          value={edytowany.imie}
          onChange={(e) => setEdytowany({ ...edytowany, imie: e.target.value })}
        />
        <input
          placeholder="Tytuły"
          value={edytowany.tytul}
          onChange={(e) =>
            setEdytowany({ ...edytowany, tytul: e.target.value })
          }
        />
        <input
          onChange={() => {}}
          value={dajSpecjalnosci()}
          placeholder="Specjalizacje"
          onClick={() => setEdycjaSpecjalnosci(true)}
        />
        <input
          placeholder="Email"
          value={edytowany.email}
          onChange={(e) =>
            setEdytowany({ ...edytowany, email: e.target.value })
          }
        />
        <button onClick={zapisz}>ZAPISZ</button>
        <button onClick={props.onAnuluj}>ANULUJ</button>
      </div>
      {edycjaSpecjalnosci && (
        <WyborSpecjalnosci
          specjalnosci={edytowany.specjalnosci}
          onWybrano={(lista) => onWybranoSpecjalnosci(lista)}
        />
      )}
    </div>
  );
}

export default KadraEdycja;
