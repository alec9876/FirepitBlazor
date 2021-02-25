using FirepitUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitUI.Repository.Interface
{
    public interface IPersonRepository 
    {
        Task<Person> Get(string url, string id);
        Task<IList<Person>> Get(string url);
        Task<bool> Create(string url, Person obj);
        Task<bool> Update(string url, Person obj, string id);
        Task<bool> Delete(string url, string id);
    }
}
