using Diplom.Models.Enums;

namespace Diplom.Models.TaskModels;

public record class CompleteRowTask : TaskBase
{
    public string[] Row { get; }
    public string[] Variants { get; }
    public string Correct { get; }

    public CompleteRowTask(string id, string description,
                           string[] row, string[] variants,
                           string correct,
                           AphasiaType[] t, Severity[] s)
        : base(id, description, t, s)
    {
        Row = row;
        Variants = variants;
        Correct = correct;
    }
}
