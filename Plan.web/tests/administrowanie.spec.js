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

test("Wyświetla okno logowania", async (t) => {});

test("Nie wpuszcza do aplikacji bez zalogowania", async (t) => {});

test("Błąd przy nieudanym logowaniu", async (t) => {
  await t
    .typeText("#email", "admin1", { replace: true })
    .typeText("#haslo", "Dupa1234!1", { replace: true })
    .click(Selector("button"))
    .expect(Selector("#blad").textContent)
    .eql("Podano nieprawidłowy e-mail lub hasło.")
    .expect(Selector(".zalogowany").exists)
    .notOk();
});

test("Dane użytkownika po udanym logowaniu", async (t) => {
  await t
    .typeText("#email", "admin", { replace: true })
    .typeText("#haslo", "Dupa1234!", { replace: true })
    .click(Selector("button"))
    .expect(Selector(".zalogowany").child("p").textContent)
    .eql("Zalogowano jako Alojzy Testowy");
});

test("Pusta lista grup", async (t) => {
  await t
    .click(Selector("a").withText("Grupy"))
    .expect(Selector("#row").count)
    .eql(0);
});

test("Formularz dodawania grupy działa prawidłowo", async (t) => {
  await t
    .click(Selector("button").withText("DODAJ"))
    .expect(Selector("p").withText("NOWA GRUPA").exists)
    .ok()
    .typeText(Selector("#NrGrupy"), "Z715")
    .typeText(Selector("#SemestrGrupy"), "7")
    .click(Selector("#TrybStudiow"))
    .click(Selector("li").withText("Niestacjonarne"))
    .click(Selector("#StopienStudiow"))
    .click(Selector("li").withText("Inżynierskie"))
    .click(Selector("button").withText("ZAPISZ"))
    .expect(Selector("p").withText("NOWA GRUPA").exists)
    .notOk()

    .click(Selector("a").withText("Grupy"))
    .click(Selector("button").withText("DODAJ"))
    .expect(Selector("p").withText("NOWA GRUPA").exists)
    .ok()
    .typeText(Selector("#NrGrupy"), "D501")
    .typeText(Selector("#SemestrGrupy"), "5")
    .click(Selector("#TrybStudiow"))
    .click(Selector("li").withText("Stacjonarne"))
    .click(Selector("#StopienStudiow"))
    .click(Selector("li").withText("Magisterskie"))
    .click(Selector("button").withText("ZAPISZ"))
    .expect(Selector("p").withText("NOWA GRUPA").exists)
    .notOk();
});

test("Lista grup zawiera poprawne dane", async (t) => {
  await t
    .expect(Selector("#row").count)
    .eql(2)
    .expect(Selector("#rowNumer").nth(0).textContent)
    .eql("Z715")
    .expect(Selector("#rowSemestr").nth(0).textContent)
    .eql("7")
    .expect(Selector("#rowStopien").nth(0).textContent)
    .eql("inżynierskie")
    .expect(Selector("#rowTryb").nth(0).textContent)
    .eql("niestacjonarne")
    .expect(Selector("#rowNumer").nth(1).textContent)
    .eql("D501")
    .expect(Selector("#rowSemestr").nth(1).textContent)
    .eql("5")
    .expect(Selector("#rowStopien").nth(1).textContent)
    .eql("magisterskie")
    .expect(Selector("#rowTryb").nth(1).textContent)
    .eql("stacjonarne");
});

test("Wyszukiwanie grup działa prawidłowo", async (t) => {
  await t
    .expect(Selector("#row").count)
    .eql(2)
    .typeText(Selector("#szukajka"), "715")
    .expect(Selector("#row").count)
    .eql(1)
    .expect(Selector("#rowNumer").nth(0).textContent)
    .eql("Z715")
    .selectText(Selector("#szukajka"))
    .pressKey("backspace")
    .expect(Selector("#row").count)
    .eql(2);
});

test("Pusta lista wykładowców", async (t) => {
  await t
    .click(Selector("a").withText("Kadra"))
    .expect(Selector("#row").count)
    .eql(0);
});

