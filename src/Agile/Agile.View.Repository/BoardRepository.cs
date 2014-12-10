using System.Collections.Generic;
using Agile.View.Model;

namespace Agile.View.Repository
{
    public interface IBoardRepository
    {
        List<Column> GetColumns();
    }

    public class BoardRepository : IBoardRepository
    {
        public List<Column> GetColumns()
        {
            var columns = new List<Column>
            {
                new Column
                {
                    Id = 1, Description = "Desc", Name = "Open",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 1, Description = "Lorizzle ipsizzle dolor own yo' amizzle, consectetizzle adipiscing bow wow wow. Pot velizzle, boom shackalack volutpizzle, dawg gizzle, da bomb vizzle, arcu. Pellentesque izzle tortizzle. You son of a bizzle eros. Shizzle my nizzle crocodizzle crunk dolizzle fo shizzle tempus tempor. Mauris pellentesque nibh et turpizzle. Vestibulum izzle .", Id = 1, Name = "Task 1"},
                        new Task{ColumnId = 1, Description = "Lorizzle ipsizzle dolor own yo' amizzle, consectetizzle adipiscing bow wow wow. Pot velizzle, boom shackalack volutpizzle, dawg gizzle, da bomb vizzle, arcu. Pellentesque izzle tortizzle. You son of a bizzle eros. Shizzle my nizzle crocodizzle crunk dolizzle fo shizzle tempus tempor. Mauris pellentesque nibh et turpizzle. Vestibulum izzle .", Id = 1, Name = "Task 1"}
                    }
                
                },
                new Column
                {
                    Id = 2, Description = "Desc", Name = "In Progress",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 2, Description = "TD", Id = 2, Name = "Task 1"}
                    }
                
                },
                new Column
                {
                    Id = 3, Description = "Desc", Name = "Testing",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 2, Description = "TD", Id = 2, Name = "Task 1"}
                    }
                
                },
                new Column
                {
                    Id = 4, Description = "Desc", Name = "Done",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 2, Description = "TD", Id = 2, Name = "Task 1"}
                    }
                },
                new Column
                {
                    Id = 4, Description = "Desc", Name = "Done",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 2, Description = "TD", Id = 2, Name = "Task 1"}
                    }
                }
            };
            return columns;
        }
    }
}
