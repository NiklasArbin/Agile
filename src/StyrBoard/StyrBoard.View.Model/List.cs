using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyrBoard.View.Model
{
    public class List
    {
        public List()
        {
            Cards = new List<Card>();
        }

        public List<Card> Cards { get; set; }
    }
}
