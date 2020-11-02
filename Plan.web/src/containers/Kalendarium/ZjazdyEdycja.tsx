import React, { useEffect, useState } from "react";
import { httpClient } from "../../helpers/httpClient";
import { ZjazdWidok } from "../../helpers/types";
import formatujDate from "../../helpers/formatujDate";
import moment from "moment";

interface ZjazdyEdycjaProps {
  onWybierz: (zjazd: ZjazdWidok) => void;
}

function ZjazdyEdycja({ onWybierz }: ZjazdyEdycjaProps) {
  const [dataOd, setDataOd] = useState(moment(new Date()).format("yyyy-MM-dd"));
  const [dataDo, setDataDo] = useState(moment(new Date()).format("yyyy-MM-dd"));
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
    <div>
      <h3>Zjazdy</h3>
      {lista.map((x) => (
        <div>
          <span>{formatujDate(x.dataOd)} - </span>
          <span>{formatujDate(x.dataDo)}</span>
          <button onClick={() => onWybierz(x)}>Wybierz</button>
        </div>
      ))}
      <div>
        <input
          type="date"
          value={dataOd.toString()}
          onChange={(e) => setDataOd(e.target.value)}
        />
        <span> - </span>
        <input
          type="date"
          value={dataDo.toString()}
          onChange={(e) => setDataDo(e.target.value)}
        />
        <button onClick={dodaj}>DODAJ</button>
      </div>
    </div>
  );
}

export default ZjazdyEdycja;
