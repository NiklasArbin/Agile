
using System;

using StyrBoard.Domain.Model;

namespace StyrBoard.Tests
{
    public class TestDomainEntity : IAggregateRoot
    {
        public Guid Id { get; set; }
        public int DisplayId { get; set; }
    }
}
