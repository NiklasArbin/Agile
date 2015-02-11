using System.Collections.Generic;
using System.Linq;
using StyrBoard.Domain.Model;
using StyrBoard.View.Model;


namespace StyrBoard.View.Repository.Mappings
{
    public static class BoardMapper
    {
        public static Model.Task ToViewModel(this UserStory domainModel)
        {
            return new Model.Task
            {
                Name = domainModel.Title,
                Description = domainModel.Description,
                Id = domainModel.Id,
                DisplayId = domainModel.DisplayId,
                ColumnId = domainModel.State.Id
                Points = domainModel.Points
            };
        }
        public static Board ToViewModel(this List<UserStory> domainModel)
        {
            var result = new Board
            {
                Columns = GetDefaultColumns()
            };

            foreach (var userStory in domainModel)
            {
                var column = result.Columns.SingleOrDefault(s => s.Name == userStory.State.Name);
                if (column == null)
                    column = result.Columns.Single(s => s.Name == "Open");

                column.Tasks.Add(new View.Model.Task()
                {
                    ColumnId = column.Id,
                    Name = userStory.Title,
                    Id = userStory.Id,
                    DisplayId = userStory.DisplayId
                    Points = userStory.Points
                });
            }

            return result;
        }

        public static List<Column> GetDefaultColumns()
        {
            return State.GetDefaultStates().Select(state => new Column()
            {
                Name = state.Name,
                Id = state.Id,
            }).ToList();
        }
    }
}
