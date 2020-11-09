import { Button, Checkbox, TextField } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import styled from "styled-components";
import { httpClient } from "../../helpers/httpClient";

const WyborSpecjalnosciStyle = styled.div`
  display: grid;
  grid-template-rows: auto 1fr 1fr;
  row-gap: 12px;

  .dodaj {
    padding-left: 14px;
  }

  .zapisz-btn {
    justify-self: end;
    margin-top: 12px;
  }
`;

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
    <WyborSpecjalnosciStyle>
      <div className="opcje">
        {lista.map((x, i) => (
          <div key={i}>
            <Checkbox
              onChange={(e) => zmienWybrane(x, e.target.checked)}
              checked={wybrane.some((y) => y.id === x.id)}
            />
            <span>{x.nazwa}</span>
          </div>
        ))}
      </div>
      <div className="dodaj">
        <TextField value={nowa} onChange={(e) => setNowa(e.target.value)} />
        <Button variant="text" color="secondary" onClick={dodaj}>
          DODAJ
        </Button>
      </div>
      <Button
        variant="contained"
        color="secondary"
        onClick={() => props.onWybrano(wybrane)}
        className="zapisz-btn"
      >
        WYBIERZ
      </Button>
    </WyborSpecjalnosciStyle>
  );
}

export default WyborSpecjalnosci;
