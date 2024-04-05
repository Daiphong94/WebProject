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
    public class EventRepository : EventInterface
    {
        private readonly AppDbContext _context;
        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Events entity)
        {
            await _context.Events.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var ev = await GetById(id);
            if (ev != null)
            {
                _context.Events.Remove(ev);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Events>> GetAll()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Events> GetById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task Update(Events entity)
        {
             _context.Events.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
