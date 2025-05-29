using Diplom.Models;
using Diplom.Models.TaskModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Helpers
{
    public sealed class TaskTemplateSelector : DataTemplateSelector
    {
        public DataTemplate? FindOddTemplate { get; set; }
        public DataTemplate? CompleteRowTemplate { get; set; }
        public DataTemplate? CategoryTemplate { get; set; }
        public DataTemplate? YesNoTemplate { get; set; }
        public DataTemplate? TrueFalseTemplate { get; set; }
        public DataTemplate? SentenceTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject _)
        => item switch
        {
            FindOddTask => FindOddTemplate!,
            CompleteRowTask => CompleteRowTemplate!,
            CategoryTask => CategoryTemplate!,
            YesNoTask => YesNoTemplate!,
            SentenceCompleteTask => SentenceTemplate!,
            _ => throw new NotSupportedException()
        };
    }
}
