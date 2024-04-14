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
    public class QuestionRepository : QuestionInterface
    {
        private readonly AppDbContext _context;
        public QuestionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Question entity)
        {
            await _context.Questions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var question = await GetById(id);
            if (question != null)
            {
                _context.Questions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Question>> GetAll()
        {
            return await _context.Questions.Include(q => q.Competition).ToListAsync();
        }

        public async Task<List<Question>> GetByCompetitionID(int Id)
        {
            return await _context.Questions.Where(q => q.CompetitionID == Id).ToListAsync();
        }

        public async Task<Question> GetById(int id)
        {
            return await _context.Questions.FindAsync(id);
        }

        public async Task<string> GetCompetitionByQuestionIdAsync(int questionId)
        {
            var competitionName = await _context.Competitions
        .Where(c => c.Questions.Any(q => q.QuestionID == questionId))
        .Select(c => c.CompetitionName)
        .SingleOrDefaultAsync();

            return competitionName;
        }

        public async Task<int> GetQuestionIdByCompetitionId(int id)
        {
            var questionId = await _context.Questions
       .Where(q => q.CompetitionID == id)
       .Select(q => q.QuestionID)
       .FirstOrDefaultAsync();

            return questionId;
        }
            public async Task Update(Question entity)
        {
            _context.Questions.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
