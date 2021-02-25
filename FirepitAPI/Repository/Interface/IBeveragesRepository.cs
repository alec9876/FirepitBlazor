using FirepitAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.Repository.Interface
{
    public interface IBeveragesRepository : IBaseRepository<Beverages>
    {
        Task<IList<Beverages>> FindTypes();
        Task<IList<Beverages>> FindByType(string type);
    }
}
