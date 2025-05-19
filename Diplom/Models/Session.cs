using Diplom.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Models;

public sealed class Session
{
    public AphasiaType Type { get; }
    public Severity Level { get; }

    public int Total { get; }
    public int Index { get; private set; }
    public int Correct { get; private set; }

    public Session(AphasiaType type, Severity level, int total)
        => (Type, Level, Total) = (type, level, total);

    public void IncCorrect() => Correct++;

    public void NextTask() => Index++;

    public bool IsFinished => Index >= Total;
}
