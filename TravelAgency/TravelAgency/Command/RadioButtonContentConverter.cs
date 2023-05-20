using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TravelAgency.Command
{
    public class RadioButtonContentConverter : IValueConverter
    {           
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
                var radioButtonContent = (string)parameter;
                var isChecked = (bool)value;

                return isChecked ? radioButtonContent : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
