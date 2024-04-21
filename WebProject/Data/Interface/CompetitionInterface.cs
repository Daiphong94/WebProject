using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface CompetitionInterface
    {
        Task<Competition> GetById(int id);
        Task<IEnumerable<Competition>> GetAll();
        Task Add(Competition entity);
        Task Update(Competition entity);
        Task Delete(int id);
        Task<Competition> GetByName(string competitionName);
        Task<Competition> GetFirst();
    }
}
