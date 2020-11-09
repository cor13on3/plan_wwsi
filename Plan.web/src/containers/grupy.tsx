import {
  Button,
  Drawer,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@material-ui/core";
import React, { useEffect, useState } from "react";
import ContextMenu from "../components/ContextMenu";
import { StopienStudiow, TrybStudiow } from "../helpers/enums";
import { Blad, httpClient } from "../helpers/httpClient";
import { GrupaWidok } from "../helpers/types";
import { GrupaEdycjaStyle } from "../styles/GrupaEdycjaStyle";
import "./Grupy.css";

interface Grupa {
  nrGrupy: string;
  semestr: number;
  trybStudiow: TrybStudiow;
  stopienStudiow: StopienStudiow;
}

function Grupy() {
  const [lista, setLista] = useState([] as GrupaWidok[]);
  const [blad, setBlad] = useState("");
  const [czyEdycja, setCzyEdycja] = useState(false);
  const [grupa, setGrupa] = useState({
    trybStudiow: TrybStudiow.Niestacjonarne,
    stopienStudiow: StopienStudiow.Inzynierskie,
  } as Grupa);

  function odswiezListe() {
    httpClient
      .GET("/api/grupa")
      .then((res: GrupaWidok[]) => setLista(res))
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  useEffect(() => {
    odswiezListe();
  }, []);

  function pokazEdycje() {
    setCzyEdycja(true);
  }

  function zapisz() {
    if (czyEdycja)
      httpClient
        .POST("/api/grupa", {
          NrGrupy: grupa.nrGrupy,
          Semestr: grupa.semestr,
          TrybStudiow: grupa.trybStudiow,
          StopienStudiow: grupa.stopienStudiow,
        })
        .then(() => {
          setCzyEdycja(false);
          setGrupa({
            trybStudiow: TrybStudiow.Niestacjonarne,
            stopienStudiow: StopienStudiow.Inzynierskie,
          } as Grupa);
          odswiezListe();
        })
        .catch((err: Blad) => setBlad(err.Tresc));
  }

  function usun(numer: string) {
    httpClient
      .DELETE(`/api/grupa/${numer}`)
      .then(() => {
        odswiezListe();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function dajStopien(stopien: StopienStudiow) {
    if (stopien === StopienStudiow.Inzynierskie) return "inżynierskie";
    return stopien.toLowerCase();
  }

  return (
    <div className="grupy">
      <div className="grupy_header">
        <span className="xxl">Zarządzanie grupami</span>
        <Button
          variant="contained"
          color="secondary"
          onClick={() => pokazEdycje()}
        >
          DODAJ
        </Button>
      </div>
      {blad && <p className="blad">{blad}</p>}
      <div className="lista">
        <div className="lista_header disabled">
          <span>NUMER</span>
          <span>SEMESTR</span>
          <span>STOPIEŃ</span>
          <span>TRYB</span>
        </div>
        {lista.map((grupa, i) => (
          <div className="grupa" key={i}>
            <span className="xl">{grupa.numer}</span>
            <span className="xl">{grupa.semestr}</span>
            <span className="xl">{dajStopien(grupa.stopienStudiow)}</span>
            <span className="xl">{grupa.trybStudiow.toLowerCase()}</span>
            <ContextMenu
              items={[{ title: "Usuń", action: () => usun(grupa.numer) }]}
            />
          </div>
        ))}
      </div>
      <Drawer
        open={czyEdycja}
        onClose={() => setCzyEdycja(false)}
        anchor="right"
      >
        <GrupaEdycjaStyle>
          <p className="xl">NOWA GRUPA</p>
          <form>
            <TextField
              variant="outlined"
              label="Numer"
              value={grupa.nrGrupy}
              onChange={(e) => setGrupa({ ...grupa, nrGrupy: e.target.value })}
              autoFocus
            />
            <TextField
              variant="outlined"
              placeholder="Semestr"
              value={grupa.semestr}
              onChange={(e) =>
                setGrupa({ ...grupa, semestr: Number.parseInt(e.target.value) })
              }
            />
            <FormControl variant="outlined">
              <InputLabel>Tryb studiów</InputLabel>
              <Select
                value={grupa.trybStudiow}
                onChange={(e) =>
                  setGrupa({
                    ...grupa,
                    trybStudiow: e.target.value as TrybStudiow,
                  })
                }
                label="Tryb studiów"
              >
                <MenuItem value={TrybStudiow.Niestacjonarne}>
                  Niestacjonarne
                </MenuItem>
                <MenuItem value={TrybStudiow.Stacjonarne}>Stacjonarne</MenuItem>
              </Select>
            </FormControl>
            <FormControl variant="outlined">
              <InputLabel>Stopień studiów</InputLabel>
              <Select
                value={grupa.stopienStudiow}
                onChange={(e) =>
                  setGrupa({
                    ...grupa,
                    stopienStudiow: e.target.value as StopienStudiow,
                  })
                }
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
            <Button variant="contained" color="secondary" onClick={zapisz}>
              ZAPISZ
            </Button>
          </form>
        </GrupaEdycjaStyle>
      </Drawer>
    </div>
  );
}

export default Grupy;
