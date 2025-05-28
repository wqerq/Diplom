using Diplom.Models.Enums;

namespace Diplom.Models.TaskModels;

public record class CategoryTask : TaskBase
{
    public string[] Images { get; }
    public string Category { get; }
    public string[] Answers { get; }

    public CategoryTask(string id, string description,
                        string category, string[] images,
                        string[] answers,
                        AphasiaType[] t, Severity[] s)
        : base(id, description, t, s)
    {
        Category = category;
        Images = images;
        Answers = answers;
    }
}
