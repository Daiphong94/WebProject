using Data.Models;
using Data.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Data.Repository
{
    public class UserRepository : UserInterface
    {
        private readonly AppDbContext _context;
        

        public UserRepository(AppDbContext context)
        {
            _context = context;
           
        }

        public async Task Add(User entity)
        {
            await _context.Users.AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await GetById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(int id)
        {
            return await _context.Users.AnyAsync(u => u.UserID == id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> GetByName(string username)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.UserName == username);
        }

       

        public async Task Update(User entity)
        {
            _context.Users.Update(entity);
            await SaveChangesAsync();
        }


        private async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                
                throw ;
            }
        }
    }
}
