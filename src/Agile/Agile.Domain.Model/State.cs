using System.Collections.Generic;

namespace Agile.Domain.Model
{
    public class State : IHaveATitle, IComparer<State>
    {
        public State()
        {
            Things = new SortedSet<Thing>();
        }
        public string Title { get; set; }
        public int SortOrder { get; set; }
        public SortedSet<Thing> Things { get; private set; }
        public int Compare(State x, State y)
        {
            return x.SortOrder - y.SortOrder;
        }
    }
}
