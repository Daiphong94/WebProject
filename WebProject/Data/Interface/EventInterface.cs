using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface EventInterface
    {
        Task<Events> GetById(int id);
        Task<IEnumerable<Events>> GetAll();
        Task Add(Events entity);
        Task Update(Events entity);
        Task Delete(int id);
    }
}
