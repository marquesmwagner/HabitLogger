using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitLogger.Models
{
    internal class HabbitLogger
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
    }
}
