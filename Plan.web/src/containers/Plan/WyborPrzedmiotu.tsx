import { Button, TextField } from "@material-ui/core";
import { DeleteOutline } from "@material-ui/icons";
import React, { useEffect, useState } from "react";
import { PrzedmiotWidok } from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
import { ErrorStyle } from "../../styles/ErrorStyle";
import WyborPrzedmiotuStyle from "../../styles/WyborPrzedmiotuStyle";

interface WyborPrzedmiotuProps {
  onWybierz: (wybrany: PrzedmiotWidok) => void;
}

function WyborPrzedmiotu(props: WyborPrzedmiotuProps) {
  const [lista, setLista] = useState([] as PrzedmiotWidok[]);
  const [nazwa, setNazwa] = useState("");
  const [blad, setBlad] = useState("");

  useEffect(() => {
    odswiezListe();
  }, []);

  function odswiezListe() {
    httpClient
      .GET("/api/przedmiot")
      .then((res: PrzedmiotWidok[]) => {
        setLista(res);
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function dodaj() {
    httpClient
      .POST("/api/przedmiot", nazwa)
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function usun(id: number) {
    httpClient
      .DELETE(`/api/przedmiot/${id}`)
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function onWybierz(wybrany: PrzedmiotWidok) {
    props.onWybierz(wybrany);
  }

  return (
    <WyborPrzedmiotuStyle>
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <span className="xl">PRZEDMIOTY</span>
      <div>
        {lista.map((x) => (
          <div className="element">
            <Button onClick={() => usun(x.id)}>
              <DeleteOutline color="secondary" />
            </Button>
            <span>{x.nazwa}</span>
            <Button color="secondary" onClick={() => onWybierz(x)}>
              WYBIERZ
            </Button>
          </div>
        ))}
      </div>
      <div className="element-dodaj">
        <TextField
          label="Nazwa"
          value={nazwa}
          onChange={(e) => setNazwa(e.target.value)}
        />
        <Button color="secondary" onClick={dodaj}>
          DODAJ
        </Button>
      </div>
    </WyborPrzedmiotuStyle>
  );
}

export default WyborPrzedmiotu;
