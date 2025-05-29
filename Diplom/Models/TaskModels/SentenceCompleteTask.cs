using Diplom.Models.Enums;

namespace Diplom.Models.TaskModels;

public record SentenceCompleteTask(
    string Id,
    string Description,
    string Opt1,
    string Opt2,
    string Opt3,
    string Answer,
    AphasiaType[] Types,
    Severity[] Levels)
    : TaskBase(Id, Description, Types, Levels);
