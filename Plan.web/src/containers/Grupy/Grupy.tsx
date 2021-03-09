import { Button, Drawer, TextField } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import ContextMenu from "../../components/ContextMenu";
import { StopienStudiow } from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
import { GrupaWidok } from "../../helpers/types";
import { ErrorStyle } from "../../styles/ErrorStyle";
import GrupaEdycja from "./GrupaEdycja";
import { GrupyStyle } from "../../styles/GrupyStyle";

function Grupy() {
  const [blad, setBlad] = useState("");
  const [lista, setLista] = useState([] as GrupaWidok[]);
  const [czyEdycja, setCzyEdycja] = useState(false);
  const [fraza, setFraza] = useState("");

  function odswiezListe() {
    httpClient
      .GET(`/api/grupa?fraza=${fraza}`)
      .then((res: GrupaWidok[]) => {
        setLista(res);
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  useEffect(() => {
    odswiezListe();
    // eslint-disable-next-line
  }, [fraza]);

  function pokazEdycje() {
    setCzyEdycja(true);
  }

  function usun(numer: string) {
    httpClient
      .DELETE(`/api/grupa/${numer}`)
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function onZapisz() {
    setCzyEdycja(false);
    odswiezListe();
  }

  function dajStopien(stopien: StopienStudiow) {
    if (stopien === StopienStudiow.Inzynierskie) return "inżynierskie";
    return stopien.toLowerCase();
  }

  return (
    <GrupyStyle>
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <div className="grupy_header">
        <span className="xxl">Zarządzanie grupami</span>
        <Button
          variant="contained"
          color="secondary"
          onClick={() => pokazEdycje()}
        >
          DODAJ
        </Button>
      </div>
      <div className="szukajka">
        <TextField
          id="szukajka"
          value={fraza}
          onChange={(e) => setFraza(e.target.value)}
          variant="outlined"
          placeholder="Szukaj.."
          autoFocus
        />
      </div>
      <div className="lista_header disabled">
        <span>NUMER</span>
        <span>SEMESTR</span>
        <span>STOPIEŃ</span>
        <span>TRYB</span>
      </div>
      <div className="lista">
        {lista.map((grupa, i) => (
          <div id="row" className="grupa" key={i}>
            <span id="rowNumer" className="l">
              {grupa.numer}
            </span>
            <span id="rowSemestr" className="l">
              {grupa.semestr}
            </span>
            <span id="rowStopien" className="l">
              {dajStopien(grupa.stopienStudiow)}
            </span>
            <span id="rowTryb" className="l">
              {grupa.trybStudiow.toLowerCase()}
            </span>
            <ContextMenu
              items={[{ title: "Usuń", action: () => usun(grupa.numer) }]}
            />
          </div>
        ))}
      </div>
      <Drawer
        open={czyEdycja}
        onClose={() => setCzyEdycja(false)}
        anchor="right"
      >
        <GrupaEdycja onZapisz={onZapisz} />
      </Drawer>
    </GrupyStyle>
  );
}

export default Grupy;
