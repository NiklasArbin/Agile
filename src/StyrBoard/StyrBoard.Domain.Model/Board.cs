using System;
using Raven.Client.Linq;

namespace StyrBoard.Domain.Model
{
    public class Board : IDescribed, IAggregateRoot
    {
        
        public Guid Id { get; set; }
        public int DisplayId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid SprintId { get; set; }
    }
}