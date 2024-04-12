using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class AnswerRepository : AnswerInterface
    {
        private readonly AppDbContext _context;
        public AnswerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Answer entity)
        {
            await _context.Answers.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var answer = await GetById(id);
            if(answer != null)
            {
                _context.Answers.Remove(answer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Answer>> GetAll()
        {
            return await _context.Answers.ToListAsync();
        }

        public async Task<Answer> GetById(int id)
        {
            return await _context.Answers.FindAsync(id);
        }

        public async Task<Answer> GetByStudentAndQuestion(int studentId, int questionId)
        {
            return await _context.Answers.FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.QuestionID == questionId);
        }

        public Task SubmitAnswer(Answer answer)
        {
            throw new NotImplementedException();
        }

        public async Task Update(Answer entity)
        {
            _context.Answers.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
