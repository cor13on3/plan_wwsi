import { Button, TextField } from "@material-ui/core";
import { DeleteOutline } from "@material-ui/icons";
import React, { useEffect, useState } from "react";
import { SalaWidok } from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
import { ErrorStyle } from "../../styles/ErrorStyle";
import WyborSaliStyle from "../../styles/WyborSaliStyle";

interface WyborSaliProps {
  onWybierz: (wybrany: SalaWidok) => void;
}

function WyborSali(props: WyborSaliProps) {
  const [lista, setLista] = useState([] as SalaWidok[]);
  const [nazwa, setNazwa] = useState("");
  const [blad, setBlad] = useState("");

  useEffect(() => {
    odswiezListe();
  }, []);

  function odswiezListe() {
    httpClient
      .GET("/api/sala")
      .then((res: SalaWidok[]) => {
        setLista(res);
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function dodaj() {
    httpClient
      .POST("/api/sala", { Nazwa: nazwa })
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function onWybierz(wybrany: SalaWidok) {
    props.onWybierz(wybrany);
  }

  function usun(id: number) {
    httpClient
      .DELETE(`/api/sala/${id}`)
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  return (
    <WyborSaliStyle>
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <span className="xl">SALE</span>
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
          id="salaNazwa"
          label="Nazwa"
          value={nazwa}
          onChange={(e) => setNazwa(e.target.value)}
        />
        <Button color="secondary" onClick={dodaj}>
          DODAJ
        </Button>
      </div>
    </WyborSaliStyle>
  );
}

export default WyborSali;
