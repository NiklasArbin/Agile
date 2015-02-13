using System;
using System.Collections.Generic;

namespace StyrBoard.Domain.Model
{
    public interface IPriority
    {
        int Add(Guid id);
        int Get(Guid id);
        void SetPriority(Guid id, int index);
        void InsertBefore(Guid id, Guid existing);
        void InsertAfter(Guid id, Guid existing);
    }

    public class Priority : IPriority
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

        public void SetPriority(Guid id, int index)
        {
            if (!Items.Contains(id)) return;

            var oldIndex = Get(id);

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

        public void InsertBefore(Guid id, Guid existing)
        {
            var index = Get(existing);
            SetPriority(id,index);
        }
        public void InsertAfter(Guid id, Guid existing)
        {
            var index = Get(existing) +1;
            SetPriority(id, index);
        }
        
    }
}