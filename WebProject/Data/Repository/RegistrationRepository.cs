using Data.Interface;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class RegistrationRepository : RegistrationInterface
    {
        private readonly AppDbContext _context;
        
        public RegistrationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(Registration entity)
        {
            await _context.Registrations.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public  async Task<bool> ApproveRegistrationAsync(int registrationId)
        {
            var registration = await _context.Registrations.FindAsync(registrationId);
            if (registration != null)
            {
                registration.Status = "Approved";
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task Delete(int id)
        {
            var regis = await GetById(id);
            if (regis != null)
            {
                _context.Registrations.Remove(regis);
                await _context.SaveChangesAsync();

                int userId = regis.UserID;
                var user = await _context.Users.FindAsync(userId);
                if (user != null)
                {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task DeleteRegis(int id)
        {
            var regis = await GetById(id);
            if (regis != null)
            {
                _context.Registrations.Remove(regis);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Registration>> GetAll()
        {
            return await _context.Registrations
           
           .Include(r => r.User)
           .ToListAsync();
        }

        public async Task<Registration> GetById(int id)
        {
            return await _context.Registrations.FindAsync(id);
        }

        public async Task<User> GetUserByRegistrationId(int id)
        {
            var registration = await _context.Registrations.FindAsync(id);
            if (registration != null)
            {
                
                return await _context.Users.FindAsync(registration.UserID);
            }
            return null;
        }

        public async Task RegisterUserAsync(Registration registration)
        {
            try
            {
                _context.Registrations.Add(registration);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred while saving changes: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RejectRegistrationAsync(int registrationId)
        {
            var registration = await _context.Registrations.FindAsync(registrationId);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }    
    }
}
