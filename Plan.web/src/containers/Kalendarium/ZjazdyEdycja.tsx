import React, { useEffect, useState } from "react";
import { httpClient } from "../../helpers/httpClient";
import { ZjazdWidok } from "../../helpers/types";
import formatujDate from "../../helpers/formatujDate";
import ZjazdyEdycjaStyle from "../../styles/ZjazdyEdycjaStyle";
import moment from "moment";
import { Button } from "@material-ui/core";
import DateFnsUtils from "@date-io/date-fns";
import {
  KeyboardDatePicker,
  MuiPickersUtilsProvider,
} from "@material-ui/pickers";

interface ZjazdyEdycjaProps {
  onWybierz: (zjazd: ZjazdWidok) => void;
}

function ZjazdyEdycja({ onWybierz }: ZjazdyEdycjaProps) {
  const [dataOd, setDataOd] = useState(moment(new Date()).format("yyyy-MM-DD"));
  const [dataDo, setDataDo] = useState(moment(new Date()).format("yyyy-MM-DD"));
  const [lista, setLista] = useState([] as ZjazdWidok[]);

  function odswiezListe() {
    httpClient.GET("/api/kalendarium/zjazdy").then((res: ZjazdWidok[]) => {
      setLista(res);
    });
  }

  useEffect(() => {
    odswiezListe();
  }, []);

  function dodaj() {
    httpClient
      .POST("/api/kalendarium/dodaj-zjazd", {
        DataOd: dataOd,
        DataDo: dataDo,
      })
      .then(() => {
        odswiezListe();
      });
  }

  return (
    <ZjazdyEdycjaStyle>
      <p className="xl">ZJAZDY</p>
      <div className="zjazdy">
        {lista.map((x, i) => (
          <div key={i} className="element">
            <span>{formatujDate(x.dataOd)}</span>
            <span> - </span>
            <span>{formatujDate(x.dataDo)}</span>
            <Button color="secondary" onClick={() => onWybierz(x)}>
              Wybierz
            </Button>
          </div>
        ))}
      </div>
      <div className="element-dodaj">
        <MuiPickersUtilsProvider utils={DateFnsUtils}>
          <KeyboardDatePicker
            disableToolbar
            variant="inline"
            format="yyyy-MM-dd"
            margin="normal"
            label="Data od"
            value={dataOd}
            onChange={(e, v) => v && setDataOd(v)}
          />
        </MuiPickersUtilsProvider>
        <span> - </span>
        <MuiPickersUtilsProvider utils={DateFnsUtils}>
          <KeyboardDatePicker
            disableToolbar
            variant="inline"
            format="yyyy-MM-dd"
            margin="normal"
            label="Data do"
            value={dataDo}
            onChange={(e, v) => v && setDataDo(v)}
          />
        </MuiPickersUtilsProvider>
        <Button color="secondary" onClick={dodaj}>
          DODAJ
        </Button>
      </div>
    </ZjazdyEdycjaStyle>
  );
}

export default ZjazdyEdycja;
