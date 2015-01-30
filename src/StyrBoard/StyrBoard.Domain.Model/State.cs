using System.Collections.Generic;

namespace StyrBoard.Domain.Model
{
    public class State
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public static IEnumerable<State> GetDefaultStates()
        {
            return new List<State>
            {
                new State {Name = "Open",  Id = 1},
                new State {Name = "In Progress", Id = 2},
                new State {Name = "Testing", Id = 3},
                new State {Name = "Completed", Id = 4},
                new State {Name = "Closed", Id = 5},
            };
        }
    }
}