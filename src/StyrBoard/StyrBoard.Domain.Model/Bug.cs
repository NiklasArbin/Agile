using System;
using System.Collections;
using System.Collections.Generic;

namespace StyrBoard.Domain.Model
{
    public class Bug : IDescribed, IAggregateRoot, ICanHaveImpediments, IHaveState
    {
        public Bug()
        {
            Impediments = new List<Impediment>();
        }
        public Guid Id { get; set; }
        public int DisplayId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public List<Impediment> Impediments { get; private set; }
        public State State { get; set; }
    }

    public class Priority
    {
        public Priority()
        {
            Items = new List<Guid>();
        }

        private List<Guid> Items { get; set; }

        public int Add(Guid id)
        {
            if(!Items.Contains(id)) Items.Add(id);
            return Items.IndexOf(id);
        }

        public int Get(Guid id)
        {
            return Items.IndexOf(id);
        }

        public void Move(Guid id, int index)
        {
            if (!Items.Contains(id)) return;

            var oldIndex = Items.IndexOf(id);

            var minIndex = Math.Min(index, oldIndex);
            var maxIndex = Math.Max(index, oldIndex);

            var changed = new List<Guid>();

            for (int i = minIndex; i <= maxIndex; i++)
            {
                changed.Add(Items[i]);
            }

            //0
            //  3->1
            //1 1->2
            //2 2->3
            //3 3-> null
            //4
            //5
            Items.Remove(id);
            Items.Insert(index, id);

        }
        
    }
}
