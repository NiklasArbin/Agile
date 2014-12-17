using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agile.Domain.Model
{
    public class Feature: IAggregateRoot, IDescribed
    {
        public Feature()
        {
            UserStories = new List<UserStory>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<UserStory> UserStories { get; set; }
    }
}
