export interface UzytkownikState {
  zalogowano: boolean;
  email: string;
  imie: string;
  nazwisko: string;
}

const initState: UzytkownikState = {
  zalogowano: false,
  email: "",
  imie: "",
  nazwisko: "",
};

export default function uzytkownikReducer(
  state = initState,
  action: any
): UzytkownikState {
  switch (action.type) {
    case "ZALOGOWANO":
      return {
        ...state,
        zalogowano: true,
        email: action.payload.email,
        imie: action.payload.imie,
        nazwisko: action.payload.nazwisko,
      };
    case "WYLOGOWANO":
      return initState;
    default:
      return state;
  }
}
