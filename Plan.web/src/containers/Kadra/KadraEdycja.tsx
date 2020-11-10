import {
  Button,
  Dialog,
  DialogContent,
  DialogTitle,
  TextField,
} from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { Blad, httpClient } from "../../helpers/httpClient";
import { KadraEdycjaStyle } from "../../styles/KadraEdycjaStyle";
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
    <KadraEdycjaStyle>
      {blad && <p>{blad}</p>}
      <p className="xl">NOWY WYKŁADOWCA</p>
      <form>
        <TextField
          label="Nazwisko"
          variant="outlined"
          value={edytowany.nazwisko}
          onChange={(e) =>
            setEdytowany({ ...edytowany, nazwisko: e.target.value })
          }
        />
        <TextField
          variant="outlined"
          label="Imie"
          value={edytowany.imie}
          onChange={(e) => setEdytowany({ ...edytowany, imie: e.target.value })}
        />
        <TextField
          variant="outlined"
          label="Tytuły"
          value={edytowany.tytul}
          onChange={(e) =>
            setEdytowany({ ...edytowany, tytul: e.target.value })
          }
        />
        <TextField
          variant="outlined"
          onChange={() => {}}
          value={dajSpecjalnosci()}
          label="Specjalizacje"
          onClick={() => setEdycjaSpecjalnosci(true)}
        />
        <TextField
          variant="outlined"
          label="Email"
          value={edytowany.email}
          onChange={(e) =>
            setEdytowany({ ...edytowany, email: e.target.value })
          }
        />
        <div className="buttons">
          <Button color="secondary" variant="outlined" onClick={props.onAnuluj}>
            ANULUJ
          </Button>
          <Button color="secondary" variant="contained" onClick={zapisz}>
            ZAPISZ
          </Button>
        </div>
      </form>
      <Dialog
        open={edycjaSpecjalnosci}
        onClose={() => setEdycjaSpecjalnosci(false)}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">Specjalizacje</DialogTitle>
        <DialogContent>
          <WyborSpecjalnosci
            specjalnosci={edytowany.specjalnosci}
            onWybrano={(lista) => onWybranoSpecjalnosci(lista)}
          />
        </DialogContent>
      </Dialog>
    </KadraEdycjaStyle>
  );
}

export default KadraEdycja;
