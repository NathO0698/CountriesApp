using CountriesApp.Models;
using CountriesApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Bunit;
using Moq;
using Moq.Protected;

namespace CountriesApp.Tests
{
    public class CountryServiceTests
    {
        private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private readonly HttpClient _httpClient;
        private readonly CountryService _countryService;

        public CountryServiceTests()
        {
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            _httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new System.Uri("https://restcountries.com/v3.1/")
            };

            _countryService = new CountryService(_httpClient);
        }

        [Fact]
        public async Task GetAllCountriesAsync_ReturnsCountries_WhenApiCallIsSuccessful()
        {
            // To simululate datas from API
            var mockApiResponse = new List<CountryAPI>
            {
                new CountryAPI
                {
                    Name = new NameData {Common = "Belgium", Official = "Kingdom of Belgium"},
                    Capital = new string[] {"Brussels"},
                    Region = "Europe",
                    Population = 11500000,
                    Flags = new FlagsData {Png = "https://example.com/belgium-flag.png"}
                },
                new CountryAPI
                {
                    Name = new NameData {Common = "France", Official = "French Republic"},
                    Capital = new string[] {"Paris"},
                    Region = "Europe",
                    Population = 67000000,
                    Flags = new FlagsData {Png = "https://example.com/france-flag.png"}
                }
            };

            // Convert to JSON to simulate response from API
            var jsonResponse = JsonSerializer.Serialize(mockApiResponse);
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, "application/json")
            };

            // Set up a Mock to simulate the API response
            _mockHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            // Execut the method
            var result = await _countryService.GetAllCountriesAsync();

            // Verification
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "Belgium");
            Assert.Contains(result, c => c.Name == "France");
        }
    }
}