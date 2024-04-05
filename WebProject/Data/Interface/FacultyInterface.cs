using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface FacultyInterface
    {
        Task<Faculty> GetByIdFaculty(int id);
        Task<IEnumerable<Faculty>> GetAllFaculty();
        Task AddFaculty(Faculty entity);
        Task UpdateFaculty(Faculty entity);
        Task DeleteFaculty(int id);
    }
}
