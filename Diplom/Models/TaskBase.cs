using Diplom.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public abstract record TaskBase(
    string Id, string Description,
    AphasiaType Type, Severity Level);
}
