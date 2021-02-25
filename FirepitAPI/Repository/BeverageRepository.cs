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
    public class BeverageRepository : IBeveragesRepository
    {
        private readonly ApplicationDbContext _db;

        public BeverageRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Beverages entity)
        {
            await _db.Beverages.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Beverages entity)
        {
            _db.Beverages.Remove(entity);
            return await Save();
        }

        public async Task<IList<Beverages>> FindAll()
        {
            var beverages = await _db.Beverages.Include(p => p.Person).Include(m => m.Meeting).ToListAsync();
            return beverages;
        }

        public async Task<Beverages> FindById(int id)
        {
            var beverages = await _db.Beverages.Include(p => p.Person).Include(m => m.Meeting).FirstOrDefaultAsync(x => x.Id == id);
            return beverages;
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Beverages.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Beverages entity)
        {
            _db.Beverages.Update(entity);
            return await Save();
        }
        public async Task<IList<Beverages>> FindTypes()
        {
            var getBeverages = await FindAll();
            var getBeverageTypes = getBeverages.GroupBy(x => x.Type).Select(x => x.FirstOrDefault()).ToList();
            return getBeverageTypes;
        }

        public async Task<IList<Beverages>> FindByType(string type)
        {
            var getBeverages = await FindAll();
            var getBeverageTypes = getBeverages.Where(x => x.Type == type).ToList();
            return getBeverageTypes;
        }
    }
}
