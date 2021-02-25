using FirepitUI.Models;
using FirepitUI.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitUI.Repository.Interface
{
    public interface IBeveragesRepository : IBaseRepository<Beverages>
    {
        Task<IList<Beverages>> FindTypes(string url);
        Task<IList<Beverages>> FindByType(string url, string type);
    }
}
