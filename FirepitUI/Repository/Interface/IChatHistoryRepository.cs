using FirepitUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitUI.Repository.Interface
{
    public interface IChatHistoryRepository : IBaseRepository<ChatHistory>
    {
        Task<IList<ChatHistory>> GetChats(string url, string toUserId);
    }
}
