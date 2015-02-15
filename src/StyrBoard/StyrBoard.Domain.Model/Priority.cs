using System;
using System.Collections.Generic;
using System.Linq;

namespace StyrBoard.Domain.Model
{
    public interface IPriority
    {
        int Add(Guid id);
        int Get(Guid id);
        void Delete(Guid id);
        KeyValuePair<Guid, int>[] SetPriority(Guid id, int index);
        
    }

    public class Priority : IPriority
    {
        public Priority()
        {
            Items = new List<Guid>();
        }

        public Guid Id { get { return new Guid("1915B34F-EAF8-4A84-A6EB-74C518B89270"); }}
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

        public void Delete(Guid id)
        {
            if (!Items.Contains(id)) return;
            Items.Remove(id);
        }

        public KeyValuePair<Guid, int>[] SetPriority(Guid id, int index)
        {
            if (!Items.Contains(id)) return new KeyValuePair<Guid, int>[0];

            var oldIndex = Get(id);

            Items.RemoveAt(oldIndex);
            Items.Insert(index, id);

            var changed = new Dictionary<Guid,int>();

            for (var i = Math.Min(index, oldIndex); i <= Math.Max(index, oldIndex); i++)
            {
                changed.Add(Items[i], i);
            }
            
            return changed.ToArray();
        }
    }
}