using Diplom.Models.Enums;

namespace Diplom.Models
{
    public record FindOddTask(
    string Id, string Description,
    string[] Images, int OddIndex,
    AphasiaType[] Types, Severity[] Levels)
    : TaskBase(Id, Description, Types, Levels);
}
