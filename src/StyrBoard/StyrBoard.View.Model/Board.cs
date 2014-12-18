using System.Collections.Generic;

namespace StyrBoard.View.Model
{
    public class Board
    {
        public Board()
        {
            Columns = new List<Column>();
        }
        public List<Column> Columns { get; set; }
    }

    public class Column
    {
        public Column()
        {
            Tasks = new List<Task>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Task> Tasks { get; set; }
    }

    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnId { get; set; }
    }
}
