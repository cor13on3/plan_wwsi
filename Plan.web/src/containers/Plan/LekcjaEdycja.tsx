import {
  Button,
  Checkbox,
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
  TrybStudiow,
  ZjazdGrupyWidok,
} from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
import { Grupa, WykladowcaWidok } from "../../helpers/types";
import { ErrorStyle } from "../../styles/ErrorStyle";
import LekcjaEdycjaStyle from "../../styles/LekcjaEdycjaStyle";
import WyborPrzedmiotu from "./WyborPrzedmiotu";
import WyborSali from "./WyborSali";
import WyborLekcji from "./WyborLekcji";
import { LekcjaWybor } from "../../helpers/types";

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
  const [wyborLekcji, setWyborLekcji] = useState(false);
  const [semestr, setSemestr] = useState(0);
  const [trybStudiow, setTrybStudiow] = useState(TrybStudiow.Niestacjonarne);
  const [idLekcji, setIdLekcji] = useState(0);
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
    if (wybraneZjazdy.length === 0) {
      setBlad("Wybierz zjazdy");
      return;
    }
    if (idLekcji > 0) {
      if (zjazdOdpracowywany) {
        przypiszGrupe(idLekcji, zjazdOdpracowywany, true);
      } else {
        wybraneZjazdy.forEach((x) => {
          przypiszGrupe(idLekcji, x, false);
        });
      }
    } else {
      if (!przedmiot.id || !wykladowca.id || !sala.id) {
        setBlad("Uzupełnij dane");
        return;
      }
      httpClient
        .POST("/api/lekcja/dodaj", {
          IdPrzedmiotu: przedmiot.id,
          IdWykladowcy: wykladowca.id,
          IdSali: sala.id,
          GodzinaOd: od,
          GodzinaDo: godzinaDo,
          Forma: forma,
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
  }

  function onWyborPrzedmiotu(wybrany: PrzedmiotWidok) {
    setPrzedmiot(wybrany);
    setEdycjaPrzedmiotu(false);
  }

  function onWyborSali(wybrana: SalaWidok) {
    setSala(wybrana);
    setEdycjaSali(false);
  }

  function onWyborLekcji(wybrana: LekcjaWybor) {
    setIdLekcji(wybrana.idLekcji);
    setPrzedmiot({ nazwa: wybrana.przedmiot } as PrzedmiotWidok);
    setWykladowca({ nazwa: wybrana.wykladowca } as WykladowcaWidok);
    setSala({ nazwa: wybrana.sala } as SalaWidok);
    setOd(wybrana.godzinaOd);
    setDo(wybrana.godzinaDo);
    setForma(wybrana.forma);
    setWyborLekcji(false);
    setBlad("");
  }

  function otworzWybor() {
    httpClient.GET(`/api/grupa/${grupa}`).then((dto: Grupa) => {
      setTrybStudiow(dto.trybStudiow);
      setSemestr(dto.semestr);
      setWyborLekcji(true);
    });
  }

  return (
    <LekcjaEdycjaStyle>
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <p className="xl">
        {grupa} / {dajDzienTygodnia(dzienTygodnia).toUpperCase()}
      </p>
      <Button variant="outlined" color="secondary" onClick={otworzWybor}>
        DOŁĄCZ DO ZAJĘĆ
      </Button>
      <p className="l">Lub dodaj nowe:</p>
      <form>
        <TextField
          disabled={idLekcji > 0}
          label="Przedmiot"
          variant="outlined"
          onClick={() => {
            if (idLekcji === 0) setEdycjaPrzedmiotu(true);
          }}
          value={przedmiot.nazwa}
          InputLabelProps={{
            shrink: !!przedmiot.nazwa,
          }}
        />
        <Autocomplete
          disabled={idLekcji > 0}
          value={wykladowca}
          options={wykladowcy}
          getOptionLabel={(w) => w.nazwa}
          renderInput={(params) => (
            <TextField {...params} label="Wykładowca" variant="outlined" />
          )}
          onChange={(e, v) => v && setWykladowca(v)}
        />
        <TextField
          disabled={idLekcji > 0}
          label="Sala"
          variant="outlined"
          onClick={() => {
            if (idLekcji === 0) setEdycjaSali(true);
          }}
          value={sala.nazwa}
          InputLabelProps={{
            shrink: !!sala.nazwa,
          }}
        />
        {!zjazdOdpracowywany && (
          <Autocomplete
            multiple
            disableCloseOnSelect
            options={zjazdy}
            getOptionLabel={(w) => w.toString()}
            renderInput={(params) => (
              <TextField {...params} label="Zjazdy" variant="outlined" />
            )}
            renderOption={(option, { selected }) => (
              <React.Fragment>
                <Checkbox style={{ marginRight: 8 }} checked={selected} />
                {option}
              </React.Fragment>
            )}
            onChange={(e, v) => v && setWybraneZjazdy(v)}
          />
        )}
        <div>
          <TextField
            disabled={idLekcji > 0}
            variant="outlined"
            type="time"
            value={od}
            onChange={(e) => setOd(e.target.value)}
            label="Godzina od"
          />
          <span> - </span>
          <TextField
            disabled={idLekcji > 0}
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
            disabled={idLekcji > 0}
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
      <Drawer
        open={wyborLekcji}
        anchor="right"
        onClose={() => setWyborLekcji(false)}
      >
        <WyborLekcji
          onWybierz={onWyborLekcji}
          semestr={semestr}
          tryb={trybStudiow}
          dzienTygodnia={dzienTygodnia}
        />
      </Drawer>
    </LekcjaEdycjaStyle>
  );
}

export default LekcjaEdycja;