test("Formularz dodawania wykładowcy działa prawidłowo", async (t) => {
  await t
    .click(Selector("button").withText("DODAJ"))
    .expect(Selector("p").withText("NOWY WYKŁADOWCA").exists)
    .ok()
    .typeText(Selector("#nazwisko"), "Kowalski")
    .typeText(Selector("#imie"), "Adam")
    .typeText(Selector("#tytul"), "inż.")
    .click(Selector("#specjalizacje"))
    .typeText(Selector("#specjalizacja"), "Programowanie")
    .click(Selector("#specjalizacjaDodaj"))
    .click(Selector('[type="checkbox"]').nth(0))
    .click(Selector("button").withText("WYBIERZ"))
    .typeText(Selector("#email"), "a.kowalski@wwsi.edu.pl")
    .click(Selector("button").withText("ZAPISZ"))
    .expect(Selector("p").withText("NOWY WYKŁADOWCA").exists)
    .notOk()

    .click(Selector("button").withText("DODAJ"))
    .expect(Selector("p").withText("NOWY WYKŁADOWCA").exists)
    .ok()
    .typeText(Selector("#nazwisko"), "Malinowska")
    .typeText(Selector("#imie"), "Anna")
    .typeText(Selector("#tytul"), "mgr")
    .click(Selector("#specjalizacje"))
    .typeText(Selector("#specjalizacja"), "Finanse")
    .click(Selector("#specjalizacjaDodaj"))
    .typeText(Selector("#specjalizacja"), "Matematyka")
    .click(Selector("#specjalizacjaDodaj"))
    .click(Selector('[type="checkbox"]').nth(1))
    .click(Selector('[type="checkbox"]').nth(2))
    .click(Selector("button").withText("WYBIERZ"))
    .expect(Selector("#specjalizacje").value)
    .eql("Finanse, Matematyka")
    .typeText(Selector("#email"), "a.malinowska@wwsi.edu.pl")
    .click(Selector("button").withText("ZAPISZ"))
    .expect(Selector("p").withText("NOWY WYKŁADOWCA").exists)
    .notOk();
});

test("Lista wykładowców zawiera poprawne dane", async (t) => {
  await t
    .expect(Selector("#row").count)
    .eql(2)
    .expect(Selector("#rowNazwa").nth(0).innerText)
    .eql("inż. A. Kowalski")
    .expect(Selector("#rowEmail").nth(0).innerText)
    .eql("a.kowalski@wwsi.edu.pl")
    .expect(Selector("#rowNazwa").nth(1).innerText)
    .eql("mgr A. Malinowska")
    .expect(Selector("#rowEmail").nth(1).innerText)
    .eql("a.malinowska@wwsi.edu.pl");
});

test("Wyszukiwanie wykładowców działa prawidłowo", async (t) => {
  await t
    .expect(Selector("#row").count)
    .eql(2)
    .typeText(Selector("#szukajka"), "Kowal")
    .expect(Selector("#row").count)
    .eql(1)
    .expect(Selector("#rowNazwa").nth(0).textContent)
    .eql("inż. A. Kowalski")
    .selectText(Selector("#szukajka"))
    .pressKey("backspace")
    .expect(Selector("#row").count)
    .eql(2);
});

test("Puste okno kalendarium", async (t) => {
  await t
    .click(Selector("a").withText("Kalendarium"))
    .expect(Selector("#tytul").innerText)
    .eql("Zarządzanie zjazdami")
    .expect(Selector("#row").count)
    .eql(0);
});

test("Wyświetla grupy dla wybranych kryteriów", async (t) => {
  await t
    .expect(Selector("#zjazdyOpis").innerText)
    .eql("Brak grup dla wybranych kryteriów")
    .click(Selector("#stopienStudiow"))
    .click(Selector("li").withText("Inżynierskie"))
    .click(Selector("#trybStudiow"))
    .click(Selector("li").withText("Niestacjonarne"))
    .click(Selector("#semestr"))
    .click(Selector("li").withText("7"))
    .expect(Selector("#zjazdyOpis").innerText)
    .eql(
      "Kalendarium dla 7. semestru studiów Inzynierskie (Niestacjonarne) (grupy: Z715)"
    );
});

