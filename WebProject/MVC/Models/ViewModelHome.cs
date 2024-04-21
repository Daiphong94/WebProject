using Data.Models;

namespace MVC.Models
{
    public class ViewModelHome
    {
        public IEnumerable<Data.Models.Competition> Competition { get; set; }
        public IEnumerable<Data.Models.FAQ> FAQ { get; set; }
        public IEnumerable<Data.Models.Events> Events { get; set; }
        public Data.Models.Competition FirstCompetition { get; set; }
    }
}
