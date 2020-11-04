import { TextField } from "@material-ui/core";
import { Autocomplete } from "@material-ui/lab";
import React, { useEffect, useState } from "react";
import {
  FormaLekcji,
  PrzedmiotWidok,
  SalaWidok,
  ZjazdGrupyWidok,
} from "../../helpers/enums";
import { httpClient } from "../../helpers/httpClient";
import { WykladowcaWidok } from "../../helpers/types";
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
  const [od, setOd] = useState("");
  const [godzinaDo, setDo] = useState("");
  const [forma, setForma] = useState(FormaLekcji.Wyklad);
  const [edycjaPrzedmiotu, setEdycjaPrzedmiotu] = useState(false);
  const [edycjaSali, setEdycjaSali] = useState(false);

  function onWyborPrzedmiotu(wybrany: PrzedmiotWidok) {
    setPrzedmiot(wybrany);
    setEdycjaPrzedmiotu(false);
  }

  function onWyborSali(wybrana: SalaWidok) {
    setSala(wybrana);
    setEdycjaSali(false);
  }

  useEffect(() => {
    httpClient.GET("/api/wykladowca").then((res: WykladowcaWidok[]) => {
      setWykladowcy(res);
    });
    if (!zjazdOdpracowywany) {
      httpClient
        .GET(`/api/kalendarium/${grupa}`)
        .then((res: ZjazdGrupyWidok[]) => {
          setZjazdy(res.filter((x) => !x.czyOdpracowanie).map((x) => x.nr));
        });
    }
    // eslint-disable-next-line
  }, []);

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
          httpClient
            .POST("/api/lekcja/przypisz-grupe", {
              IdLekcji: id,
              NrGrupy: grupa,
              NrZjazdu: zjazdOdpracowywany,
              DzienTygodnia: dzienTygodnia,
              CzyOdpracowanie: true,
            })
            .then(() => onZapisz());
        } else {
          wybraneZjazdy.forEach((x) => {
            httpClient
              .POST("/api/lekcja/przypisz-grupe", {
                IdLekcji: id,
                NrGrupy: grupa,
                NrZjazdu: x,
                DzienTygodnia: dzienTygodnia,
                CzyOdpracowanie: false,
              })
              .then(() => onZapisz());
          });
        }
      });
  }

  return (
    <div>
      <h3>
        Edycja zajęć dla grupy {grupa} na dzień {dzienTygodnia}.
      </h3>
      <span>Przedmiot: </span>
      <input
        onClick={() => setEdycjaPrzedmiotu(true)}
        onChange={() => {}}
        value={przedmiot.nazwa}
      />
      {edycjaPrzedmiotu && <WyborPrzedmiotu onWybierz={onWyborPrzedmiotu} />}
      <p>Wykładowca: </p>
      <Autocomplete
        options={wykladowcy}
        getOptionLabel={(w) => w.nazwa}
        style={{ width: 300 }}
        renderInput={(params) => (
          <TextField {...params} label="Wykładowca" variant="outlined" />
        )}
        onChange={(e, v) => v && setWykladowca(v)}
      />
      <span>Sala: </span>
      <input
        onClick={() => setEdycjaSali(true)}
        onChange={() => {}}
        value={sala.nazwa}
      />
      {edycjaSali && <WyborSali onWybierz={onWyborSali} />}
      {!zjazdOdpracowywany && (
        <>
          <p>Zjazdy: </p>
          <Autocomplete
            multiple
            options={zjazdy}
            getOptionLabel={(w) => w.toString()}
            style={{ width: 300 }}
            renderInput={(params) => (
              <TextField {...params} label="Zjazdy" variant="outlined" />
            )}
            onChange={(e, v) => v && setWybraneZjazdy(v)}
          />
        </>
      )}
      <span>Godzina: </span>
      <input
        type="time"
        value={od}
        onChange={(e) => setOd(e.target.value)}
        placeholder="Godzina od"
      />
      <span> - </span>
      <input
        type="time"
        value={godzinaDo}
        onChange={(e) => setDo(e.target.value)}
        placeholder="Godzina do"
      />
      <p></p>
      <select
        value={forma}
        onChange={(e) => setForma(e.target.value as FormaLekcji)}
      >
        <option value={FormaLekcji.Wyklad}>Wykład</option>
        <option value={FormaLekcji.Cwiczenia}>Ćwiczenie</option>
      </select>
      <p></p>
      <button onClick={zapisz}>ZAPISZ</button>
    </div>
  );
}

export default LekcjaEdycja;
