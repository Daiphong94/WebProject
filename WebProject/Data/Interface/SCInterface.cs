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
        Task<StudentCompetition> GetById(int studentId, int competitionId);
        Task<IEnumerable<StudentCompetition>> GetAll();
        Task Add(StudentCompetition entity);
        Task Update(StudentCompetition entity);
        Task Delete(int studentId, int competitionId);
        Task<StudentCompetition> GetByStudentAndCompetition(int studentId, int competitionId);
        
        Task<int> GetStudentCompetitionIdByAnswerId(int answerId);
        Task DeleteCompetition(int studentId, int competitionId);
        Task<StudentCompetition> GetStudentCompetitionById(int studentId, int competitionId);
        Task<IEnumerable<StudentCompetition>> GetCompetitionsById(int studentId);
    }
}
