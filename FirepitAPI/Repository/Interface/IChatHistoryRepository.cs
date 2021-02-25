using FirepitAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.Repository.Interface
{
    public interface IChatHistoryRepository : IBaseRepository<ChatHistory>
    {
        Task<IList<ChatHistory>> GetChats(string fromUserId, string toUserId);
    }
}
