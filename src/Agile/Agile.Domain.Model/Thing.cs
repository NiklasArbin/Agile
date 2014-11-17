using System.Collections.Generic;

namespace Agile.Domain.Model
{
    public class Thing : IHaveATitle, IComparer<Thing>
    {
        public Thing()
        {
            Children = new List<Thing>();
            Parents = new List<Thing>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public IEnumerable<Thing> Children { get; private set; }
        public IEnumerable<Thing> Parents { get; private set; }
        public int Compare(Thing x, Thing y)
        {
            return x.SortOrder - y.SortOrder;
        }
    }
}