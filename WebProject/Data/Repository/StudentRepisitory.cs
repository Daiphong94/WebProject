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
    public class StudentRepisitory : StudentInterface
    {
        private readonly AppDbContext _context;
        public StudentRepisitory(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddStudent(Student entity)
        {
            await _context.Students.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(int id)
        {
            var student = await GetByIdStudent(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByEmail(string email)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Email == email);
        }

        public async Task<Student> GetByIdStudent(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> GetByUserId(int userId)
        {
            return await _context.Students.SingleOrDefaultAsync(u => u.UserID == userId);
        }

        public Task<IEnumerable<Competition>> GetRegisteredCompetitions(int studentId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateStudent(Student entity)
        {
            _context.Students.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
