using Diplom.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public record CompleteRowTask(
    string Id, string Description,
    string[] RowImages,
    string CorrectImage,
    string[] DistractorImages,
    AphasiaType Type, Severity Level)
    : TaskBase(Id, Description, Type, Level);
}
