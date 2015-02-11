﻿using System.Collections.Generic;
using System.Linq;
using StyrBoard.Domain.Model;
using StyrBoard.View.Model;


namespace StyrBoard.View.Repository.Mappings
{
    public static class BoardMapper
    {
        public static Model.Card ToViewModel(this UserStory domainModel)
        {
            return new Model.Card
            {
                Name = domainModel.Title,
                Description = domainModel.Description,
                Id = domainModel.Id,
                DisplayId = domainModel.DisplayId,
                ColumnId = domainModel.State.Id,
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

                column.Cards.Add(new View.Model.Card()
                {
                    Id = userStory.Id,
                    DisplayId = userStory.DisplayId,
                    ColumnId = column.Id,
                    Name = userStory.Title,
                    Description = userStory.Description,
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
