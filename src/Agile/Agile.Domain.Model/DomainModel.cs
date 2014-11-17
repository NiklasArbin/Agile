using System;

namespace Agile.Domain.Model
{
    public interface IDomainModel
    {
        Guid Id { get; set; }
    }

    public abstract class DomainModel:IDomainModel
    {
        protected DomainModel()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
    }
}