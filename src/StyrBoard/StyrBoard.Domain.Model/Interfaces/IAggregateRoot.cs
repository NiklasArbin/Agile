
using System;

namespace StyrBoard.Domain.Model
{
    public interface IAggregateRoot
    {
        Guid Id { get; set; }
        int DisplayId { get; set; }
    }
}
