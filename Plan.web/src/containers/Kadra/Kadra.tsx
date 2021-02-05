import { Button, Drawer, TextField } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import ContextMenu from "../../components/ContextMenu";
import { Blad, httpClient } from "../../helpers/httpClient";
import { WykladowcaWidok } from "../../helpers/types";
import { ErrorStyle } from "../../styles/ErrorStyle";
import "./Kadra.css";
import KadraEdycja from "./KadraEdycja";

function Kadra() {
  const [lista, setLista] = useState([] as WykladowcaWidok[]);
  const [blad, setBlad] = useState("");
  const [czyEdycja, setCzyEdycja] = useState(false);
  const [edytowany, setEdytowany] = useState(null as number | null);
  const [fraza, setFraza] = useState("");

  useEffect(() => {
    odswiezListe();
  }, [fraza]);

  function odswiezListe() {
    httpClient
      .GET(`/api/wykladowca?fraza=${fraza}`)
      .then((res: WykladowcaWidok[]) => setLista(res))
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function onZapisz() {
    setCzyEdycja(false);
    setEdytowany(null);
    odswiezListe();
  }

  function onAnuluj() {
    setCzyEdycja(false);
    setEdytowany(null);
  }

  function usun(id: number) {
    httpClient
      .DELETE("/api/wykladowca/" + id)
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  return (
    <div className="kadra">
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <div className="kadra_header">
        <span className="xxl">Zarządzanie kadrą</span>
        <Button
          variant="contained"
          color="secondary"
          onClick={() => setCzyEdycja(true)}
        >
          DODAJ
        </Button>
      </div>
      <div className="szukajka">
        <TextField
          value={fraza}
          onChange={(e) => setFraza(e.target.value)}
          variant="outlined"
          placeholder="Szukaj.."
          autoFocus
        />
      </div>
      <div className="lista">
        <div className="kadra_lista_header disabled">
          <span>NAZWISKO</span>
          <span>EMAIL</span>
        </div>
        {lista.map((x, i) => (
          <div className="wykladowca" key={i}>
            <span>{x.nazwa}</span>
            <span>{x.email}</span>
            <ContextMenu
              items={[
                {
                  title: "Edytuj",
                  action: () => {
                    setEdytowany(x.id);
                    setCzyEdycja(true);
                  },
                },
                { title: "Usuń", action: () => usun(x.id) },
              ]}
            />
          </div>
        ))}
      </div>
      <Drawer
        open={czyEdycja}
        onClose={() => setCzyEdycja(false)}
        anchor="right"
      >
        <KadraEdycja id={edytowany} onZapisz={onZapisz} onAnuluj={onAnuluj} />
      </Drawer>
    </div>
  );
}

export default Kadra;
