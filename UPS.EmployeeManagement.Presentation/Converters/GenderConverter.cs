using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace UPS.EmployeeManagement.Presentation.Converters
{
    public class GenderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is string gender && gender.Equals(parameter);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isChecked && isChecked)
                return parameter;
            return Binding.DoNothing;
        }
    }
}
