using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities.Concrete
{
    public class Log : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Detail { get; set; }
        public string Audit { get; set; }
    }
}
