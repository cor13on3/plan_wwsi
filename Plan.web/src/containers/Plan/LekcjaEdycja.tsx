import {
  Button,
  Drawer,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@material-ui/core";
import { Autocomplete } from "@material-ui/lab";
import React, { useEffect, useState } from "react";
import dajDzienTygodnia from "../../helpers/dajDzienTygodnia";
import {
  FormaLekcji,
  PrzedmiotWidok,
  SalaWidok,
  ZjazdGrupyWidok,
} from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
import { WykladowcaWidok } from "../../helpers/types";
import LekcjaEdycjaStyle from "../../styles/LekcjaEdycjaStyle";
import WyborPrzedmiotu from "./WyborPrzedmiotu";
import WyborSali from "./WyborSali";

interface LekcjaEdycjaProps {
  grupa: string;
  dzienTygodnia: number;
  onZapisz: Function;
  zjazdOdpracowywany?: number;
}

function LekcjaEdycja({
  grupa,
  dzienTygodnia,
  zjazdOdpracowywany,
  onZapisz,
}: LekcjaEdycjaProps) {
  const [przedmiot, setPrzedmiot] = useState({} as PrzedmiotWidok);
  const [wykladowca, setWykladowca] = useState({} as WykladowcaWidok);
  const [wykladowcy, setWykladowcy] = useState([] as WykladowcaWidok[]);
  const [sala, setSala] = useState({} as SalaWidok);
  const [zjazdy, setZjazdy] = useState([] as number[]);
  const [wybraneZjazdy, setWybraneZjazdy] = useState([] as number[]);
  const [od, setOd] = useState("08:00");
  const [godzinaDo, setDo] = useState("08:00");
  const [forma, setForma] = useState(FormaLekcji.Wyklad);
  const [edycjaPrzedmiotu, setEdycjaPrzedmiotu] = useState(false);
  const [edycjaSali, setEdycjaSali] = useState(false);
  const [blad, setBlad] = useState("");

  useEffect(() => {
    httpClient
      .GET("/api/wykladowca")
      .then((res: WykladowcaWidok[]) => {
        setWykladowcy(res);
      })
      .catch((err: Blad) => setBlad(err.Tresc));
    if (!zjazdOdpracowywany) {
      httpClient
        .GET(`/api/kalendarium/${grupa}`)
        .then((res: ZjazdGrupyWidok[]) => {
          setZjazdy(res.filter((x) => !x.czyOdpracowanie).map((x) => x.nr));
        })
        .catch((err: Blad) => setBlad(err.Tresc));
    }
    // eslint-disable-next-line
  }, []);

  function przypiszGrupe(
    id: number,
    nrZjazdu: number,
    czyOdpracowanie: boolean
  ) {
    httpClient
      .POST("/api/lekcja/przypisz-grupe", {
        IdLekcji: id,
        NrGrupy: grupa,
        NrZjazdu: nrZjazdu,
        DzienTygodnia: dzienTygodnia,
        CzyOdpracowanie: czyOdpracowanie,
      })
      .then(() => onZapisz())
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function zapisz() {
    httpClient
      .POST("/api/lekcja/dodaj", {
        IdPrzedmiotu: przedmiot.id,
        IdWykladowcy: wykladowca.id,
        IdSali: sala.id,
        GodzinaOd: od,
        GodzinaDo: godzinaDo,
        FormaLekcji: forma,
      })
      .then((id: number) => {
        if (zjazdOdpracowywany) {
          przypiszGrupe(id, zjazdOdpracowywany, true);
        } else {
          wybraneZjazdy.forEach((x) => {
            przypiszGrupe(id, x, false);
          });
        }
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function onWyborPrzedmiotu(wybrany: PrzedmiotWidok) {
    setPrzedmiot(wybrany);
    setEdycjaPrzedmiotu(false);
  }

  function onWyborSali(wybrana: SalaWidok) {
    setSala(wybrana);
    setEdycjaSali(false);
  }

  return (
    <LekcjaEdycjaStyle>
      {blad && <p className="blad">{blad}</p>}
      <p className="xl">
        ZAJĘCIA DLA GRUPY {grupa} NA{" "}
        {dajDzienTygodnia(dzienTygodnia).toUpperCase()}
      </p>
      <form>
        <TextField
          label="Przedmiot"
          variant="outlined"
          onClick={() => setEdycjaPrzedmiotu(true)}
          value={przedmiot.nazwa}
        />
        <Autocomplete
          options={wykladowcy}
          getOptionLabel={(w) => w.nazwa}
          renderInput={(params) => (
            <TextField {...params} label="Wykładowca" variant="outlined" />
          )}
          onChange={(e, v) => v && setWykladowca(v)}
        />
        <TextField
          label="Sala"
          variant="outlined"
          onClick={() => setEdycjaSali(true)}
          value={sala.nazwa}
        />
        {!zjazdOdpracowywany && (
          <Autocomplete
            multiple
            options={zjazdy}
            getOptionLabel={(w) => w.toString()}
            renderInput={(params) => (
              <TextField {...params} label="Zjazdy" variant="outlined" />
            )}
            onChange={(e, v) => v && setWybraneZjazdy(v)}
          />
        )}
        <div>
          <TextField
            variant="outlined"
            type="time"
            value={od}
            onChange={(e) => setOd(e.target.value)}
            label="Godzina od"
          />
          <span> - </span>
          <TextField
            variant="outlined"
            type="time"
            value={godzinaDo}
            onChange={(e) => setDo(e.target.value)}
            label="Godzina do"
          />
        </div>
        <FormControl variant="outlined">
          <InputLabel>Forma zajęć</InputLabel>
          <Select
            label="Forma zajęć"
            value={forma}
            onChange={(e) => setForma(e.target.value as FormaLekcji)}
          >
            <MenuItem value={FormaLekcji.Wyklad}>Wykład</MenuItem>
            <MenuItem value={FormaLekcji.Cwiczenia}>Ćwiczenie</MenuItem>
          </Select>
        </FormControl>
        <Button
          className="zapiszBtn"
          variant="contained"
          color="secondary"
          onClick={zapisz}
        >
          ZAPISZ
        </Button>
      </form>
      <Drawer
        open={edycjaPrzedmiotu}
        anchor="right"
        onClose={() => setEdycjaPrzedmiotu(false)}
      >
        <WyborPrzedmiotu onWybierz={onWyborPrzedmiotu} />
      </Drawer>
      <Drawer
        open={edycjaSali}
        anchor="right"
        onClose={() => setEdycjaSali(false)}
      >
        <WyborSali onWybierz={onWyborSali} />
      </Drawer>
    </LekcjaEdycjaStyle>
  );
}

export default LekcjaEdycja;
