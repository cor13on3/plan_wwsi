import { Button, Drawer } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import ContextMenu from "../../components/ContextMenu";
import { StopienStudiow } from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
import { GrupaWidok } from "../../helpers/types";
import { ErrorStyle } from "../../styles/ErrorStyle";
import GrupaEdycja from "./GrupaEdycja";
import "./Grupy.css";

function Grupy() {
  const [blad, setBlad] = useState("");
  const [lista, setLista] = useState([] as GrupaWidok[]);
  const [czyEdycja, setCzyEdycja] = useState(false);

  function odswiezListe() {
    httpClient
      .GET("/api/grupa")
      .then((res: GrupaWidok[]) => setLista(res))
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  useEffect(() => {
    odswiezListe();
  }, []);

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
    <div className="grupy">
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
      <div className="lista">
        <div className="lista_header disabled">
          <span>NUMER</span>
          <span>SEMESTR</span>
          <span>STOPIEŃ</span>
          <span>TRYB</span>
        </div>
        {lista.map((grupa, i) => (
          <div className="grupa" key={i}>
            <span className="l">{grupa.numer}</span>
            <span className="l">{grupa.semestr}</span>
            <span className="l">{dajStopien(grupa.stopienStudiow)}</span>
            <span className="l">{grupa.trybStudiow.toLowerCase()}</span>
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
    </div>
  );
}

export default Grupy;
