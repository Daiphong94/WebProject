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
    public class ResponseRepository : ResponseInterface
    {

        private readonly AppDbContext _context;
        public ResponseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddResponse(Response entity)
        {
            await _context.Responses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResponse(int id)
        {
            var response = await GetByIdResponse(id);
            if (response != null)
            {
                _context.Responses.Remove(response);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Response>> GetAllResponse()
        {
            return await _context.Responses.ToListAsync();
        }

        public async Task<Response> GetByIdResponse(int id)
        {
            return await _context.Responses.FindAsync(id);
        }

        public async Task UpdateResponse(Response entity)
        {
            _context.Responses.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
