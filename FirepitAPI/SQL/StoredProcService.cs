using FirepitAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.SQL
{
    public interface StoredProcService
    {
        Task<bool> DeleteMeetingProc();
    }
}
