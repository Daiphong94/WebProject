using Data.Models;

namespace Data.Interface
{
    public interface RegistrationInterface
    {
        Task RegisterUserAsync(Registration registration);
        Task<bool> ApproveRegistrationAsync(int registrationId);
        Task<bool> RejectRegistrationAsync(int registrationId);
        Task<Registration> GetById(int id);
        Task<User> GetUserByRegistrationId(int id);
        Task<IEnumerable<Registration>> GetAll();
        Task Delete(int id);
        Task DeleteRegis(int id);
        Task Add(Registration entity);
        
        
    }
}
