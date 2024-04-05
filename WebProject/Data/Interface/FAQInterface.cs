using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface FAQInterface
    {
        Task<FAQ> GetByIdFAQ(int id);
        Task<IEnumerable<FAQ>> GetAllFAQ();
        Task AddFAQ(FAQ entity);
        Task UpdateFAQ(FAQ entity);
        Task DeleteFAQ(int id);
    }
}
