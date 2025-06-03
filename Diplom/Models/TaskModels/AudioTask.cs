using Diplom.Models.Enums;

namespace Diplom.Models.TaskModels
{
    public record AudioTask(
    string Id,
    string Description,
    string Text,
    AphasiaType[] Types,
    Severity[] Levels)
    : TaskBase(Id, Description, Types, Levels);
}
