using Blazored.LocalStorage;
using System.Text.Json;

namespace PITPortalTest.Services
{
    public interface ICustomPages
    {
        Task SavePages(List<Page> pages);
        Task<List<Page>> GetPages();
    }

    public class CustomPages : ICustomPages
    {
        private readonly ILocalStorageService localStorage;

        public CustomPages(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }
        public async Task SavePages(List<Page> pages)
        {
            var pageString = JsonSerializer.Serialize(pages);
            await localStorage.SetItemAsStringAsync("Pages", pageString);
        }
        public async Task<List<Page>> GetPages()
        {
            var localPages = await localStorage.GetItemAsync<List<Page>>("Pages");
            if (localPages is null)
            {
                var pages = new List<Page>();
                var counterPage = new Page()
                {
                    Id = 1,
                    PageName = "CounterPage",
                    RouteValue = "Counter",
                    AuthorizedRoles = new()
                };
                counterPage.AuthorizedRoles.Add("CounterView");

                var weatherPage = new Page()
                {
                    Id = 2,
                    PageName = "WeatherPage",
                    RouteValue = "FetchData",
                    AuthorizedRoles = new()
                };

                weatherPage.AuthorizedRoles.Add("WeatherView");
                pages.Add(counterPage);
                pages.Add(weatherPage);
                await SavePages(pages);
                return pages;
            }
            else
            {
                return localPages;
            }
        }
    }
}
