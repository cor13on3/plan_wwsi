export enum TrybStudiow {
  Stacjonarne = "Stacjonarne",
  Niestacjonarne = "Niestacjonarne",
}

export enum StopienStudiow {
  Inzynierskie = "Inzynierskie",
  Magisterskie = "Magisterskie",
  Podyplomowe = "Podyplomowe",
}

export enum FormaLekcji {
  Wyklad = "Wyklad",
  Cwiczenia = "Cwiczenia",
}

export interface PrzedmiotWidok {
  id: number;
  nazwa: string;
}

export interface SalaWidok {
  id: number;
  nazwa: string;
}

export interface ZjazdGrupyWidok {
  nr: number;
  dataOd: Date;
  dataDo: Date;
  czyOdpracowanie: boolean;
  idZjazdu: number;
}
