import { Selector } from "testcafe";

async function dodajUzytkownikaTestowego() {
  const fetch = require("node-fetch");
  await fetch("http://localhost:60211/api/uzytkownik/dodaj", {
    method: "POST",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      Imie: "Alojzy",
      Nazwisko: "Testowy",
      Email: "admin",
      Haslo: "Dupa1234!",
    }),
    credentials: "include",
  });
}

fixture`Administrowanie planem zajęć`.page`http://localhost:3000`.before(
  async () => {
    await dodajUzytkownikaTestowego();
  }
);

test("Próba logowania z błędnymi danymi", async (t) => {
  await t
    .typeText("#email", "admin1", { replace: true })
    .typeText("#haslo", "Dupa1234!1", { replace: true })
    .click(Selector("button"))
    .expect(Selector("#blad").textContent)
    .eql("Podano nieprawidłowy e-mail lub hasło.")
    .expect(Selector(".zalogowany").exists)
    .notOk();
});

test("Logowanie", async (t) => {
  await t
    .typeText("#email", "admin", { replace: true })
    .typeText("#haslo", "Dupa1234!", { replace: true })
    .click(Selector("button"))
    .expect(Selector(".zalogowany").child("p").textContent)
    .eql("Zalogowano jako Alojzy Testowy");
});
