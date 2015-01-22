using System;

namespace StyrBoard.Domain.Model
{
    public class Bug : IDescribed, IAggregateRoot
    {
        public Guid Id { get; set; }
        public int DisplayId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
