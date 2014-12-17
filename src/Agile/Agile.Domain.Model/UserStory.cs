using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agile.Domain.Model
{

    public interface IAggregateRoot
    {
        int Id { get; set; }
    }
    public class UserStory : IDescribed, IAggregateRoot
    {
        public UserStory()
        {
            Tasks = new List<Task>();
            Impediments = new List<Impediment>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Task> Tasks { get; set; }
        public int SprintId { get; set; }
        public List<Impediment> Impediments { get; set; }
    }
}
