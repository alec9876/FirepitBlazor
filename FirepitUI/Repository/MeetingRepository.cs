using FirepitUI.Repository.Interface;
using FirepitUI.Models;
using System.Net.Http;
using Blazored.LocalStorage;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System;
using System.Net.Http.Json;

namespace FirepitUI.Repository
{
    public class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public MeetingRepository(HttpClient client,
            ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task<bool> UpdateUserMeeting(string url, UserMeeting obj, int id)
        {
            if (obj == null)
                return false;

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetBearerToken());
            var response = await _client.PutAsJsonAsync(url + id, obj);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return true;

            return false;
        }

        private async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}
