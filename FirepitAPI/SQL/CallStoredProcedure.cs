using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FirepitAPI.Data;
using FirepitAPI.Models;
using FirepitAPI.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FirepitAPI.SQL
{
    public class CallStoredProcedure : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public CallStoredProcedure(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void DeleteMeetingProc()
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                var sql = db.Database.ExecuteSqlRaw("EXECUTE DeleteFirepitDateBeforeToday");
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            DeleteMeetingProc();

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
