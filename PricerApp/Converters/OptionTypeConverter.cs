using System;
using System.Globalization;
using System.Windows.Data;
using Pricer;

namespace PricerApp.Converters
{
    public class OptionTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return false;
            }

            return value.ToString().Equals(parameter.ToString(), StringComparison.InvariantCulture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null)
            {
                return Binding.DoNothing;
            }

            if ((bool)value && Enum.TryParse(parameter.ToString(), out OptionType optionType))
            {
                return optionType;
            }

            return Binding.DoNothing;
        }
    }
}
