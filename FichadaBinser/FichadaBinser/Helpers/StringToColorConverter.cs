using FichadaBinser.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace FichadaBinser.Helpers
{
    public class StringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return Color.Black;

            switch (System.Convert.ToChar(value))
            {
                case ((char)EnumTextColor.Black):
                    return Color.Black;

                case ((char)EnumTextColor.Green):
                    return Color.Green;

                case ((char)EnumTextColor.Gray):
                    return Color.Gray;

                case ((char)EnumTextColor.Red):
                    return Color.Red;

                case ((char)EnumTextColor.White):
                    return Color.White;

                default:
                    return Color.Black;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
