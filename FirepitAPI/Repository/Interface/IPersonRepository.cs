using FirepitAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FirepitAPI.Repository.Interface
{
    public interface IPersonRepository 
    {
        Task<IList<Person>> FindAll();
        Task<Person> FindById(string id);
        Task<bool> isExists(string id);
        Task<Person> FindByEmail(string email);
        Task<bool> Create(Person entity);
        Task<bool> Update(Person entity);
        Task<bool> Delete(Person entity);
        Task<bool> Save();
    }
}
