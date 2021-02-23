import { Button, Checkbox, Drawer, TextField } from "@material-ui/core";
import React, { useState } from "react";
import formatujDate from "../../helpers/formatujDate";
import { Blad, httpClient } from "../../helpers/httpClient";
import { ZjazdWidok } from "../../helpers/types";
import { ErrorStyle } from "../../styles/ErrorStyle";
import KalendariumEdycjaStyle from "../../styles/KalendariumEdycjaStyle";
import ZjazdyEdycja from "./ZjazdyEdycja";

interface KalendariumEdycjaProps {
  grupy: string[];
  onZapisz: Function;
}

function KalendariumEdycja(props: KalendariumEdycjaProps) {
  const [nr, setNr] = useState(1);
  const [odpracowanie, setOdpracowanie] = useState(false);
  const [wybranyZjazd, setWybranyZjazd] = useState({} as ZjazdWidok);
  const [edycjaZjazdow, setEdycjaZjazdow] = useState(false);
  const [blad, setBlad] = useState("");

  function zapisz() {
    httpClient
      .POST("/api/kalendarium/przyporzadkuj-grupy-do-zjazdu", {
        Grupy: props.grupy,
        Zjazd: {
          NrZjazdu: nr,
          IdZjazdu: wybranyZjazd.idZjazdu,
          CzyOdpracowanie: odpracowanie,
        },
      })
      .then(() => {
        props.onZapisz();
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function onWybierz(wybrany: ZjazdWidok) {
    setWybranyZjazd(wybrany);
    setEdycjaZjazdow(false);
  }

  return (
    <KalendariumEdycjaStyle>
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <p className="xl">PRZYPISANIE ZJAZDU</p>
      <form>
        <TextField
          id="nrKolejny"
          label="Numer kolejny"
          variant="outlined"
          value={nr}
          onChange={(e) => setNr(Number.parseInt(e.target.value))}
        />
        <div className="data">
          <TextField
            disabled
            label="Data zjazdu"
            variant="outlined"
            value={`${formatujDate(wybranyZjazd.dataOd)} - ${formatujDate(
              wybranyZjazd.dataDo
            )}`}
            onClick={() => setEdycjaZjazdow(true)}
          />
          <Button color="secondary" onClick={() => setEdycjaZjazdow(true)}>
            WYBIERZ
          </Button>
        </div>
        <div>
          <Checkbox
            className="checkbox"
            checked={odpracowanie}
            onChange={(e) => setOdpracowanie(e.target.checked)}
          />
          <span>Czy odpracowanie</span>
        </div>
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
        open={edycjaZjazdow}
        onClose={() => setEdycjaZjazdow(false)}
        anchor="right"
      >
        <ZjazdyEdycja onWybierz={onWybierz} />
      </Drawer>
    </KalendariumEdycjaStyle>
  );
}

export default KalendariumEdycja;
