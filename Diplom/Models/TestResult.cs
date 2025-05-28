using Diplom.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public record TestResult
    {
        public DateTime Date { get; init; }
        public AphasiaType Type { get; init; }
        public Severity Level { get; init; }
        public int Total { get; init; }
        public int Correct { get; init; }
    }
}