test("Formularz dodawania i przypisania zjazdu działa prawidłowo", async (t) => {
  await t
    .click(Selector("button").withText("PRZYPISZ ZJAZD"))
    .expect(Selector("p").withText("PRZYPISANIE ZJAZDU").exists)
    .ok()
    .typeText(Selector("#nrKolejny"), "1", { replace: true })
    .click(Selector("button").withText("WYBIERZ"))
    .typeText(Selector("#zjazdOd"), "2020-10-02", { replace: true })
    .typeText(Selector("#zjazdDo"), "2020-10-04", { replace: true })
    .click(Selector("button").withText("DODAJ"))
    .click(Selector("#zjazdWybierz").nth(0))
    .click(Selector("button").withText("ZAPISZ"))

    .click(Selector("button").withText("PRZYPISZ ZJAZD"))
    .expect(Selector("p").withText("PRZYPISANIE ZJAZDU").exists)
    .ok()
    .typeText(Selector("#nrKolejny"), "2", { replace: true })
    .click(Selector("button").withText("WYBIERZ"))
    .typeText(Selector("#zjazdOd"), "2020-10-16", { replace: true })
    .typeText(Selector("#zjazdDo"), "2020-10-18", { replace: true })
    .click(Selector("button").withText("DODAJ"))
    .click(Selector("#zjazdWybierz").nth(1))
    .click(Selector("button").withText("ZAPISZ"))

    .click(Selector("button").withText("PRZYPISZ ZJAZD"))
    .expect(Selector("p").withText("PRZYPISANIE ZJAZDU").exists)
    .ok()
    .typeText(Selector("#nrKolejny"), "3", { replace: true })
    .click(Selector("button").withText("WYBIERZ"))
    .typeText(Selector("#zjazdOd"), "2020-10-30", { replace: true })
    .typeText(Selector("#zjazdDo"), "2020-10-31", { replace: true })
    .click(Selector("button").withText("DODAJ"))
    .click(Selector("#zjazdWybierz").nth(2))
    .click(Selector("button").withText("ZAPISZ"))

    .click(Selector("button").withText("PRZYPISZ ZJAZD"))
    .expect(Selector("p").withText("PRZYPISANIE ZJAZDU").exists)
    .ok()
    .typeText(Selector("#nrKolejny"), "3", { replace: true })
    .click(Selector("button").withText("WYBIERZ"))
    .typeText(Selector("#zjazdOd"), "2020-11-06", { replace: true })
    .typeText(Selector("#zjazdDo"), "2020-11-08", { replace: true })
    .click(Selector("button").withText("DODAJ"))
    .click(Selector("#zjazdWybierz").nth(3))
    .click(Selector('[type="checkbox"]'))
    .click(Selector("button").withText("ZAPISZ"));
});

test("Lista wyświetla poprawne dane dla podanych kryteriów", async (t) => {
  await t
    .expect(Selector("#row").count)
    .eql(4)
    .expect(Selector("#rowNr").nth(0).innerText)
    .eql("1.")
    .expect(Selector("#rowZjazdOd").nth(0).innerText)
    .eql("02/10/2020")
    .expect(Selector("#rowZjazdDo").nth(0).innerText)
    .eql("04/10/2020")
    .expect(Selector("#rowNr").nth(1).innerText)
    .eql("2.")
    .expect(Selector("#rowZjazdOd").nth(1).innerText)
    .eql("16/10/2020")
    .expect(Selector("#rowZjazdDo").nth(1).innerText)
    .eql("18/10/2020")
    .expect(Selector("#rowNr").nth(2).innerText)
    .eql("3.")
    .expect(Selector("#rowZjazdOd").nth(2).innerText)
    .eql("30/10/2020")
    .expect(Selector("#rowZjazdDo").nth(2).innerText)
    .eql("31/10/2020")
    .expect(Selector("#rowNr").nth(3).innerText)
    .eql("3. (odpracowanie)")
    .expect(Selector("#rowZjazdOd").nth(3).innerText)
    .eql("06/11/2020")
    .expect(Selector("#rowZjazdDo").nth(3).innerText)
    .eql("08/11/2020")
    .click(Selector("#semestr"))
    .click(Selector("li").withText("5"))
    .expect(Selector("#row").count)
    .eql(0)
    .click(Selector("#semestr"))
    .click(Selector("li").withText("7"))
    .expect(Selector("#row").count)
    .eql(4);
});

