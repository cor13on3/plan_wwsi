import {
  Button,
  Dialog,
  DialogContent,
  DialogTitle,
  TextField,
} from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { Blad, httpClient } from "../../helpers/httpClient";
import { ErrorStyle } from "../../styles/ErrorStyle";
import { KadraEdycjaStyle } from "../../styles/KadraEdycjaStyle";
import WyborSpecjalizacji, { Specjalizacja } from "./WyborSpecjalizacji";

interface Wykladowca {
  id: number;
  tytul: string;
  imie: string;
  nazwisko: string;
  email: string;
  specjalizacje: Specjalizacja[];
}

interface Props {
  id: number | null;
  onZapisz: () => void;
  onAnuluj: () => void;
}

function KadraEdycja(props: Props) {
  const [edycjaSpecjalizacji, setEdycjaSpecjalizacji] = useState(false);
  const [blad, setBlad] = useState("");
  const [edytowany, setEdytowany] = useState({
    id: -1,
    imie: "",
    nazwisko: "",
    email: "",
    specjalizacje: [],
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

  function dajSpecjalizacje() {
    return edytowany.specjalizacje.map((x) => x.nazwa).join(", ");
  }

  function onWybranoSpecjalizacje(lista: Specjalizacja[]) {
    setEdytowany({ ...edytowany, specjalizacje: lista });
    setEdycjaSpecjalizacji(false);
  }

  function zapisz() {
    const dto = {
      ...edytowany,
      specjalizacje: edytowany.specjalizacje.map((x) => x.id),
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
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <p className="xl">NOWY WYKŁADOWCA</p>
      <form>
        <TextField
          id="nazwisko"
          label="Nazwisko"
          variant="outlined"
          value={edytowany.nazwisko}
          onChange={(e) =>
            setEdytowany({ ...edytowany, nazwisko: e.target.value })
          }
        />
        <TextField
          id="imie"
          variant="outlined"
          label="Imie"
          value={edytowany.imie}
          onChange={(e) => setEdytowany({ ...edytowany, imie: e.target.value })}
        />
        <TextField
          id="tytul"
          variant="outlined"
          label="Tytuł"
          value={edytowany.tytul}
          onChange={(e) =>
            setEdytowany({ ...edytowany, tytul: e.target.value })
          }
        />
        <TextField
          id="specjalizacje"
          variant="outlined"
          onChange={() => {}}
          value={dajSpecjalizacje()}
          label="Specjalizacje"
          onClick={() => setEdycjaSpecjalizacji(true)}
        />
        <TextField
          id="email"
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
        open={edycjaSpecjalizacji}
        onClose={() => setEdycjaSpecjalizacji(false)}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">Specjalizacje</DialogTitle>
        <DialogContent>
          <WyborSpecjalizacji
            specjalizacje={edytowany.specjalizacje}
            onWybrano={(lista) => onWybranoSpecjalizacje(lista)}
          />
        </DialogContent>
      </Dialog>
    </KadraEdycjaStyle>
  );
}

export default KadraEdycja;
