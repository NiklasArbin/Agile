using System.Collections.Generic;
using System.Linq;
using StyrBoard.Domain.Model;
using StyrBoard.View.Model;

namespace StyrBoard.View.Repository.Mappings
{
    public static class BoardMapper
    {
        public static Board ToViewModel(this List<UserStory> domainModel)
        {
            var result = new Board
            {
                Columns = new List<Column>
                {
                    new Column{Name = "Open"},
                    new Column{Name = "In Progress"},
                    new Column{Name = "Testing"},
                    new Column{Name = "Completed"},
                    new Column{Name = "Closed"},
                }
            };

            foreach (var userStory in domainModel)
            {
                var column = result.Columns.SingleOrDefault(s => s.Name == userStory.State.Name);
                if (column == null)
                    column = result.Columns.Single(s => s.Name == "Open");

                column.Tasks.Add(new View.Model.Task()
                {
                    Name = userStory.Title,
                    Id = userStory.DisplayId
                });
            }

            return result;
        }
    }
}
