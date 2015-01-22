using System.Collections.Generic;

namespace StyrBoard.Domain.Model
{
    public interface ICanHaveImpediments
    {
        List<Impediment> Impediments { get; }
    }
}