import { Button } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import dajDzienTygodnia from "../../helpers/dajDzienTygodnia";
import { TrybStudiow } from "../../helpers/enums";
import { Blad, httpClient } from "../../helpers/httpClient";
import { LekcjaWybor } from "../../helpers/types";
import { ErrorStyle } from "../../styles/ErrorStyle";
import WyborLekcjiStyle from "../../styles/WyborLekcjiStyle";

interface WyborLekcjiProps {
  tryb: TrybStudiow;
  semestr: number;
  dzienTygodnia: number;
  onWybierz: (wybrany: LekcjaWybor) => void;
}

function WyborLekcji(props: WyborLekcjiProps) {
  const [lista, setLista] = useState([] as LekcjaWybor[]);
  const [blad, setBlad] = useState("");

  useEffect(() => {
    odswiezListe();
    // eslint-disable-next-line
  }, []);

  function odswiezListe() {
    httpClient
      .GET(
        `/api/lekcja/daj-lekcje-dzien-tyg/${props.tryb}/${props.semestr}/${props.dzienTygodnia}`
      )
      .then((res: LekcjaWybor[]) => {
        setLista(res);
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function onWybierz(wybrany: LekcjaWybor) {
    props.onWybierz(wybrany);
  }

  return (
    <WyborLekcjiStyle>
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <span className="xl">
        ZAJĘCIA / {dajDzienTygodnia(props.dzienTygodnia).toUpperCase()} / SEM{" "}
        {props.semestr}
      </span>
      <div>
        {lista.map((x) => (
          <div className="element">
            <div className="element_opis">
              <div>
                <span className="xl">{x.przedmiot} </span>
                <span>{x.forma}</span>
              </div>
              <span>{x.wykladowca}</span>
              <div>
                <span>Sala {x.sala},</span>
                <span> {x.godzinaOd} - </span>
                <span>{x.godzinaDo}</span>
              </div>
            </div>
            <Button color="secondary" onClick={() => onWybierz(x)}>
              WYBIERZ
            </Button>
          </div>
        ))}
      </div>
    </WyborLekcjiStyle>
  );
}

export default WyborLekcji;
