--- KAMIL ŁOŃ NR ALBUMU: 8150                       ---
--- SYSTEM INFORMATYCZNY DO OBSUGI PLANU ZAJĘĆ WWSI ---
--- INSTRUKCJA URUCHAMIANIA SYSTEMU                 ---

Zainstalować serwer baz danych PostgreSQL 12, link: https://www.postgresql.org/download/windows/
Utworzyć pustą bazę danych o nazwie "planwwsi"

Katalog "Plan.backend" zawiera aplikację serwerową.
Plik projektu (Plan.sln) znajduje się w katalogu "Plan.backend\Plan", należy go otworzyć za pomocą środowiska Visual Studio.
Na głównym projekcie wybrać opcję "Restore NuGet Packages"
Na głównym projekcie wybrać opcję "Rebuild"
Uruchomić program klikając CTRL+F5. Serwer powinien być dostępny pod adresem: http://127.0.0.1:60211
Dodać pierwszego użytkownika, wykonując żądanie: http://127.0.0.1:60211/api/uzytkownik/dodaj?Imie=Alojzy&Nazwisko=Testowy&Email=test@planwwsi.pl&Haslo=Haslo123!

Katalog "Plan.web" zawiera aplikację webową.
Do uruchomienia aplikacji potrzebne jest oprogramowanie Node.js, link: https://nodejs.org/en/download/
W katalogu "Plan.web" uruchomić terminal i  wykonać komendę "npm install"
W katalogu "Plan.web" uruchomić terminal i wykonać komendę "npm start"
Aplikacja dostępna będzie pod adresem: http://localhost:3000
Dane do logowania: email: test@planwwsi.pl hasło: Haslo123!

Katalog "Plan.mobile" zawiera aplikację mobilną.
Plik projektu (PlanWWSI.sln) znajduje się w katalogu "Plan.mobile", należy go otworzyć za pomocą środowiska Visual Studio.
Na projekcie "PlanWWSI.Android" wybrać opcję "Set as Startup project"
Na głównym projekcie wybrać opcję "Restore NuGet Packages"
Na głównym projekcie wybrać opcję "Rebuild"
Uruchomić program klikając CTRL+F5.
Aplikacja będzie dostępna w emulatorze urządzenia z systemem Android.