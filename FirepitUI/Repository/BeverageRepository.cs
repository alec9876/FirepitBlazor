using Blazored.LocalStorage;
using FirepitUI.Models;
using FirepitUI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FirepitUI.Repository
{
    public class BeverageRepository : BaseRepository<Beverages>, IBeveragesRepository
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public BeverageRepository(HttpClient client,
            ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<IList<Beverages>> FindTypes(string url)
        {
            try
            {
                _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetBearerToken());
                var reponse = await _client.GetFromJsonAsync<IList<Beverages>>(url);

                return reponse;

            }
            catch (Exception)
            {
                return null;

            }
        }

        public async Task<IList<Beverages>> FindByType(string url, string type)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetBearerToken());
            var reponse = await _client.GetFromJsonAsync<IList<Beverages>>(url + type);

            return reponse;
        }

        private async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}
