using System.ComponentModel.DataAnnotations;

namespace Diplom.Models.Enums
{
    public enum Severity
    {
        [Display(Name = "Лёгкая")]
        SeverityOne,
        [Display(Name = "Средняя")]
        SeverityTwo,
        [Display(Name = "Тяжелая")]
        SeverityThree
    }
}
