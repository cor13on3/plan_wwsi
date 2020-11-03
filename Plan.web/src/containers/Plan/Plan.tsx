import React, { useEffect, useState } from "react";
import { FormaLekcji, TrybStudiow } from "../../helpers/enums";
import formatujGodzine from "../../helpers/formatujGodzine";
import { Blad, httpClient } from "../../helpers/httpClient";
import { GrupaWidok } from "../../helpers/types";
import LekcjaEdycja from "./LekcjaEdycja";
import "./Plan.css";

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
    "Standardowy" as "Standardowy" | "Odpracowania"
  );
  const [plan, setPlan] = useState([] as PlanDnia[]);
  const [blad, setBlad] = useState("");
  const [edytowanyDzien, setEdytowanyDzien] = useState(-1);

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
    if (grupa !== "Wybierz") {
      httpClient
        .GET(`/api/lekcja/daj-plan-na-tydzien/${grupa}`)
        .then((res: PlanDnia[]) => {
          setPlan(res);
        });
    }
  }, [grupa]);

  function dajLekcje(dzienTygodnia: number) {
    return plan
      .find((x) => x.dzienTygodnia === dzienTygodnia)
      ?.lekcje.map((l) => (
        <div>
          <p>
            Zjazdy:
            {l.zjazdy.join(", ")}
          </p>
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
      {trybStudiow !== "Wybierz" && grupa !== "Wybierz" && (
        <div>
          {trybStudiow === TrybStudiow.Niestacjonarne ? (
            <div className="tydzien">
              <div className="dzien">
                <span>Piątek</span>
                {dajLekcje(5)}
                <button onClick={() => setEdytowanyDzien(5)}>DODAJ</button>
              </div>
              <div className="dzien">
                <span>Sobota</span>
                {dajLekcje(6)}
                <button onClick={() => setEdytowanyDzien(6)}>DODAJ</button>
              </div>
              <div className="dzien">
                <span>Niedziela</span>
                {dajLekcje(0)}
                <button onClick={() => setEdytowanyDzien(0)}>DODAJ</button>
              </div>
            </div>
          ) : (
            <div>
              <span>Poniedziałek</span>
              <span>Wtorek</span>
              <span>Środa</span>
              <span>Czwartek</span>
              <span>Piątek</span>
            </div>
          )}
        </div>
      )}
      {edytowanyDzien >= 0 && (
        <LekcjaEdycja grupa={grupa} dzienTygodnia={edytowanyDzien} />
      )}
    </div>
  );
}

export default Plan;
