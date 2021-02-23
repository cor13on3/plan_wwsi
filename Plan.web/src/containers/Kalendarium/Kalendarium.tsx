import React, { useEffect, useState } from "react";
import {
  StopienStudiow,
  TrybStudiow,
  ZjazdGrupyWidok,
} from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
import { GrupaWidok } from "../../helpers/types";
import KalendariumEdycja from "./KalendariumEdycja";
import "./Kalendarium.css";
import formatujDate from "../../helpers/formatujDate";
import {
  Button,
  Drawer,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
} from "@material-ui/core";
import ContextMenu from "../../components/ContextMenu";
import { ErrorStyle } from "../../styles/ErrorStyle";

function Kalendarium() {
  const [stopien, setStopien] = useState(StopienStudiow.Inzynierskie);
  const [tryb, setTryb] = useState(TrybStudiow.Niestacjonarne);
  const [semestr, setSemestr] = useState("1");
  const [grupy, setGrupy] = useState([] as string[]);
  const [blad, setBlad] = useState("");
  const [lista, setLista] = useState([] as ZjazdGrupyWidok[]);
  const [czyEdycja, setCzyEdycja] = useState(false);

  function odswiezListe() {
    httpClient
      .GET(`/api/grupa/filtruj/${tryb}/${stopien}/${semestr}`)
      .then((res: GrupaWidok[]) => setGrupy(res.map((x) => x.numer)))
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  useEffect(() => {
    odswiezListe();
    // eslint-disable-next-line
  }, [stopien, tryb, semestr]);

  useEffect(() => {
    if (grupy.length > 0) {
      httpClient
        .GET(`/api/kalendarium/${grupy[0]}`)
        .then((res: ZjazdGrupyWidok[]) => {
          setLista(res);
        })
        .catch((err: Blad) => setBlad(err.Tresc));
    } else setLista([]);
  }, [grupy]);

  function dajOpis() {
    if (grupy.length === 0) return "Brak grup dla wybranych kryteriów";
    let opis = `Kalendarium dla ${semestr}. semestru studiów ${stopien} (${tryb})`;
    opis += ` (grupy: ${grupy.join(", ")})`;
    return opis;
  }

  function onZapisz() {
    setCzyEdycja(false);
    odswiezListe();
  }

  function onUsun(nr: number) {
    httpClient
      .POST("/api/kalendarium/usun-grupy-z-zjazdu", {
        Grupy: grupy,
        NrKolejny: nr,
      })
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  return (
    <div className="kalendarium">
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <div className="kalendarium_header">
        <span id="tytul" className="xxl">
          Zarządzanie zjazdami
        </span>
        <Button
          variant="contained"
          color="secondary"
          onClick={() => setCzyEdycja(true)}
        >
          PRZYPISZ ZJAZD
        </Button>
      </div>
      <div className="kalendarium_main">
        <div className="filters">
          <FormControl variant="outlined">
            <InputLabel>Stopień studiów</InputLabel>
            <Select
              id="stopienStudiow"
              value={stopien}
              onChange={(e) => setStopien(e.target.value as StopienStudiow)}
              label="Stopień studiów"
            >
              <MenuItem value={StopienStudiow.Inzynierskie}>
                Inżynierskie
              </MenuItem>
              <MenuItem value={StopienStudiow.Magisterskie}>
                Magisterskie
              </MenuItem>
              <MenuItem value={StopienStudiow.Podyplomowe}>
                Podyplomowe
              </MenuItem>
            </Select>
          </FormControl>
          <FormControl variant="outlined">
            <InputLabel>Tryb studiów</InputLabel>
            <Select
              id="trybStudiow"
              value={tryb}
              onChange={(e) => setTryb(e.target.value as TrybStudiow)}
              label="Tryb studiów"
            >
              <MenuItem value={TrybStudiow.Niestacjonarne}>
                Niestacjonarne
              </MenuItem>
              <MenuItem value={TrybStudiow.Stacjonarne}>Stacjonarne</MenuItem>
            </Select>
          </FormControl>
          <FormControl variant="outlined">
            <InputLabel>Semestr</InputLabel>
            <Select
              id="semestr"
              value={semestr}
              onChange={(e) => setSemestr(e.target.value as string)}
              label="Semestr"
            >
              <MenuItem value="1">1</MenuItem>
              <MenuItem value="2">2</MenuItem>
              <MenuItem value="3">3</MenuItem>
              <MenuItem value="4">4</MenuItem>
              <MenuItem value="5">5</MenuItem>
              <MenuItem value="6">6</MenuItem>
              <MenuItem value="7">7</MenuItem>
            </Select>
          </FormControl>
        </div>
        <div className="zjazd_lista">
          <span id="zjazdyOpis" className="xl">
            {dajOpis()}
          </span>
          <div className="zjazd_lista_header disabled">
            <span>NR</span>
            <span>DATA</span>
          </div>
          {lista.map((x, i) => (
            <div id="row" key={i} className="zjazd">
              <span id="rowNr">
                {x.nr}. {x.czyOdpracowanie && "(odpracowanie)"}
              </span>
              <span id="rowZjazdOd">{formatujDate(x.dataOd)}</span>
              <span> - </span>
              <span id="rowZjazdDo">{formatujDate(x.dataDo)}</span>
              <ContextMenu
                items={[
                  {
                    title: "Usuń",
                    action: () => onUsun(x.nr),
                  },
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
          <KalendariumEdycja grupy={grupy} onZapisz={onZapisz} />
        </Drawer>
      </div>
    </div>
  );
}

export default Kalendarium;
