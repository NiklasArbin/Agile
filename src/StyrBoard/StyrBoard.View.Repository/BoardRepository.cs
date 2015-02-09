using System.Collections.Generic;
using System.Linq;
using StyrBoard.View.Model;

namespace StyrBoard.View.Repository
{
    public interface IBoardRepository
    {
        List<Column> GetColumns();
        void MoveTask(int taskId, int targetColId);
        Task GetTask(int taskId);
        void DeleteTask(int taskId);
        int CreateTask(Task task);
    }

    public class BoardRepository : IBoardRepository
    {
        private List<Column> _columns;

        public List<Column> GetColumns()
        {
            if (_columns != null) return _columns;

            var columns = new List<Column>
            {
                new Column
                {
                    Id = 1, Description = "Desc", Name = "Open",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 1, Description = "Lorizzle ipsizzle dolor own yo' amizzle, consectetizzle adipiscing bow wow wow. Pot velizzle, boom shackalack volutpizzle, dawg gizzle, da bomb vizzle, arcu. Pellentesque izzle tortizzle. You son of a bizzle eros. Shizzle my nizzle crocodizzle crunk dolizzle fo shizzle tempus tempor. Mauris pellentesque nibh et turpizzle. Vestibulum izzle .", Id = 1, Name = "Task 1"},
                        new Task{ColumnId = 1, Description = "Lorizzle ipsizzle dolor own yo' amizzle, consectetizzle adipiscing bow wow wow. Pot velizzle, boom shackalack volutpizzle, dawg gizzle, da bomb vizzle, arcu. Pellentesque izzle tortizzle. You son of a bizzle eros. Shizzle my nizzle crocodizzle crunk dolizzle fo shizzle tempus tempor. Mauris pellentesque nibh et turpizzle. Vestibulum izzle .", Id = 2, Name = "Task 2"}
                    }
                
                },
                new Column
                {
                    Id = 2, Description = "Desc", Name = "In Progress",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 2, Description = "TD", Id = 3, Name = "Task 3"}
                    }
                
                },
                new Column
                {
                    Id = 3, Description = "Desc", Name = "Testing",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 3, Description = "TD", Id = 4, Name = "Task 4"}
                    }
                
                },
                new Column
                {
                    Id = 4, Description = "Desc", Name = "Completed",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 4, Description = "TD", Id = 5, Name = "Task 5"}
                    }
                },
                new Column
                {
                    Id = 5, Description = "Desc", Name = "Closed",
                    Tasks = new List<Task>
                    {
                        new Task{ColumnId = 5, Description = "TD", Id = 6, Name = "Task 6"}
                    }
                }
            };
            _columns = columns;
            return columns;
        }

        public Task GetTask(int taskId)
        {
            return _columns.Single(c => c.Tasks.Exists(t => t.Id == taskId)).Tasks.Single(t => t.Id == taskId);
        }

        public void DeleteTask(int taskId)
        {
            var column = _columns.Single(c => c.Tasks.Exists(t => t.Id == taskId));
            column.Tasks.RemoveAll(t => t.Id == taskId);
        }

        public int CreateTask(Task task)
        {
            var max = (from column in _columns from t in column.Tasks select t.Id).Concat(new[] {0}).Max();
            task.Id = max + 1;
            _columns[0].Tasks.Add(task);
            return task.Id;
        }

        public void MoveTask(int taskId, int targetColId)
        {
            var task = _columns.Single(c => c.Tasks.Exists(t => t.Id == taskId)).Tasks.Single(t => t.Id == taskId);
            var column = _columns.Single(c => c.Tasks.Any(t => t.Id == taskId));
            column.Tasks.Remove(task);
            task.ColumnId = targetColId;
            _columns.Single(c => c.Id == targetColId).Tasks.Add(task);

        }
    }
}
