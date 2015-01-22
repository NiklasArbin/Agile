using System;
using System.Collections.Generic;

namespace StyrBoard.Domain.Model
{
    public class Epic : IAggregateRoot, IDescribed
    {
        public Epic()
        {
            Features = new List<Feature>();
        }

        public Guid Id { get; set; }
        public int DisplayId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Feature> Features { get; set; }
    }
}