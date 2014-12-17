using System.Collections.Generic;

namespace Agile.Domain.Model
{
    public class Epic : IAggregateRoot, IDescribed
    {
        public Epic()
        {
            Features = new List<Feature>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Feature> Features { get; set; }
    }
}