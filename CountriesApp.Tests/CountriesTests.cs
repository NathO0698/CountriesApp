using CountriesApp.Models;
using CountriesApp.Pages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CountriesApp.Tests {
    public class CountriesTests{
        private readonly Countries _countries;

        public CountriesTests() {
            _countries = new Countries();
            _countries.SetCountries(new List<Country> {
                new Country { Name = "Belgium", Capital = "Brussels" },
                new Country { Name = "Germany", Capital = "Berlin" },
                new Country { Name = "France", Capital = "Paris" }
            });
        }

        [Fact]
        public void SearchCountries_FiltersCountriesCorrectly() {
            _countries.SearchQuery = "Belgium";                                     //Search text
            _countries.SearchCountries();
            Assert.Single(_countries.FilteredCountries);                            //OK 1 country?
            Assert.Equal("Belgium", _countries.FilteredCountries.First().Name);
        }

        [Fact]
        public void SearchCountries_ReturnsAllCountries_WhenQueryIsEmpty() {
            _countries.SearchQuery = string.Empty;                                  //Search bar empty
            _countries.SearchCountries();
            Assert.Equal(3, _countries.FilteredCountries.Count);                    //All Countries display?
        }
    }
}