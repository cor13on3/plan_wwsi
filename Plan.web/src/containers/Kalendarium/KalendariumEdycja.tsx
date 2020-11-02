import React, { useState } from "react";
import formatujDate from "../../helpers/formatujDate";
import { Blad, httpClient } from "../../helpers/httpClient";
import { ZjazdWidok } from "../../helpers/types";
import ZjazdyEdycja from "./ZjazdyEdycja";

interface KalendariumEdycjaProps {
  grupy: string[];
  onZapisz: Function;
}

function KalendariumEdycja(props: KalendariumEdycjaProps) {
  const [nr, setNr] = useState(1);
  const [odpracowanie, setOdpracowanie] = useState(false);
  const [wybranyZjazd, setWybranyZjazd] = useState({} as ZjazdWidok);
  const [edycjaZjazdow, setEdycjaZjazdow] = useState(false);
  const [blad, setBlad] = useState("");

  function zapisz() {
    httpClient
      .POST("/api/kalendarium/przyporzadkuj-grupy-do-zjazdu", {
        Grupy: props.grupy,
        Zjazd: {
          NrZjazdu: nr,
          IdZjazdu: wybranyZjazd.idZjazdu,
          CzyOdpracowanie: odpracowanie,
        },
      })
      .then(() => {
        props.onZapisz();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function onWybierz(wybrany: ZjazdWidok) {
    setWybranyZjazd(wybrany);
    setEdycjaZjazdow(false);
  }

  return (
    <div>
      {blad && <p className="blad">{blad}</p>}
      <p>Wprowadzanie zjazdu</p>
      <span>Nr kolejny: </span>
      <input
        placeholder="Numer kolejny"
        type="number"
        value={nr}
        onChange={(e) => setNr(e.target.valueAsNumber)}
      />
      <span>Data: </span>
      {wybranyZjazd.idZjazdu ? (
        <>
          <span>{formatujDate(wybranyZjazd.dataOd)} - </span>
          <span>{formatujDate(wybranyZjazd.dataDo)}</span>
        </>
      ) : (
        <button onClick={() => setEdycjaZjazdow(true)}>Wybierz zjazd</button>
      )}
      <span>Czy odpracowanie</span>
      <input
        type="checkbox"
        checked={odpracowanie}
        onChange={(e) => setOdpracowanie(e.target.checked)}
      />
      <button onClick={zapisz}>ZAPISZ</button>
      {edycjaZjazdow && <ZjazdyEdycja onWybierz={onWybierz} />}
    </div>
  );
}

export default KalendariumEdycja;
