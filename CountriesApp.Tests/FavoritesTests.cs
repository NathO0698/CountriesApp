using CountriesApp.Models;
using CountriesApp.Pages;
using System.Collections.Generic;
using Xunit;

namespace CountriesApp.Tests {
    public class AddToFavoritesTests {
        private readonly Countries _countries;

        public AddToFavoritesTests() {
            _countries = new Countries();
            _countries.SetCountries(new List<Country> {
                new Country { Name = "Belgium", Capital = "Brussels" }
            });
        }

        [Fact]
        public async Task AddToFavorites_AddsCountryToFavorites()
        {
            var country = new Country { Name = "Belgium", Capital = "Brussels" };               //Country to add
            _countries.Favorites = new List<Country>();
            await _countries.AddToFavorites(country);                                       //Add to fav
            Assert.Contains(country, _countriesPage.Favorites);                                 //Add OK?
        }

        [Fact]
        public async Task AddToFavorites_DoesNotAddDuplicateCountry()
        {
            var country = new Country { Name = "Belgium", Capital = "Brussels" };               //Already in the fav
            _countriesPage.Favorites = new List<Country> { country };
            await _countries.AddToFavorites(country);                                           //Try to add the same
            Assert.Single(_countries.Favorites);                                                //Not add 2 times?
        }
    }
}