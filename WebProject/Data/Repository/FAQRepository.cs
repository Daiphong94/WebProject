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
    public class FAQRepository : FAQInterface
    {
        private readonly AppDbContext _context;
        public FAQRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddFAQ(FAQ entity)
        {
            await _context.FAQs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFAQ(int id)
        {
            var faq = await GetByIdFAQ(id);
            if (faq != null)
            {
                _context.FAQs.Remove(faq);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<FAQ>> GetAllFAQ()
        {
            return await _context.FAQs.ToListAsync();
        }

        public async Task<FAQ> GetByIdFAQ(int id)
        {
            return await _context.FAQs.FindAsync(id);
        }

        public async Task UpdateFAQ(FAQ entity)
        {
            _context.FAQs.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
