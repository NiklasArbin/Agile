
using System;

namespace StyrBoard.Domain.Model
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
        int DisplayId { get; set; }
    }
}
