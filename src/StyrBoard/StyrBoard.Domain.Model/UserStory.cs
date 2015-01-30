using System;
using System.Collections.Generic;

namespace StyrBoard.Domain.Model
{
    public class UserStory : IDescribed, IAggregateRoot, ICanHaveImpediments, IHaveState
    {
        public UserStory()
        {
            Tasks = new List<Task>();
            Impediments = new List<Impediment>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public int DisplayId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Task> Tasks { get; set; }
        public int SprintId { get; set; }
        public List<Impediment> Impediments { get; private set; }
        public State State { get; set; }

        public void ChangeState(string newState)
        {
            this.State.Name = newState;
        }
    }
}
