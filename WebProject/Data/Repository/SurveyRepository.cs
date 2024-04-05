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
    public class SurveyRepository : SurveyInterface
    {
        private readonly AppDbContext _context;
        public SurveyRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddSurvey(Survey entity)
        {
            await _context.Surveys.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSurvey(int id)
        {
            var survey = await GetByIdSurvey(id);
            if (survey != null)
            {
                _context.Surveys.Remove(survey);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Survey>> GetAllSurvey()
        {
            return await _context.Surveys.ToListAsync();
        }

        public async Task<Survey> GetByIdSurvey(int id)
        {
            return await _context.Surveys.FindAsync(id);
        }

        public async Task UpdateSurvey(Survey entity)
        {
            _context.Surveys.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
