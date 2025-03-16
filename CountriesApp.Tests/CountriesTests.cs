using CountriesApp.Models;
using CountriesApp.Pages;
using CountriesApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Xunit;
using Bunit;
using Moq;

namespace CountriesApp.Tests
{
    public class CountriesTests : TestContext
    {
        private readonly Mock<ICountryService> _mockService;
        private readonly Mock<IJSRuntime> _mockJsRuntime;

        public CountriesTests()
        {
            _mockService = new Mock<ICountryService>();
            _mockJsRuntime = new Mock<IJSRuntime>();

            // To simulate API data
            _mockService.Setup(service => service.GetAllCountriesAsync())
                .ReturnsAsync(new List<Country> {
                    new Country {Name = "Belgium", Capital = "Brussels"},
                    new Country {Name = "Germany", Capital = "Berlin"},
                    new Country {Name = "France", Capital = "Paris"}
                });

            // Add mocks to Blazor Service
            Services.AddSingleton(_mockService.Object);
            Services.AddSingleton(_mockJsRuntime.Object);
        }


        [Fact]
        public void CountriesPage_ShouldDisplayAllCountries()
        {
            // all 3 countries display?
            var component = RenderComponent<Countries>();
            var countryCards = component.FindAll(".country-card");
            Assert.Equal(3, countryCards.Count);
        }


        [Fact]
        public async Task SearchCountries_FiltersCountriesCorrectly()
        {
            var component = RenderComponent<Countries>();
            var countryCards = component.FindAll(".country-card");
            Assert.Equal(3, countryCards.Count);

            // To find the search bar and simulate an search
            var searchInput = component.Find("input.search-input");
            await searchInput.ChangeAsync(new ChangeEventArgs {Value = "Belgium"});
            await searchInput.KeyUpAsync(new KeyboardEventArgs {Key = "Enter"});

            // Belgium display?
            countryCards = component.FindAll(".country-card");
            Assert.Single(countryCards);
            Assert.Contains("Belgium", countryCards[0].TextContent);
        }


        [Fact]
        public void AddToFavorites_ShouldCallLocalStorage()
        {
            var component = RenderComponent<Countries>();

            // Find the button
            var addButton = component.Find("button");
            addButton.Click();

            // local storage call ok?
            _mockJsRuntime.Setup(js => js.InvokeAsync<List<Country>>("localStorageHelper.loadFavorites", It.IsAny<object[]>()))
                .ReturnsAsync(new List<Country>());
        }
    }
}