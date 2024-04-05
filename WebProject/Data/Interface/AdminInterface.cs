
using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface AdminInterface
    {
        Task<Admin> GetByIdAdmin(int id);
        Task<IEnumerable<Admin>> GetAllAdmin();
        Task AddAdmin(Admin entity);
        Task UpdateAdmin(Admin entity);
        Task DeleteAdmin(int id);

    }
}
