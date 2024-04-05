using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interface
{
    public interface SurveyInterface
    {
        Task<Survey> GetByIdSurvey(int id);
        Task<IEnumerable<Survey>> GetAllSurvey();
        Task AddSurvey(Survey entity);
        Task UpdateSurvey(Survey entity);
        Task DeleteSurvey(int id);
    }
}
