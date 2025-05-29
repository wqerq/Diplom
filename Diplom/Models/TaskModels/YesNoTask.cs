using Diplom.Models.Enums;

namespace Diplom.Models.TaskModels
{
    public record YesNoTask(
    string Id,
    string Description,
    string Image,
    bool Answer,
    AphasiaType[] Types,
    Severity[] Levels)
    : TaskBase(Id, Description, Types, Levels);
}
