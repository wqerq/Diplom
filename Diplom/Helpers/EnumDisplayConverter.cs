using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;

namespace Diplom.Helpers;

public class EnumDisplayConverter : IValueConverter
{
    public object Convert(object value, Type targetType,
                          object parameter, CultureInfo culture)
    {
        if (value is null) return null;

        var type = value.GetType();
        var member = type.GetMember(value.ToString()!)[0];

        var display = member.GetCustomAttribute<DisplayAttribute>();
        if (display?.GetName() is { } displayName)
            return displayName;

        var description = member.GetCustomAttribute<DescriptionAttribute>();
        if (description?.Description is { } descr)
            return descr;

        return value.ToString();
    }

    public object ConvertBack(object value, Type targetType,
                              object parameter, CultureInfo culture)
        => throw new NotSupportedException();
}
