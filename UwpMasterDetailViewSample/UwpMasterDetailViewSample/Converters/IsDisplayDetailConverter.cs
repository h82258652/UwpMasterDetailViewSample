using System;
using Windows.UI.Xaml.Data;
using UwpMasterDetailViewSample.Views;

namespace UwpMasterDetailViewSample.Converters
{
    public class IsDisplayDetailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value is NoDetailView == false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}