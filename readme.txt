--- SYSTEM INFORMATYCZNY DO OBSUGI PLANU ZAJĘĆ WWSI ---
--- INSTRUKCJA URUCHAMIANIA ---

Zainstalować serwer baz danych PostgreSQL 12 oraz utworzyć bazę o nazwie "planwwsi".
Link: https://www.postgresql.org/download/windows/

Katalog "Plan.backend" zawiera aplikację serwerową.
Plik "Plan.backend\Plan\Plan.sln" należy otworzyć za pomocą środowiska Visual Studio.
Na opcji "Solution 'Plan'" wybrać opcję "Restore NuGet Packages", następnie "Rebuild", a następnie uruchomić program (F5).
Aby można było zalogować się do aplikacji webowej należy utworzyć użytkownika wykonując komendę "/api/uzytkownik/dodaj" z parametrami Imie, Nazwisko, Email oraz Haslo.

Katalog "Plan.web" zawiera aplikację webową.
Należy zainstalować Node.js oraz Yarn a następnie w katalogu "Plan.web" wykonać komendy "yarn install" oraz "yarn start".
Aplikacja dostępna będzie pod adresem "http://localhost:3000"

Katalog "Plan.mobile" zawiera aplikację mobilną.
Plik "Plan.mobile\PlanWWSI.sln" należy otworzyć za pomocą środowiska Visual Studio.
Wybrać projekt "PlanWWSI.Android" jako projekt startowy.
Wybrać opcję "Restore NuGet Packages", następnie opcję "Rebuild".
Wybrać opcję uruchamiania programu, utworzyć nowy Android emulator.
Aplikację można również uruchomić w trybie debugowania na smartfonie, należy w tym celu podłączyć smartfon poprzez USB oraz włączyć w smartfonie tryb debugowania poprzez USB.