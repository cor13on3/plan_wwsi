import {
  Button,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  TextField,
} from "@material-ui/core";
import React, { useState } from "react";
import { StopienStudiow, TrybStudiow } from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
import { Grupa } from "../../helpers/types";
import { ErrorStyle } from "../../styles/ErrorStyle";
import { GrupaEdycjaStyle } from "../../styles/GrupaEdycjaStyle";

interface Props {
  onZapisz: () => void;
}

const GrupaEdycja = (props: Props) => {
  const [grupa, setGrupa] = useState({
    trybStudiow: TrybStudiow.Niestacjonarne,
    stopienStudiow: StopienStudiow.Inzynierskie,
  } as Grupa);
  const [blad, setBlad] = useState("");

  function zapisz() {
    httpClient
      .POST("/api/grupa", {
        NrGrupy: grupa.nrGrupy,
        Semestr: grupa.semestr,
        TrybStudiow: grupa.trybStudiow,
        StopienStudiow: grupa.stopienStudiow,
      })
      .then(() => {
        setGrupa({
          trybStudiow: TrybStudiow.Niestacjonarne,
          stopienStudiow: StopienStudiow.Inzynierskie,
        } as Grupa);
        props.onZapisz();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  return (
    <GrupaEdycjaStyle>
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <p className="xl">NOWA GRUPA</p>
      <form>
        <TextField
          variant="outlined"
          label="Numer"
          id="NrGrupy"
          value={grupa.nrGrupy}
          onChange={(e) => setGrupa({ ...grupa, nrGrupy: e.target.value })}
          autoFocus
        />
        <TextField
          variant="outlined"
          placeholder="Semestr"
          id="SemestrGrupy"
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
            id="TrybStudiow"
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
            id="StopienStudiow"
          >
            <MenuItem value={StopienStudiow.Inzynierskie}>
              Inżynierskie
            </MenuItem>
            <MenuItem value={StopienStudiow.Magisterskie}>
              Magisterskie
            </MenuItem>
            <MenuItem value={StopienStudiow.Podyplomowe}>Podyplomowe</MenuItem>
          </Select>
        </FormControl>
        <Button variant="contained" color="secondary" onClick={zapisz}>
          ZAPISZ
        </Button>
      </form>
    </GrupaEdycjaStyle>
  );
};

export default GrupaEdycja;
