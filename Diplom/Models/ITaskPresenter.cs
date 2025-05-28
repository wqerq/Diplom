using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public interface ITaskPresenter
    {
        TaskBase Task { get; set; }
        bool IsCompleted { get; }
        bool IsCorrect { get; }
        void Reset();
        void CheckAnswer();

        event EventHandler ContinueRequested;
    }

}
