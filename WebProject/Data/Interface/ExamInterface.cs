using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface ExamInterface
    {
        Task<Exam> GetById(int id);
        Task<IEnumerable<Exam>> GetAll();
        Task Add(Exam entity);
        Task Update(Exam entity);
        Task Delete(int id);
        Task<int> CalculateRank(int competitionId, float score);
        Task<IEnumerable<Exam>> GetExamsByCompetitionId(int competitionId);
    }
}
