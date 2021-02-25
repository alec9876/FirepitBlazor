using Blazored.LocalStorage;
using FirepitUI.Models;
using FirepitUI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace FirepitUI.Repository
{
    public class ChatHistoryRepository : BaseRepository<ChatHistory>, IChatHistoryRepository
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public ChatHistoryRepository(HttpClient client,
            ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<IList<ChatHistory>> GetChats(string url, string toUserId)
        {
            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetBearerToken());
            var reponse = await _client.GetFromJsonAsync<IList<ChatHistory>>(url + toUserId);

            return reponse;
        }

        private async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}
