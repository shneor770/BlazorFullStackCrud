using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorFullStackCrud.Client.Services.SuperHeroService
{
    public class SuperHeroService : ISuperHeroService
    {
        private readonly HttpClient http;
        private readonly NavigationManager navigationManager;

        public SuperHeroService(HttpClient http, NavigationManager navigationManager)
        {
            this.http = http;
            this.navigationManager = navigationManager;
        }
        public List<SuperHero> Heroes { get; set; } = new List<SuperHero>();
        public List<Comic> Comics { get; set; } = new List<Comic>();

        public async Task GetComics()
        {
            var result = await this.http.GetFromJsonAsync<List<Comic>>("api/superhero/comics");
            if (result != null)
                this.Comics = result;
        }

        public async Task<SuperHero> GetSingleHero(int id)
        {
            var result = await this.http.GetFromJsonAsync<SuperHero>($"api/superhero/{id}");
            if (result != null)
                return result;
            throw new Exception("Hero Not found");
        }

        public async Task GetSuperHeroes()
        {
            var result = await this.http.GetFromJsonAsync<List<SuperHero>>("api/superhero");
            if(result != null)
                this.Heroes = result;
        }



        public async Task CreateHero(SuperHero hero)
        {
            var result = await http.PostAsJsonAsync("api/superhero", hero);
            await SetHeros(result);
        }
        public async Task UpdateHero(SuperHero hero)
        {
            var result = await this.http.PutAsJsonAsync($"api/superhero/{hero.Id}", hero);
            await SetHeros(result);
        }
        public async Task DeleteHero(int id)
        {
            var result = await http.DeleteAsync($"api/superhero/{id}");
            await SetHeros(result);
        }

        private async Task SetHeros(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<SuperHero>>();
            Heroes = response;
            this.navigationManager.NavigateTo("superheroes");
        }
    }
}