test("Puste okno planu zajęć, kryteria planu działają prawidłowo", async (t) => {
  await t
    .click(Selector("a").withText("Plan zajęć"))
    .expect(Selector("#tytul").innerText)
    .eql("Zarządzanie planem zajęć")
    .expect(Selector("#lekcja").count)
    .eql(0)
    .click(Selector("#trybStudiow"))
    .click(Selector("li").withText("Niestacjonarne"))
    .click(Selector("#grupa"))
    .click(Selector("li").withText("Z715"))
    .click(Selector("#trybPlanu"))
    .click(Selector("li").withText("Standardowy"))
    .expect(Selector('[class="dzien-lekcje"]').count)
    .eql(3)
    .click(Selector("#trybPlanu"))
    .click(Selector("li").withText("Odpracowania"))
    .click(Selector("#zjazdOdpr"))
    .click(Selector("li").withText("3"))
    .expect(Selector("#dataOdpr").innerText)
    .contains("06/11/2020 - 08/11/2020")
    .click(Selector("#trybPlanu"))
    .click(Selector("li").withText("Standardowy"))
    .expect(Selector("#dataOdpr").exists)
    .notOk()
    .click(Selector("#trybStudiow"))
    .click(Selector("li").withText("Stacjonarne"))
    .click(Selector("#grupa"))
    .click(Selector("li").withText("D501"))
    .expect(Selector('[class="dzien-lekcje"]').count)
    .eql(5);
});

