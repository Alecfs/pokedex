# Pokedex

## Kort om projektet

Dette projekt er en simpel Pokedex konsol applikation udviklet i C#. Den tillader brugere at se, søge, tilføje, redigere og slette Pokemon.

## Features

- **Brugerlogin**: En bruger kan logge ind og ud og få adgang til flere funktioner når de er logget ind.
- **Se Pokemon**: Vis alle Pokemon.
- **Søg efter Pokemon**: Søg efter Pokemon på navn eller type.
- **Tilføk Pokemon**: Tilføj nye Pokemon.
- **Rediger Pokemon**: Rediger eksisterende Pokemon.
- **Slet Pokemon**: Slet Pokemon.

## Fil Struktur

- **Classes/**
  - `User.cs`: Definere `User` klassen.
  - `Pokemon.cs`: Definere `Pokemon` klassen.
  - `LoginManager.cs`: Kontrollerer `User` log ind og log ud.
  - `FileManager.cs`: Håndtere fil operationer for users og Pokemon data.
  - `PokedexManager.cs`: Håndtere Pokedex operationer såsom læsning, søgning, tilføjning, redigering og sletning af Pokemon.
- **data/**
  - `pokedata.csv`: Indeholder Pokemon data.
  - `users.csv`: Indeholder user data.

## Sådan bruger du programmet

- **Log ind**: Brug det standard login `admin/admin` for at logge ind og få adgang til at tilføje, redigere og slette Pokemon.
- **Se alle Pokemon**: For at få vist alle Pokemon.
- **Søg efter Pokemon**: For at søge efter specifikke Pokemon efter `navn` eller `type`.
- **Tilføj Pokemon**: For at tilføje en ny Pokemon.
- **Rediger Pokemon**: For at redigere en gemt Pokemon baseret på ID.
- **Slet Pokemon**: For at slette en Pokemon baseret på ID.
- **Log ud**: Hvis du er logget ind har du muligheden for at logge ud igen.
