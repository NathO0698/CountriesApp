using CountriesApp.Models;
using CountriesApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CountriesApp.Tests {
    public class CountryServiceTests {
        private readonly CountryService _countryService;
        public CountryServiceTests() {
            _countryService = new CountryService();
        }

        [Fact]
        public async Task GetAllCountriesAsync_ReturnsCountries_WhenApiCallIsSuccessful()
        {
            
            var mockCountries = new List<CountryAPI> {                                          //fictive data
                new CountryAPI {
                    Name = new NameData {Common = "Belgium", Official = "Kingdom of Belgium"},
                    Capital = new string[] {"Brussels"},
                    Region = "Europe",
                    Population = 11555997,
                    Flags = new FlagsData {Png = "url_to_flag"}
                }
            };

            _countryService.SetMockData(mockCountries);
            var countries = await _countryService.GetAllCountriesAsync();

            Assert.NotNull(countries);
            Assert.Single(countries);  
            Assert.Equal("Belgium", countries[0].Name);
        }

        [Fact]
        public async Task GetAllCountriesAsync_ReturnsEmptyList_WhenApiCallFails() {
            
            _countryService.SetMockData(null);                                  //Echec simulation
            var countries = await _countryService.GetAllCountriesAsync();
            Assert.Empty(countries);                                            //Empty list if echec?
        }
    }
}