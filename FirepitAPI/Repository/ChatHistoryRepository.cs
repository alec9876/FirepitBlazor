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
    public class ChatHistoryRepository : IChatHistoryRepository
    {
        private readonly ApplicationDbContext _db;

        public ChatHistoryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IList<ChatHistory>> GetChats(string fromUserId, string toUserId)
        {
            var getChats = await FindAll();
            var chat = getChats.Where(x => x.FromUserId == fromUserId).Where(x => x.ToUserId == toUserId).ToList();
            return chat;
        }

        public async Task<bool> Create(ChatHistory entity)
        {
            await _db.ChatHistory.AddAsync(entity);
            return await Save();
        }

        public async Task<IList<ChatHistory>> FindAll()
        {
            var chats = await _db.ChatHistory.Include(x => x.FromUser).Include(x => x.ToUser).ToListAsync();
            return chats;
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }


        //Not Used - Don't Need To Use
        public Task<bool> Delete(ChatHistory entity)
        {
            throw new NotImplementedException();
        }

        public Task<ChatHistory> FindById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> isExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ChatHistory entity)
        {
            throw new NotImplementedException();
        }
    }
}
