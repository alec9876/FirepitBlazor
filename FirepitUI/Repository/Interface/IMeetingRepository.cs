using FirepitUI.Models;
using FirepitUI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitUI.Repository.Interface
{
    public interface IMeetingRepository : IBaseRepository<Meeting>
    {
        Task<bool> UpdateUserMeeting(string url, UserMeeting obj, int meetingId);
    }
}
