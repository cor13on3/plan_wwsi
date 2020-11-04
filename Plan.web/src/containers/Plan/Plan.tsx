import React, { useEffect, useState } from "react";
import { FormaLekcji, TrybStudiow, ZjazdGrupyWidok } from "../../helpers/enums";
import formatujDate from "../../helpers/formatujDate";
import formatujGodzine from "../../helpers/formatujGodzine";
import { Blad, httpClient } from "../../helpers/httpClient";
import { GrupaWidok } from "../../helpers/types";
import LekcjaEdycja from "./LekcjaEdycja";
import "./Plan.css";
import dajDzienTygodnia from "../../helpers/dajDzienTygodnia";

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
  const [trybStudiow, setTrybStudiow] = useState(
    TrybStudiow.Niestacjonarne as TrybStudiow | "Wybierz"
  );
  const [grupy, setGrupy] = useState([] as GrupaWidok[]);
  const [grupa, setGrupa] = useState("Z715");
  const [tryb, setTryb] = useState(
    "Wybierz" as "Wybierz" | "Standardowy" | "Odpracowania"
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
    if (trybStudiow !== "Wybierz") {
      httpClient
        .GET(`/api/grupa/${trybStudiow}`)
        .then((res: GrupaWidok[]) => {
          setGrupy(res);
        })
        .catch((err: Blad) => setBlad(err.Tresc));
    }
  }, [trybStudiow]);

  useEffect(() => {
    if (grupa !== "Wybierz" && tryb !== "Wybierz") {
      odswiezListe();
    }
    // eslint-disable-next-line
  }, [grupa, tryb, wybranyZjazdOdpr]);

  function odswiezListe() {
    const url =
      tryb === "Odpracowania" && wybranyZjazdOdpr
        ? `/api/lekcja/daj-plan-odpracowania/${grupa}/${wybranyZjazdOdpr.nr}`
        : `/api/lekcja/daj-plan-na-tydzien/${grupa}`;
    httpClient
      .GET(url)
      .then((res: PlanDnia[]) => {
        setPlan(res);
      })
      .catch((err: Blad) => setBlad(err.Tresc));
  }

  function dajLekcje(dzienTygodnia: number) {
    return plan
      .find((x) => x.dzienTygodnia === dzienTygodnia)
      ?.lekcje.map((l) => (
        <div className="lekcja">
          {tryb === "Standardowy" && (
            <>
              <p>
                Zjazdy:
                {l.zjazdy.join(", ")}
              </p>
            </>
          )}
          <p>
            {formatujGodzine(l.lekcja.od)} - {formatujGodzine(l.lekcja.do)}
          </p>
          <p>
            {l.lekcja.nazwa} ({l.lekcja.forma.toString().toLowerCase()})
          </p>
          <p>{l.lekcja.wykladowca}</p>
          <p>{l.lekcja.sala}</p>
        </div>
      ));
  }

  function onZapisz() {
    setEdytowanyDzien(-1);
    odswiezListe();
  }

  return (
    <div>
      <h1>PLAN</h1>
      {blad && <p className="blad">{blad}</p>}
      <span>Tryb studiów </span>
      <select
        value={trybStudiow}
        onChange={(e) => setTrybStudiow(e.target.value as TrybStudiow)}
      >
        <option value="Wybierz">Wybierz</option>
        <option value={TrybStudiow.Niestacjonarne}>Niestacjonarne</option>
        <option value={TrybStudiow.Stacjonarne}>Stacjonarne</option>
      </select>
      <span>Grupa </span>
      <select value={grupa} onChange={(e) => setGrupa(e.target.value)}>
        <option value="Wybierz">Wybierz</option>
        {grupy.map((x) => (
          <option value={x.numer}>{x.numer}</option>
        ))}
      </select>
      <span>Tryb planu </span>
      <select
        value={tryb}
        onChange={(e) =>
          setTryb(e.target.value as "Standardowy" | "Odpracowania")
        }
      >
        <option value="Standardowy">Standardowy</option>
        <option value="Odpracowania">Odpracowania</option>
      </select>
      {tryb === "Odpracowania" && zjazdyOdpracowujace.length > 0 && (
        <>
          <span>Odpracowanie zjadu </span>
          <select
            value={wybranyZjazdOdpr?.nr}
            onChange={(e) =>
              setWybranyZjazdOdpr(
                zjazdyOdpracowujace.find(
                  (x) => x.nr === Number.parseInt(e.target.value)
                )
              )
            }
          >
            {zjazdyOdpracowujace.map((x) => (
              <option value={x.nr}>{x.nr}</option>
            ))}
          </select>
          {wybranyZjazdOdpr && (
            <span>
              ({formatujDate(wybranyZjazdOdpr.dataOd)} -{" "}
              {formatujDate(wybranyZjazdOdpr.dataDo)})
            </span>
          )}
        </>
      )}
      {trybStudiow !== "Wybierz" && grupa !== "Wybierz" && (
        <div>
          {trybStudiow === TrybStudiow.Niestacjonarne ? (
            <div className="tydzien3">
              <div className="dzien">
                <span>{dajDzienTygodnia(5)}</span>
                {dajLekcje(5)}
                <button onClick={() => setEdytowanyDzien(5)}>DODAJ</button>
              </div>
              <div className="dzien">
                <span>{dajDzienTygodnia(6)}</span>
                {dajLekcje(6)}
                <button onClick={() => setEdytowanyDzien(6)}>DODAJ</button>
              </div>
              <div className="dzien">
                <span>{dajDzienTygodnia(0)}</span>
                {dajLekcje(0)}
                <button onClick={() => setEdytowanyDzien(0)}>DODAJ</button>
              </div>
            </div>
          ) : (
            <div className="tydzien5">
              <div className="dzien">
                <span>{dajDzienTygodnia(1)}</span>
                {dajLekcje(1)}
                <button onClick={() => setEdytowanyDzien(1)}>DODAJ</button>
              </div>
              <div className="dzien">
                <span>{dajDzienTygodnia(2)}</span>
                {dajLekcje(2)}
                <button onClick={() => setEdytowanyDzien(2)}>DODAJ</button>
              </div>
              <div className="dzien">
                <span>{dajDzienTygodnia(3)}</span>
                {dajLekcje(3)}
                <button onClick={() => setEdytowanyDzien(3)}>DODAJ</button>
              </div>
              <div className="dzien">
                <span>{dajDzienTygodnia(4)}</span>
                {dajLekcje(4)}
                <button onClick={() => setEdytowanyDzien(4)}>DODAJ</button>
              </div>
              <div className="dzien">
                <span>{dajDzienTygodnia(5)}</span>
                {dajLekcje(5)}
                <button onClick={() => setEdytowanyDzien(5)}>DODAJ</button>
              </div>
            </div>
          )}
        </div>
      )}
      {edytowanyDzien >= 0 && (
        <LekcjaEdycja
          grupa={grupa}
          dzienTygodnia={edytowanyDzien}
          zjazdOdpracowywany={wybranyZjazdOdpr?.nr}
          onZapisz={onZapisz}
        />
      )}
    </div>
  );
}

export default Plan;
