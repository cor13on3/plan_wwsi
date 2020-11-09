import { StopienStudiow, TrybStudiow } from "./enums";

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
