# CountriesApp

Application Blazor permettant de visualiser la liste des pays du monde, avec des fonctionnalités de recherche, d'affichage des détails des pays, ainsi que la possibilité d'ajouter des pays en favoris dans le `localStorage` du navigateur.
L'API utilisée est "REST countries".

## Fonctionnalités

- **Affichage des pays** : L'application récupère la liste des pays depuis l'API REST [RestCountries](https://restcountries.com/) et dans une liste.
- **Recherche de pays** : L'utilisateur peut rechercher des pays par nom à l'aide d'une barre de recherche dynamique.
- **Favoris** : Les utilisateurs peuvent ajouter des pays à leur liste de favoris, stockée dans le `localStorage` du navigateur.
- **Détails du pays** : En cliquant sur un pays, l'utilisateur peut accéder à une page détaillant plus d'informations sur ce pays.

## Technologies utilisées

- **Blazor WebAssembly**
- **.NET 6** 
- **C#**
- **JavaScript (JSRuntime)**
- **RestCountries API**

## Prérequis

Avant de démarrer le projet, assurez-vous d'avoir installé :

- [Visual Studio](https://visualstudio.microsoft.com/) ou un autre éditeur compatible avec .NET.
- [.NET 6 SDK](https://dotnet.microsoft.com/download).
- [Node.js](https://nodejs.org/) (si vous souhaitez tester les outils frontend).

## Installation

1. Clonez le dépôt GitHub dans votre répertoire local :

      ```bash
     git clone https://github.com/your-username/CountriesApp.git
   

2. Ouvrez le projet dans Visual Studio ou votre éditeur préféré.

3. Restaurez les dépendances du projet :

    ```bash
    dotnet restore


4. Lancez le projet avec la commande suivante :

    ```bash
    dotnet run


Vous pouvez également exécuter le projet depuis Visual Studio en appuyant sur Ctrl + F5 ou en utilisant le bouton de démarrage.

5. L'application devrait être accessible sur https://localhost:5001.

## Structure

CountriesApp
    |   App.razor
    |   CountriesApp.csproj
    |   Program.cs
    |   _Imports.razor
    |
    +---bin\Debug\net8.0
    |
     +---Layout
    |       MainLayout.razor
    |       MainLayout.razor.css
    |       NavMenu.razor
    |       NavMenu.razor.css
    |
    +---Models
    |       Country.cs
    |
    +---obj\Debug\net8.0...
    |
    +---Pages
    |       Countries.razor
    |       Countries.razor.css
    |       CountryDetails.razor
    |       CountryDetails.razor.css
    |       Favorites.razor
    |       Favorites.razor.css
    |       Home.razor
    |       Home.razor.css
    |
    +---Properties
    |       launchSettings.json
    |
    +---Services
    |       CountryService.cs
    |
    \---wwwroot
        |   favicon.png
        |   icon-192.png
        |   index.html
        |
        +---css
        |   |   app.css
        |   |
        |   \---bootstrap
        |           bootstrap.min.css
        |           bootstrap.min.css.map
        |
        +---Images
        |       Country-flags.png
        |
        +---js
        |       localStorage.js
        |
        \---sample-data
                weather.json

## Auteurs
Auteur principal du projet : Nathan
Autres contributeurs : /

