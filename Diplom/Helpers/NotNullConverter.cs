﻿using System.Globalization;


namespace Diplom.Helpers
{
    public class NotNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                              object parameter, CultureInfo culture) => value != null;

        public object ConvertBack(object value, Type targetType,
                                  object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }

}
