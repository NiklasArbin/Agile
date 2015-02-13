using System;
using System.Collections.Generic;

namespace StyrBoard.Domain.Model
{
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

            Items.Remove(id);
            Items.Insert(index, id);

        }
        
    }
}