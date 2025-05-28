using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models
{
    public interface IResultStorage
    {
        void Save(TestResult r);
        IReadOnlyList<TestResult> LoadAll();
    }
}
