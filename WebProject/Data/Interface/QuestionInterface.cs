using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface QuestionInterface
    {
        Task<Question> GetById(int id);
        Task<IEnumerable<Question>> GetAll();
        Task Add(Question entity);
        Task Update(Question entity);
        Task Delete(int id);
    }
}
