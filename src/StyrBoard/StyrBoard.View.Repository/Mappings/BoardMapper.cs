using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using StyrBoard.Domain.Model;
using StyrBoard.View.Model;
using Task = StyrBoard.View.Model.Task;


namespace StyrBoard.View.Repository.Mappings
{
    public static class BoardMapper
    {
        public static List<Task> ToViewModel(this IEnumerable<Domain.Model.Task> domainModel, UserStory story)
        {
            return domainModel.Select(x => new Task
            {
                Description = x.Description,
                Name = x.Title,
                UserStoryId = x.Id
            }).ToList();
            
        }
        public static Model.Card ToViewModel(this UserStory domainModel)
        {
            return new Model.Card
            {
                Name = domainModel.Title,
                Description = domainModel.Description,
                Id = domainModel.Id,
                DisplayId = domainModel.DisplayId,
                ColumnId = domainModel.State.Id,
                Points = domainModel.Points,
                Tasks = domainModel.Tasks.ToViewModel(domainModel)
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

                column.Cards.Add(new Card()
                {
                    Id = userStory.Id,
                    DisplayId = userStory.DisplayId,
                    ColumnId = column.Id,
                    Name = userStory.Title,
                    Description = userStory.Description,
                    Points = userStory.Points,
                    Tasks = userStory.Tasks.ToViewModel(userStory)
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
