using FirepitAPI.Data;
using FirepitAPI.Models;
using FirepitAPI.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly UserManager<Person> _usermanager;
        private readonly ApplicationDbContext _db;

        public PersonRepository(UserManager<Person> userManager, ApplicationDbContext db)
        {
            _db = db;
            _usermanager = userManager;
        }
        public async Task<bool> Create(Person entity)
        {
            await _db.Users.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Person entity)
        {
             _db.Users.Remove(entity);
            return await Save();
        }

        public async Task<IList<Person>> FindAll()
        {
            var people = _db.Users.Include(x => x.Beverages).ToListAsync();
            return await people;
        }

        public async Task<Person> FindById(string id)
        {
            var people = await _db.Users.Include(x => x.Beverages).FirstOrDefaultAsync(x => x.Id == id);
            return people;
        }

        public async Task<bool> isExists(string id)
        {
            return await _db.Users.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 1;
        }

        public async Task<bool> Update(Person entity)
        {
            await _usermanager.UpdateAsync(entity);
            return await Save();
        }

        public async Task<Person> FindByEmail(string email)
        {
            var person = await _db.Users.FirstOrDefaultAsync(x => x.Email == email);
            return person;
        }
    }
}
