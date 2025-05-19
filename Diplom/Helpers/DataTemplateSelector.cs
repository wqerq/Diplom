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

        protected override DataTemplate OnSelectTemplate(object item, BindableObject _)
        {
            return item switch
            {
                Models.FindOddTask => FindOddTemplate!,
                Models.CompleteRowTask => CompleteRowTemplate!,
                _ => throw new NotSupportedException($"No template for {item}")
            };
        }
    }
}
