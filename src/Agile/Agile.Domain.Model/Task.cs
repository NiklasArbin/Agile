using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agile.Domain.Model
{
    public interface IDescribed
    {
        string Title { get; set; }
        string Description { get; set; }
    }
    public class Task : IDescribed
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
