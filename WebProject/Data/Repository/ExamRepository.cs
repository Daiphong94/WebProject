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
            return await _context.Exams.ToListAsync();
        }

        public async Task<Exam> GetById(int id)
        {
           return await _context.Exams.FindAsync(id);
        }

        public async Task Update(Exam entity)
        {
            _context.Exams.Update(entity);
            await _context.AddRangeAsync();
        }
    }
}
