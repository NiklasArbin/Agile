using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agile.Domain.Model
{
    public class UserStory : IDescribed
    {
        public UserStory()
        { 
            Tasks = new List<Task>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Task> Tasks { get; set; }


    }
}
