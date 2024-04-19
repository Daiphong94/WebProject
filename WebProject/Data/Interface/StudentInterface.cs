using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface StudentInterface
    {
        Task<Student> GetByUserId(int userId);
        Task<Student> GetByIdStudent(int id);
        Task<IEnumerable<Student>> GetAllStudent();
        Task AddStudent(Student entity);
        Task UpdateStudent(Student entity);
        Task DeleteStudent(int id);
        Task<Student> GetByEmail(string email);

        Task<IEnumerable<Competition>> GetRegisteredCompetitions(int studentId);
    }
}