test("Formularz dodawania zajęć działa prawidłowo", async (t) => {
  await t
    .click(Selector("#trybStudiow"))
    .click(Selector("li").withText("Niestacjonarne"))
    .click(Selector("#grupa"))
    .click(Selector("li").withText("Z715"))
    .click(Selector("#trybPlanu"))
    .click(Selector("li").withText("Standardowy"))
    // piątek, pierwsza lekcja
    .click(Selector("button").withText("+").nth(0))
    .expect(Selector("p").withText("Z715 / PIĄTEK").exists)
    .ok()
    .click(Selector("#przedmiot"))
    .expect(Selector("span").withText("PRZEDMIOTY").exists)
    .ok()
    .typeText(Selector("#przedmiotNazwa"), "Programowanie", { replace: true })
    .click(Selector("button").withText("DODAJ"))
    .click(Selector("button").withText("WYBIERZ").nth(0))
    .expect(Selector("#przedmiot").value)
    .eql("Programowanie")
    .click(Selector("#wykladowca"))
    .click(Selector("li").withText("Kowalski"))
    .expect(Selector("#wykladowca").value)
    .eql("inż. A. Kowalski")
    .click(Selector("#sala"))
    .expect(Selector("span").withText("SALE").exists)
    .ok()
    .typeText(Selector("#salaNazwa"), "208", { replace: true })
    .click(Selector("button").withText("DODAJ"))
    .click(Selector("button").withText("WYBIERZ").nth(0))
    .expect(Selector("#sala").value)
    .eql("208")
    .click(Selector("#zjazdy"))
    .click(Selector("li").withText("1"))
    .click(Selector("li").withText("3"))
    .click(Selector("#zjazdy"))
    .typeText(Selector("#lekcjaOd"), "08:00", { replace: true })
    .typeText(Selector("#lekcjaDo"), "09:35", { replace: true })
    .click(Selector("#lekcjaForma"))
    .click(Selector("li").withText("Ćwiczenie"))
    .click(Selector("button").withText("ZAPISZ"))
    .expect(Selector("p").withText("Z715 / PIĄTEK").exists)
    .notOk()
    // druga lekcja w piątek
    .click(Selector("button").withText("+").nth(0))
    .expect(Selector("p").withText("Z715 / PIĄTEK").exists)
    .ok()
    .click(Selector("#przedmiot"))
    .expect(Selector("span").withText("PRZEDMIOTY").exists)
    .ok()
    .typeText(Selector("#przedmiotNazwa"), "Przedsiębiorczość", {
      replace: true,
    })
    .click(Selector("button").withText("DODAJ"))
    .click(Selector("button").withText("WYBIERZ").nth(1))
    .expect(Selector("#przedmiot").value)
    .eql("Przedsiębiorczość")
    .click(Selector("#wykladowca"))
    .click(Selector("li").withText("Malinowska"))
    .expect(Selector("#wykladowca").value)
    .eql("mgr A. Malinowska")
    .click(Selector("#sala"))
    .expect(Selector("span").withText("SALE").exists)
    .ok()
    .typeText(Selector("#salaNazwa"), "210", { replace: true })
    .click(Selector("button").withText("DODAJ"))
    .click(Selector("button").withText("WYBIERZ").nth(1))
    .expect(Selector("#sala").value)
    .eql("210")
    .click(Selector("#zjazdy"))
    .click(Selector("li").withText("1"))
    .click(Selector("li").withText("2"))
    .click(Selector("li").withText("3"))
    .click(Selector("#zjazdy"))
    .typeText(Selector("#lekcjaOd"), "09:45", { replace: true })
    .typeText(Selector("#lekcjaDo"), "11:15", { replace: true })
    .click(Selector("#lekcjaForma"))
    .click(Selector("li").withText("Wykład"))
    .click(Selector("button").withText("ZAPISZ"))
    .expect(Selector("p").withText("Z715 / PIĄTEK").exists)
    .notOk()
    // pierwsza lekcja w sobotę
    .click(Selector("button").withText("+").nth(1))
    .expect(Selector("p").withText("Z715 / SOBOTA").exists)
    .ok()
    .click(Selector("#przedmiot"))
    .expect(Selector("span").withText("PRZEDMIOTY").exists)
    .ok()
    .typeText(Selector("#przedmiotNazwa"), "Algebra liniowa", {
      replace: true,
    })
    .click(Selector("button").withText("DODAJ"))
    .click(Selector("button").withText("WYBIERZ").nth(2))
    .expect(Selector("#przedmiot").value)
    .eql("Algebra liniowa")
    .click(Selector("#wykladowca"))
    .click(Selector("li").withText("Kowalski"))
    .expect(Selector("#wykladowca").value)
    .eql("inż. A. Kowalski")
    .click(Selector("#sala"))
    .expect(Selector("span").withText("SALE").exists)
    .ok()
    .typeText(Selector("#salaNazwa"), "110", { replace: true })
    .click(Selector("button").withText("DODAJ"))
    .click(Selector("button").withText("WYBIERZ").nth(2))
    .expect(Selector("#sala").value)
    .eql("110")
    .click(Selector("#zjazdy"))
    .click(Selector("li").withText("1"))
    .click(Selector("li").withText("2"))
    .click(Selector("li").withText("3"))
    .click(Selector("#zjazdy"))
    .typeText(Selector("#lekcjaOd"), "08:00", { replace: true })
    .typeText(Selector("#lekcjaDo"), "09:35", { replace: true })
    .click(Selector("#lekcjaForma"))
    .click(Selector("li").withText("Wykład"))
    .click(Selector("button").withText("ZAPISZ"))
    .expect(Selector("p").withText("Z715 / SOBOTA").exists)
    .notOk()
    // dodanie odpracowania
    .click(Selector("#trybPlanu"))
    .click(Selector("li").withText("Odpracowania"))
    .click(Selector("#zjazdOdpr"))
    .click(Selector("li").withText("3"))
    .click(Selector("button").withText("+").nth(1))
    .expect(Selector("p").withText("Z715 / SOBOTA").exists)
    .ok()
    .click(Selector("#przedmiot"))
    .expect(Selector("span").withText("PRZEDMIOTY").exists)
    .ok()
    .click(Selector("button").withText("WYBIERZ").nth(0))
    .expect(Selector("#przedmiot").value)
    .eql("Programowanie")
    .click(Selector("#wykladowca"))
    .click(Selector("li").withText("Kowalski"))
    .expect(Selector("#wykladowca").value)
    .eql("inż. A. Kowalski")
    .click(Selector("#sala"))
    .expect(Selector("span").withText("SALE").exists)
    .ok()
    .click(Selector("button").withText("WYBIERZ").nth(0))
    .expect(Selector("#sala").value)
    .eql("208")
    .expect(Selector("#zjazdy").exists)
    .notOk()
    .typeText(Selector("#lekcjaOd"), "08:00", { replace: true })
    .typeText(Selector("#lekcjaDo"), "09:35", { replace: true })
    .click(Selector("#lekcjaForma"))
    .click(Selector("li").withText("Ćwiczenie"))
    .click(Selector("button").withText("ZAPISZ"))
    .expect(Selector("p").withText("Z715 / SOBOTA").exists)
    .notOk();
});

