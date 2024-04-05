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
    public class FacultyRepository : FacultyInterface
    {
        private readonly AppDbContext _context;

        public FacultyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddFaculty(Faculty entity)
        {
            await _context.Faculties.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFaculty(int id)
        {
            var faculty = await GetByIdFaculty(id);
            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Faculty>> GetAllFaculty()
        {
            return await _context.Faculties.ToListAsync();
        }

        public async Task<Faculty> GetByIdFaculty(int id)
        {
            return await _context.Faculties.FindAsync(id);
        }

        public async Task UpdateFaculty(Faculty entity)
        {
            _context.Faculties.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
