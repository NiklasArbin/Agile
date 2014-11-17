using System.Collections.Generic;

namespace Agile.Domain.Model
{
    public class Board : IHaveATitle
    {
        public string Title { get; set; }
        public SortedSet<State> States { get; set; }
    }
}