using Data.Models;

namespace Data.Interface
{
    public interface RegistrationInterface
    {
        Task RegisterUserAsync(Registration registration);
    }
}