test("Plan zajęć zawiera poprawne dane", async (t) => {
  await t
    .click(Selector("#trybPlanu"))
    .click(Selector("li").withText("Standardowy"))
    .expect(Selector("#lekcja").count)
    .eql(3)
    .expect(Selector('[class="dzien-lekcje"]').nth(0).child("#lekcja").count)
    .eql(2)
    .expect(Selector('[class="dzien-lekcje"]').nth(1).child("#lekcja").count)
    .eql(1)
    .expect(Selector('[class="dzien-lekcje"]').nth(2).child("#lekcja").count)
    .eql(0)
    .expect(Selector("#lekcja").nth(0).innerText)
    .contains("Zjazdy: 1, 3")
    .expect(Selector("#lekcja").nth(0).innerText)
    .contains("08:00 - 09:35")
    .expect(Selector("#lekcja").nth(0).innerText)
    .contains("Programowanie (cwiczenia)")
    .expect(Selector("#lekcja").nth(0).innerText)
    .contains("inż. A Kowalski")
    .expect(Selector("#lekcja").nth(0).innerText)
    .contains("Sala 208")

    .expect(Selector("#lekcja").nth(1).innerText)
    .contains("Zjazdy: 1, 2, 3")
    .expect(Selector("#lekcja").nth(1).innerText)
    .contains("09:45 - 11:15")
    .expect(Selector("#lekcja").nth(1).innerText)
    .contains("Przedsiębiorczość (wyklad)")
    .expect(Selector("#lekcja").nth(1).innerText)
    .contains("mgr A Malinowska")
    .expect(Selector("#lekcja").nth(1).innerText)
    .contains("Sala 210")

    .expect(Selector("#lekcja").nth(2).innerText)
    .contains("Zjazdy: 1, 2, 3")
    .expect(Selector("#lekcja").nth(2).innerText)
    .contains("08:00 - 09:35")
    .expect(Selector("#lekcja").nth(2).innerText)
    .contains("Algebra liniowa (wyklad)")
    .expect(Selector("#lekcja").nth(2).innerText)
    .contains("inż. A Kowalski")
    .expect(Selector("#lekcja").nth(2).innerText)
    .contains("Sala 110")
    // sprawdzenie planu odpracowania
    .click(Selector("#trybPlanu"))
    .click(Selector("li").withText("Odpracowania"))
    .click(Selector("#zjazdOdpr"))
    .click(Selector("li").withText("3"))
    .expect(Selector('[class="dzien-lekcje"]').nth(0).child("#lekcja").count)
    .eql(0)
    .expect(Selector('[class="dzien-lekcje"]').nth(1).child("#lekcja").count)
    .eql(1)
    .expect(Selector('[class="dzien-lekcje"]').nth(2).child("#lekcja").count)
    .eql(0)
    .expect(Selector("#lekcja").nth(0).innerText)
    .contains("08:00 - 09:35")
    .expect(Selector("#lekcja").nth(0).innerText)
    .contains("Programowanie (cwiczenia)")
    .expect(Selector("#lekcja").nth(0).innerText)
    .contains("inż. A Kowalski")
    .expect(Selector("#lekcja").nth(0).innerText)
    .contains("Sala 208")
    // sprawdzenie planu innej grupy
    .click(Selector("#trybStudiow"))
    .click(Selector("li").withText("Stacjonarne"))
    .click(Selector("#grupa"))
    .click(Selector("li").withText("D501"))
    .click(Selector("#trybPlanu"))
    .click(Selector("li").withText("Standardowy"))
    .expect(Selector("#lekcja").count)
    .eql(0)
    .click(Selector("#trybStudiow"))
    .click(Selector("li").withText("Niestacjonarne"))
    .click(Selector("#grupa"))
    .click(Selector("li").withText("Z715"))
    .expect(Selector("#lekcja").count)
    .eql(3);
});
