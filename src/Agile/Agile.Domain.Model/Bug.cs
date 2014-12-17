using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agile.Domain.Model
{
    public class Bug : IDescribed, IAggregateRoot
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}
