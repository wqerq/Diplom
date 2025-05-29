using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Diplom.Models.Enums
{
    public enum AphasiaType
    {
        [Display(Name = "Сенсорная афазия")]
        SensorAphasia,
        [Display(Name = "Aфферентная моторная афазия")]
        AfferentMotornAphasia,
        [Display(Name = "Эфферентная моторная афазия")]
        EfferentMotornAphasia,
        [Display(Name = "Динамическая афазия")]
        DinamicAphasia,
        [Display(Name = "Акустико-мнестическая афазия")]
        AcousticaMnesticAphasia,
        [Display(Name = "Cемантическая афазия")]
        SemanticAphasia
    }
}
