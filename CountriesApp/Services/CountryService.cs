using System.Net.Http.Json;
using CountriesApp.Models;

namespace CountriesApp.Services {
    public interface ICountryService {
        Task<List<Country>> GetAllCountriesAsync();                        
    }

    public class CountryService : ICountryService {
        private readonly HttpClient _httpClient;
        public CountryService(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<List<Country>> GetAllCountriesAsync() {                   
            var DataFromAPI = await _httpClient.GetFromJsonAsync<List<CountryAPI>>("https://restcountries.com/v3.1/all");              //get to the API to receive countries information
            if (DataFromAPI == null) {
                Console.WriteLine("Aucune donnée reçue de l'API !");                //Check in the console
                return new List<Country>();                                         //return an empty list if no data received
            }

            return DataFromAPI.Select(c => new Country {                               //Transform datas
                Name = c.Name.Common,
                OfficialName = c.Name.Official,
                Capital = c.Capital.Length > 0 ? string.Join(", ", c.Capital) : "N/A",
                Region = c.Region,
                Population = c.Population,
                Flag = c.Flags.Png,
            }).ToList() ?? new List<Country>();
        }
    }

    public class CountryAPI {
        public NameData Name {get; set;} = new();
        public string[] Capital {get; set;} = Array.Empty<string>();
        public string Region {get; set;} = string.Empty;
        public long Population {get; set;}
        public FlagsData Flags {get; set;} = new();
    }

    public class NameData {
        public string Common {get; set;} = string.Empty;
        public string Official {get; set;} = string.Empty;
    }

    public class FlagsData {
        public string Png {get; set;} = string.Empty;
    }
}