using Data.Interface;
using Data.Models;

namespace Data.Repository
{
    public class RegistrationRepository : RegistrationInterface
    {
        private readonly AppDbContext _context;
        public RegistrationRepository(AppDbContext context)
        {
            _context = context;
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
    }
}
