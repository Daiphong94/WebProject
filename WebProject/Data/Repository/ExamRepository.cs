using Data.Models;
using Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class ExamRepository : ExamInterface
    {
        private readonly AppDbContext _context;
        public ExamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Exam entity)
        {
            await _context.Exams.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> CalculateRank(int competitionId, float score)
        {
            var examScores = await _context.Exams
       .Where(e => e.CompetitionID == competitionId)
       .Select(e => e.Score)
       .ToListAsync();

            if (examScores.Any())
            {
                var sortedScores = examScores.OrderBy(e => e).ToList();
                var rank = sortedScores.IndexOf(score) + 1;

                var sameScoreCount = sortedScores.Count(s => s == score);
                if (sameScoreCount > 1)
                {
                    var totalRank = 0;
                    for (int i = 0; i < sameScoreCount; i++)
                    {
                        totalRank += rank + i;
                    }
                    return totalRank / sameScoreCount;
                }

                return sortedScores.Count - rank + 1;
            }
            else
            {
                return -1;
            }
        }

        public async Task Delete(int id)
        {
            var exam = await GetById(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Exam>> GetAll()
        {
            return await  _context.Exams
                .Include(e => e.Competition)
                .Include(e => e.Student)
                .ToListAsync();
        }

        public async Task<Exam> GetById(int id)
        {
           return await _context.Exams.FindAsync(id);
        }

        public async Task Update(Exam entity)
        {
            var existingExam = await _context.Exams.FindAsync(entity.ExamID);
            if (existingExam == null)
            {
                
                throw new Exception("Not Found");
            }

            
            existingExam.Score = entity.Score;
            existingExam.Rank = entity.Rank; 

            
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Exam>> GetExamsByCompetitionId(int competitionId)
        {
            return await _context.Exams.Where(e => e.CompetitionID == competitionId).ToListAsync();
        }
    }
}
