using Blazored.LocalStorage;
using FirepitUI.Models;
using FirepitUI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FirepitUI.Repository
{
    public class QuoteRepository : BaseRepository<Quotes>, IQuoteRepository
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public QuoteRepository(HttpClient client,
            ILocalStorageService localStorage) : base(client, localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }
    }
}
