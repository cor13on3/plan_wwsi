import React, { useEffect, useState } from "react";
import {
  StopienStudiow,
  TrybStudiow,
  ZjazdGrupyWidok,
} from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
import { GrupaWidok } from "../../helpers/types";
import KalendariumEdycja from "./KalendariumEdycja";
import "./Kalendarium.css";
import formatujDate from "../../helpers/formatujDate";

function Kalendarium() {
  const [stopien, setStopien] = useState(
    StopienStudiow.Inzynierskie as StopienStudiow | "Wybierz"
  );
  const [tryb, setTryb] = useState(
    TrybStudiow.Niestacjonarne as TrybStudiow | "Wybierz"
  );
  const [semestr, setSemestr] = useState("7");
  const [grupy, setGrupy] = useState([] as string[]);
  const [blad, setBlad] = useState("");
  const [lista, setLista] = useState([] as ZjazdGrupyWidok[]);
  const [czyEdycja, setCzyEdycja] = useState(false);

  function odswiezListe() {
    httpClient
      .GET(`/api/grupa/${tryb}/${stopien}/${semestr}`)
      .then((res: GrupaWidok[]) => setGrupy(res.map((x) => x.numer)))
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  useEffect(() => {
    if (stopien !== "Wybierz" && tryb !== "Wybierz" && semestr !== "Wybierz") {
      odswiezListe();
    }
    // eslint-disable-next-line
  }, [stopien, tryb, semestr]);

  useEffect(() => {
    if (grupy.length > 0) {
      httpClient
        .GET(`/api/kalendarium/${grupy[0]}`)
        .then((res: ZjazdGrupyWidok[]) => {
          setLista(res);
        })
        .catch((err: Blad) => setBlad(err.Tresc));
    } else setLista([]);
  }, [grupy]);

  function dajOpis() {
    if (stopien === "Wybierz" || tryb === "Wybierz" || semestr === "Wybierz")
      return "";
    let opis = `Kalendarium dla ${semestr}. semestru studiów ${stopien} (${tryb})`;
    if (grupy.length > 0) opis += ` (grupy: ${grupy.join(", ")})`;
    else opis += " (brak grup)";
    return opis;
  }

  function onZapisz() {
    setCzyEdycja(false);
    odswiezListe();
  }

  function onUsun(nr: number) {
    httpClient
      .POST("/api/kalendarium/usun-grupy-z-zjazdu", {
        Grupy: grupy,
        NrKolejny: nr,
      })
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  return (
    <div>
      {blad && <p className="blad">{blad}</p>}
      <select
        placeholder="Stopień studiów"
        value={stopien}
        onChange={(e) => setStopien(e.target.value as StopienStudiow)}
      >
        <option value="Wybierz">Wybierz</option>
        <option value={StopienStudiow.Inzynierskie}>Inżynierskie</option>
        <option value={StopienStudiow.Magisterskie}>Magisterskie</option>
        <option value={StopienStudiow.Podyplomowe}>Podyplomowe</option>
      </select>
      <select
        placeholder="Tryb studiów"
        value={tryb}
        onChange={(e) => setTryb(e.target.value as TrybStudiow)}
      >
        <option value="Wybierz">Wybierz</option>
        <option value={TrybStudiow.Niestacjonarne}>Niestacjonarne</option>
        <option value={TrybStudiow.Stacjonarne}>Stacjonarne</option>
      </select>
      <select
        placeholder="Semestr"
        value={semestr}
        onChange={(e) => setSemestr(e.target.value)}
      >
        <option value="Wybierz">Wybierz</option>
        <option value="0">0</option>
        <option value="1">1</option>
        <option value="2">2</option>
        <option value="3">3</option>
        <option value="4">4</option>
        <option value="5">5</option>
        <option value="6">6</option>
        <option value="7">7</option>
      </select>
      <span>{dajOpis()}</span>
      <div className="lista">
        {lista.map((x, i) => (
          <div key={i}>
            <span>{x.nr}. </span>
            <span>{formatujDate(x.dataOd)} - </span>
            <span>{formatujDate(x.dataDo)}</span>
            {x.czyOdpracowanie && <span> (odpracowanie)</span>}
            <button onClick={() => onUsun(x.nr)}>USUŃ</button>
          </div>
        ))}
      </div>
      <button onClick={() => setCzyEdycja(true)}>DODAJ</button>
      {czyEdycja && <KalendariumEdycja grupy={grupy} onZapisz={onZapisz} />}
    </div>
  );
}

export default Kalendarium;
