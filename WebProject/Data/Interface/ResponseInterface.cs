using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface ResponseInterface
    {
        Task<Response> GetByIdResponse(int id);
        Task<IEnumerable<Response>> GetAllResponse();
        Task AddResponse(Response entity);
        Task UpdateResponse(Response entity);
        Task DeleteResponse(int id);
    }
}
