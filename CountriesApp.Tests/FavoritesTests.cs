using CountriesApp.Models;
using CountriesApp.Pages;
using CountriesApp.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Xunit;
using Bunit;
using Moq;

namespace CountriesApp.Tests
{
    public class FavoritesTests : TestContext
    {
        private readonly Mock<IJSRuntime> _mockJsRuntime;

        public FavoritesTests()
        {
            _mockJsRuntime = new Mock<IJSRuntime>();

            // To simulate a Favorites list
            var mockFavorites = new List<Country>
            {
                new Country {Name = "Belgium", Flag = "url_to_flag"},
                new Country {Name = "France", Flag = "url_to_flag"}
            };

            _mockJsRuntime
                .Setup(js => js.InvokeAsync<List<Country>>("localStorageHelper.loadFavorites", It.IsAny<object[]>()))
                .ReturnsAsync(mockFavorites);

            Services.AddSingleton(_mockJsRuntime.Object);
        }
        

        [Fact]
        public void FavoritesPage_ShouldDisplayFavorites()
        {
            var component = RenderComponent<Favorites>();
            var countryCards = component.WaitForElements(".country-card");
            Assert.Equal(2, countryCards.Count);
        }


        [Fact]
        public void RemoveFromFavorites_ShouldUpdateList()
        {
            var component = RenderComponent<Favorites>();
            var countryCardsBefore = component.WaitForElements(".country-card");
            Assert.Equal(2, countryCardsBefore.Count);

            var removeButton = component.Find("button");
            removeButton.Click();

            // Updated list?
            var countryCardsAfter = component.WaitForElements(".country-card");
            Assert.Single(countryCardsAfter);
        }


        [Fact]
        public async Task SearchFavorites_FiltersFavoritesCorrectly()
        {
            // Favorites well display?
            var component = RenderComponent<Favorites>();
            var countryCards = component.FindAll(".country-card");
            Assert.Equal(2, countryCards.Count);

            // find th search bar, simulate a search and press enter
            var searchInput = component.Find("input.search-input");
            await searchInput.ChangeAsync(new ChangeEventArgs {Value = "Belgium"});
            await searchInput.KeyUpAsync(new KeyboardEventArgs {Key = "Enter"});
            

            // Belgium well display?
            countryCards = component.FindAll(".country-card");
            Assert.Single(countryCards);
            Assert.Contains("Belgium", countryCards[0].TextContent);
        }
    }
}