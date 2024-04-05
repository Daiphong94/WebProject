using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface SCInterface
    {
        Task<StudentCompetition> GetById(int id);
        Task<IEnumerable<StudentCompetition>> GetAll();
        Task Add(StudentCompetition entity);
        Task Update(StudentCompetition entity);
        Task Delete(int id);
    }
}
