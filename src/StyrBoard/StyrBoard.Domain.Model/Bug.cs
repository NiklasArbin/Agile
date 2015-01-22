using System;
using System.Collections.Generic;

namespace StyrBoard.Domain.Model
{
    public class Bug : IDescribed, IAggregateRoot, ICanHaveImpediments
    {
        public Bug()
        {
            Impediments = new List<Impediment>();
        }
        public Guid Id { get; set; }
        public int DisplayId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Impediment> Impediments { get; private set; }
    }
}
