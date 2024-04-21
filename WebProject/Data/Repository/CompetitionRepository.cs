using Data.Models;
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
    public class CompetitionRepository : CompetitionInterface
    {
        private readonly AppDbContext _context;
        public CompetitionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Competition entity)
        {
            await _context.Competitions.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var competition = await GetById(id);
            if (competition != null)
            {
                _context.Competitions.Remove(competition);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Competition>> GetAll()
        {
            return await _context.Competitions.ToListAsync();
        }

        public async Task<Competition> GetById(int id)
        {
            return await _context.Competitions.FindAsync(id);
        }

        public async Task<Competition> GetByName(string competitionName)
        {
            return await _context.Competitions.FirstOrDefaultAsync(c => c.CompetitionName == competitionName);
        }

        public async Task<Competition> GetFirst()
        {
           return await _context.Competitions.OrderByDescending(c => c.CompetitionID).FirstOrDefaultAsync();
        }

        public async Task Update(Competition entity)
        {
            _context.Competitions.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
