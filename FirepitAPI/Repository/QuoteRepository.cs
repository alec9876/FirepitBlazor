using FirepitAPI.Data;
using FirepitAPI.Models;
using FirepitAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.Repository
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly ApplicationDbContext _db;

        public QuoteRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Quotes entity)
        {
            await _db.Quotes.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Quotes entity)
        {
            _db.Quotes.Remove(entity);
            return await Save();
        }

        public async Task<IList<Quotes>> FindAll()
        {
            var quotes = await _db.Quotes.Include(x => x.PersonIdKey).Include(x => x.WhoSaidItKey).ToListAsync();
            return quotes;
        }

        public async Task<Quotes> FindById(int id)
        {
            var quotes = await _db.Quotes.Include(x => x.PersonIdKey).Include(x => x.WhoSaidItKey).FirstOrDefaultAsync(x => x.Id == id);
            return quotes;
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Quotes.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Quotes entity)
        {
            _db.Quotes.Update(entity);
            return await Save();
        }
    }
}
