# 1. Instalace a spuštění

## 1.1) Prerekvizity

- Nainstalovaná poslední verze dockeru.

Následujte pokyny jednotlivých dokumentací pro stažení korektních verzí pro Váš systém.

## 1.2) Kroky ke spuštění
- Stáhnout zdrojový kód aplikace z platformy GitHub (k dispozici nejnovější stav aplikace, případně v příloze je stažený zip) (https://github.com/vformi/flight-log-dotnet)
  - Zelené tlačítko vpravo “Code”
  - Kliknout na možnost “Download ZIP”
- Otevřít zip a extrahovat soubory na počítač. (např. pomocí aplikace [7-zip](https://www.7-zip.org/download.html))
- Otevřít terminál v adresáři, kde se nachází zmíněný zip a spustit následující příkazy
  - `cd ./FlightLogNet`
  - `docker compose up -d`
- Aplikace by se měla po chvíli spustit a být dostupná z http://localhost:41313

# Architektura a moduly
#### wwwroot
- statický obsah pro frontend
#### Controllers
- vystavuje REST API pro frontend
#### Facades
- definuje rozhraní pro funkcionalitu
#### Operations
- obsahuje byznys logiku
#### Models
- obsahuje doménové modely
#### Integration
- obsahuje integrace na externí služby
#### Repositories
- obsahuje integraci na databázi
#### Entities 
- obsahuje databázové entity

