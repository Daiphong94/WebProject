
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
    
    public class AdminRepository : AdminInterface
    {
        private readonly AppDbContext _context;

        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAdmin(Admin entity)
        {
            await _context.Admins.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAdmin(int id)
        {
            var admin = await GetByIdAdmin(id);
            if(admin != null)
            {
                _context.Admins.Remove(admin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Admin>> GetAllAdmin()
        {
            return await _context.Admins.ToListAsync();
        }

        public async Task<Admin> GetByIdAdmin(int id)
        {
            return await _context.Admins.FindAsync(id);
        }

        public async Task UpdateAdmin(Admin entity)
        {
            _context.Admins.Update(entity);
            await _context.SaveChangesAsync();
        }

        
    }
}
