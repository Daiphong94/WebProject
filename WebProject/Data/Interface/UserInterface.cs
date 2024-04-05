using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface UserInterface
    {
        
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetAll();
        Task Add(User entity);
        Task Update(User entity);
        Task Delete(int id);
        Task<User> GetByName(string username); 
        Task<bool> Exists(int id);
    }
}
