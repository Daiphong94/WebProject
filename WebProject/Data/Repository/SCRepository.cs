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

        public async Task Delete(int id)
        {
            var SC = await GetById(id);
            if (SC != null)
            {
                _context.StudentCompetitions.Remove(SC);
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

        public async Task<StudentCompetition> GetById(int id)
        {
            return await _context.StudentCompetitions.FindAsync(id);
        }

        public async Task<StudentCompetition> GetByStudentAndCompetition(int studentId, int competitionId)
        {
            return await _context.StudentCompetitions.FirstOrDefaultAsync(sc => sc.StudentID == studentId && sc.CompetitionID == competitionId);
        }

        public async Task<IEnumerable<Competition>> GetCompetitionsById(int studentId)
        {
            return await _context.StudentCompetitions
        .Where(sc => sc.StudentID == studentId)
        .Select(sc => sc.Competition)
        .ToListAsync();
        }

        public async Task Update(StudentCompetition entity)
        {
            _context.StudentCompetitions.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
