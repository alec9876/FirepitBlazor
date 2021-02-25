using FirepitAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.Repository.Interface
{
    public interface IMeetingRepository : IBaseRepository<Meeting>
    {
        Task<bool> CreateUserMeeting(UserMeeting entity);
        Task<bool> UpdateUserMeeting(UserMeeting entity);
    }
}
