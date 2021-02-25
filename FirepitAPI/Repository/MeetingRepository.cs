using FirepitAPI.Data;
using FirepitAPI.DTO;
using FirepitAPI.Models;
using FirepitAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace FirepitAPI.Repository
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly ApplicationDbContext _db;

        public MeetingRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(Meeting entity)
        {
            await _db.Meetings.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> CreateUserMeeting(UserMeeting entity)
        {
            await _db.UserMeetings.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Meeting entity)
        {
            _db.Meetings.Remove(entity);
            return await Save();
        }

        public async Task<IList<Meeting>> FindAll()
        {
           return await _db.Meetings.Include(x => x.UserMeetings).Include(x => x.Beverages).ToListAsync();
        }

        public async Task<Meeting> FindById(int id)
        {
            return await _db.Meetings.Include(x => x.UserMeetings).Include(x => x.Beverages).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> isExists(int id)
        {
            return await _db.Meetings.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Meeting entity)
        {
            _db.Meetings.Update(entity);
            return await Save();
        }

        public async Task<bool> UpdateUserMeeting(UserMeeting entity)
        {
            _db.UserMeetings.Update(entity);
            return await Save();
        }
    }
}
