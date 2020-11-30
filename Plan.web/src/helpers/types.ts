import { FormaLekcji, StopienStudiow, TrybStudiow } from "./enums";

export interface Grupa {
  nrGrupy: string;
  semestr: number;
  trybStudiow: TrybStudiow;
  stopienStudiow: StopienStudiow;
}

export interface GrupaWidok {
  numer: string;
  semestr: number;
  trybStudiow: TrybStudiow;
  stopienStudiow: StopienStudiow;
}

export interface ZjazdWidok {
  idZjazdu: number;
  dataOd: Date;
  dataDo: Date;
}

export interface WykladowcaWidok {
  id: number;
  nazwa: string;
  email: string;
}

export interface LekcjaWybor {
  idLekcji: number;
  godzinaOd: string;
  godzinaDo: string;
  forma: FormaLekcji;
  idPrzedmiotu: number;
  przedmiot: string;
  idSali: number;
  sala: string;
  idWykladowcy: number;
  wykladowca: string;
}
