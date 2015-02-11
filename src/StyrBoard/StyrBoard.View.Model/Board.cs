using System;
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
            Cards = new List<Card>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Card> Cards { get; set; }
    }

    public class Card
    {
        public Guid Id { get; set; }
        public int DisplayId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ColumnId { get; set; }
        public int Points { get; set; }
    }
}
