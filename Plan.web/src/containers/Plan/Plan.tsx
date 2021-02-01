import React, { useEffect, useState } from "react";
import { FormaLekcji, TrybStudiow, ZjazdGrupyWidok } from "../../helpers/enums";
import formatujDate from "../../helpers/formatujDate";
import formatujGodzine from "../../helpers/formatujGodzine";
import { Blad, httpClient } from "../../helpers/httpClient";
import { GrupaWidok } from "../../helpers/types";
import LekcjaEdycja from "./LekcjaEdycja";
import "./Plan.css";
import dajDzienTygodnia from "../../helpers/dajDzienTygodnia";
import {
  Button,
  Drawer,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
} from "@material-ui/core";
import { ErrorStyle } from "../../styles/ErrorStyle";
import { DeleteOutline } from "@material-ui/icons";

interface LekcjaWidok {
  idLekcji: number;
  nazwa: string;
  wykladowca: string;
  forma: FormaLekcji;
  od: string;
  do: string;
  sala: string;
  czyOdpracowanie: boolean;
}

interface LekcjaWZjazdach {
  zjazdy: number[];
  lekcja: LekcjaWidok;
}

interface PlanDnia {
  dzienTygodnia: number;
  lekcje: LekcjaWZjazdach[];
}

function Plan() {
  const SELECT_PUSTY = "Wybierz";

  const [trybStudiow, setTrybStudiow] = useState(
    TrybStudiow.Niestacjonarne as TrybStudiow | string
  );
  const [grupy, setGrupy] = useState([] as GrupaWidok[]);
  const [grupa, setGrupa] = useState("Z715");
  const [tryb, setTryb] = useState(
    "Standardowy" as "Standardowy" | "Odpracowania"
  );
  const [plan, setPlan] = useState([] as PlanDnia[]);
  const [blad, setBlad] = useState("");
  const [edytowanyDzien, setEdytowanyDzien] = useState(-1);
  const [zjazdyOdpracowujace, setZjazdyOdpracowujace] = useState(
    [] as ZjazdGrupyWidok[]
  );
  const [wybranyZjazdOdpr, setWybranyZjazdOdpr] = useState(
    undefined as ZjazdGrupyWidok | undefined
  );

  useEffect(() => {
    if (tryb === "Odpracowania") {
      httpClient
        .GET(`/api/kalendarium/${grupa}`)
        .then((res: ZjazdGrupyWidok[]) => {
          const zjazdy = res.filter((x) => x.czyOdpracowanie);
          setZjazdyOdpracowujace(zjazdy);
          if (zjazdy.length > 0) setWybranyZjazdOdpr(zjazdy[0]);
        })
        .catch((err: Blad) => setBlad(err.Tresc));
    } else {
      setZjazdyOdpracowujace([]);
      setWybranyZjazdOdpr(undefined);
    }
  }, [grupa, tryb]);

  useEffect(() => {
    setPlan([]);
    setGrupa(SELECT_PUSTY);
    if (trybStudiow !== SELECT_PUSTY) {
      httpClient
        .GET(`/api/grupa/filtruj/${trybStudiow}`)
        .then((res: GrupaWidok[]) => {
          setGrupy(res);
        })
        .catch((err: Blad) => setBlad(err.Tresc));
    }
  }, [trybStudiow]);

  useEffect(() => {
    setPlan([]);
    if (grupa !== SELECT_PUSTY) {
      odswiezListe();
    }
    // eslint-disable-next-line
  }, [grupa, tryb, wybranyZjazdOdpr]);

  function odswiezListe() {
    if (tryb === "Odpracowania" && !wybranyZjazdOdpr) {
      setPlan([]);
      return;
    }
    const url =
      tryb === "Odpracowania"
        ? `/api/lekcja/daj-plan-odpracowania/${grupa}/${wybranyZjazdOdpr?.nr}`
        : `/api/lekcja/daj-plan-na-tydzien/${grupa}`;
    httpClient
      .GET(url)
      .then((res: PlanDnia[]) => {
        setPlan(res);
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function usun(idLekcji: number, zjazdy: number[]) {
    let promises: Promise<any>[] = [];
    zjazdy.forEach((z) => {
      promises.push(
        httpClient.POST("/api/lekcja/wypisz-grupe", {
          IdLekcji: idLekcji,
          NrGrupy: grupa,
          NrZjazdu: z,
          CzyOdpracowanie: tryb === "Odpracowania",
        })
      );
    });
    Promise.all(promises)
      .then(() => odswiezListe())
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function dajLekcje(dzienTygodnia: number) {
    var lekcje = plan.find((x) => x.dzienTygodnia === dzienTygodnia)?.lekcje;
    if (lekcje && lekcje.length > 0)
      return lekcje.map((l, i) => (
        <div className="lekcja" key={i}>
          <span>
            {formatujGodzine(l.lekcja.od)} - {formatujGodzine(l.lekcja.do)}
          </span>
          <span>
            {l.lekcja.nazwa} ({l.lekcja.forma.toString().toLowerCase()})
          </span>
          <span>{l.lekcja.wykladowca}</span>
          <span>Sala {l.lekcja.sala}</span>
          {tryb === "Standardowy" && (
            <div className="lekcja-zjazdy">
              <span>
                Zjazdy:
                {l.zjazdy.sort((a, b) => a - b).join(", ")}
              </span>
              <Button onClick={() => usun(l.lekcja.idLekcji, l.zjazdy)}>
                <DeleteOutline color="secondary" />
              </Button>
            </div>
          )}
          <p></p>
        </div>
      ));
    return <div />;
  }

  function onZapisz() {
    setEdytowanyDzien(-1);
    odswiezListe();
  }

  return (
    <div className="plan">
      {blad && <ErrorStyle>{blad}</ErrorStyle>}
      <div className="plan-header">
        <span className="xxl">Zarządzanie planem zajęć</span>
      </div>
      <div className="plan_filters">
        <FormControl variant="outlined">
          <InputLabel>Tryb studiów</InputLabel>
          <Select
            value={trybStudiow}
            onChange={(e) => setTrybStudiow(e.target.value as TrybStudiow)}
            label="Tryb studiów"
          >
            <MenuItem value={SELECT_PUSTY}>Wybierz</MenuItem>
            <MenuItem value={TrybStudiow.Niestacjonarne}>
              Niestacjonarne
            </MenuItem>
            <MenuItem value={TrybStudiow.Stacjonarne}>Stacjonarne</MenuItem>
          </Select>
        </FormControl>
        <FormControl variant="outlined">
          <InputLabel>Grupa</InputLabel>
          <Select
            label="Grupa"
            value={grupa}
            onChange={(e) => setGrupa(e.target.value as string)}
          >
            <MenuItem value={SELECT_PUSTY}>Wybierz</MenuItem>
            {grupy.map((x, i) => (
              <MenuItem key={i} value={x.numer}>
                {x.numer}
              </MenuItem>
            ))}
          </Select>
        </FormControl>
        <FormControl variant="outlined">
          <InputLabel>Tryb planu</InputLabel>
          <Select
            value={tryb}
            onChange={(e) =>
              setTryb(e.target.value as "Standardowy" | "Odpracowania")
            }
            label="Tryb planu"
          >
            <MenuItem value={SELECT_PUSTY}>Wybierz</MenuItem>
            <MenuItem value="Standardowy">Standardowy</MenuItem>
            <MenuItem value="Odpracowania">Odpracowania</MenuItem>
          </Select>
        </FormControl>
        {tryb === "Odpracowania" && zjazdyOdpracowujace.length > 0 && (
          <div className="plan_filters_odpr">
            <FormControl variant="outlined">
              <InputLabel>Zjazd</InputLabel>
              <Select
                value={wybranyZjazdOdpr?.nr}
                onChange={(e) =>
                  setWybranyZjazdOdpr(
                    zjazdyOdpracowujace.find((x) => x.nr === e.target.value)
                  )
                }
                label="Zjazd"
              >
                {zjazdyOdpracowujace.map((x, i) => (
                  <MenuItem value={x.nr} key={i}>
                    {x.nr}
                  </MenuItem>
                ))}
              </Select>
            </FormControl>
            {wybranyZjazdOdpr && (
              <span className="xl">
                ({formatujDate(wybranyZjazdOdpr.dataOd)} -{" "}
                {formatujDate(wybranyZjazdOdpr.dataDo)})
              </span>
            )}
          </div>
        )}
      </div>

      {trybStudiow !== SELECT_PUSTY && grupa !== SELECT_PUSTY && (
        <div>
          {trybStudiow === TrybStudiow.Niestacjonarne ? (
            <div className="tydzien3">
              <div className="dzien">
                <b>{dajDzienTygodnia(5)}</b>
                <div>{dajLekcje(5)}</div>
                <Button
                  variant="contained"
                  color="secondary"
                  className="dodajBtn"
                  onClick={() => setEdytowanyDzien(5)}
                >
                  +
                </Button>
              </div>
              <div className="dzien">
                <b>{dajDzienTygodnia(6)}</b>
                <div>{dajLekcje(6)}</div>
                <Button
                  variant="contained"
                  color="secondary"
                  className="dodajBtn"
                  onClick={() => setEdytowanyDzien(6)}
                >
                  +
                </Button>
              </div>
              <div className="dzien">
                <b>{dajDzienTygodnia(0)}</b>
                <div>{dajLekcje(0)}</div>
                <Button
                  variant="contained"
                  color="secondary"
                  className="dodajBtn"
                  onClick={() => setEdytowanyDzien(0)}
                >
                  +
                </Button>
              </div>
            </div>
          ) : (
            <div className="tydzien5">
              <div className="dzien">
                <b>{dajDzienTygodnia(1)}</b>
                {dajLekcje(1)}
                <Button
                  variant="contained"
                  color="secondary"
                  className="dodajBtn"
                  onClick={() => setEdytowanyDzien(1)}
                >
                  +
                </Button>
              </div>
              <div className="dzien">
                <b>{dajDzienTygodnia(2)}</b>
                {dajLekcje(2)}
                <Button
                  variant="contained"
                  color="secondary"
                  className="dodajBtn"
                  onClick={() => setEdytowanyDzien(2)}
                >
                  +
                </Button>
              </div>
              <div className="dzien">
                <b>{dajDzienTygodnia(3)}</b>
                {dajLekcje(3)}
                <Button
                  variant="contained"
                  color="secondary"
                  className="dodajBtn"
                  onClick={() => setEdytowanyDzien(3)}
                >
                  +
                </Button>
              </div>
              <div className="dzien">
                <b>{dajDzienTygodnia(4)}</b>
                {dajLekcje(4)}
                <Button
                  variant="contained"
                  color="secondary"
                  className="dodajBtn"
                  onClick={() => setEdytowanyDzien(4)}
                >
                  +
                </Button>
              </div>
              <div className="dzien">
                <b>{dajDzienTygodnia(5)}</b>
                {dajLekcje(5)}
                <Button
                  variant="contained"
                  color="secondary"
                  className="dodajBtn"
                  onClick={() => setEdytowanyDzien(5)}
                >
                  +
                </Button>
              </div>
            </div>
          )}
        </div>
      )}
      <Drawer
        anchor="right"
        open={edytowanyDzien >= 0}
        onClose={() => setEdytowanyDzien(-1)}
      >
        <LekcjaEdycja
          grupa={grupa}
          dzienTygodnia={edytowanyDzien}
          zjazdOdpracowywany={wybranyZjazdOdpr?.nr}
          onZapisz={onZapisz}
        />
      </Drawer>
    </div>
  );
}

export default Plan;
