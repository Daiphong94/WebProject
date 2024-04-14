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
    public class SCRepository : SCInterface
    {
        private readonly AppDbContext _context;
        public SCRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(StudentCompetition entity)
        {
            await _context.StudentCompetitions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

            public async Task Delete(int studentId, int competitionId)
            {
                var SC = await GetById(studentId, competitionId);
                if (SC != null)
                {
                    _context.StudentCompetitions.Remove(SC);
                    await _context.SaveChangesAsync();
                }
            }

       

        public async Task DeleteCompetition(int studentId, int competitionId)
        {
            var sc = await _context.StudentCompetitions
                    .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CompetitionID == competitionId);

            if (sc != null)
            {
                _context.StudentCompetitions.Remove(sc);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<StudentCompetition>> GetAll()
        {
            return await _context.StudentCompetitions
        .Include(sc => sc.Student)
        .Include(sc => sc.Competition)
        .ToListAsync();
        }

        public async Task<StudentCompetition> GetById(int studentId, int competitionId)
        {
            return await _context.StudentCompetitions
        .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CompetitionID == competitionId);
        }

        public async Task<StudentCompetition> GetByStudentAndCompetition(int studentId, int competitionId)
        {
            return await _context.StudentCompetitions
        .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CompetitionID == competitionId);
        }

        public async Task<IEnumerable<StudentCompetition>> GetCompetitionsById(int studentId)
        {
            return await _context.StudentCompetitions
       .Include(sc => sc.Competition)
       .Where(sc => sc.StudentID == studentId)
       .ToListAsync();
        }

        public async Task<IEnumerable<Competition>> GetCompetitionsByStudentId(int studentId)
        {
            var competitions = await _context.Competitions
         .Where(c => _context.StudentCompetitions.Any(sc => sc.StudentID == studentId && sc.CompetitionID == c.CompetitionID))
         .ToListAsync();

            return competitions;
        }

        public async Task<StudentCompetition> GetStudentCompetitionById(int studentId, int competitionId)
        {
            return await _context.StudentCompetitions
       .FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CompetitionID == competitionId);
        }

        public async Task<int> GetStudentCompetitionIdByAnswerId(int answerId)
        {
            var studentCompetitionId = await _context.StudentCompetitions
       .Where(sc => sc.Student.Answers.Any(a => a.AnswerID == answerId))
       .Select(sc => sc.StudentID) 
       .FirstOrDefaultAsync();

            return studentCompetitionId;
        }

        public async Task Update(StudentCompetition entity)
        {
            _context.StudentCompetitions.Update(entity);
            await _context.SaveChangesAsync();
        }

       
    }
}
