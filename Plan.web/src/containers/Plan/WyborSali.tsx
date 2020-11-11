import { Button, TextField } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { SalaWidok } from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
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

  return (
    <WyborSaliStyle>
      <span className="xl">SALE</span>
      {blad && <p className="blad">{blad}</p>}
      <div>
        {lista.map((x) => (
          <div className="element">
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
    </WyborSaliStyle>
  );
}

export default WyborSali;
