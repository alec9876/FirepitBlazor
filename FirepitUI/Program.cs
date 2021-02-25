using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using FirepitUI.Repository.Interface;
using FirepitUI.Providers;
using FirepitUI.Repository;
using Blazored.Toast;
using Blazored.Modal;

namespace FirepitUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient{ BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddBlazoredToast();
            builder.Services.AddBlazoredModal();

            // Token
            builder.Services.AddScoped<ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(p =>
                p.GetRequiredService<ApiAuthenticationStateProvider>());
            builder.Services.AddScoped<JwtSecurityTokenHandler>();
            builder.Services.AddTransient<IAuthenticationRepository, AuthenticationRepository>();

            // Repositories
            builder.Services.AddTransient<IPersonRepository, PersonRepository>();
            builder.Services.AddTransient<IBeveragesRepository, BeverageRepository>();
            builder.Services.AddTransient<IMeetingRepository, MeetingRepository>();
            builder.Services.AddTransient<IQuoteRepository, QuoteRepository>();
            builder.Services.AddTransient<IChatHistoryRepository, ChatHistoryRepository>();

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();


            await builder.Build().RunAsync();
        }
    }
}
