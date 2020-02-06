using System;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using Service.Models;

namespace QRGenerator.Shared
{
    public class GenBtnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var data = (Status)value;
            return data == Status.Enable ? "True" : "False";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }


    public class radioBoolToIntConverter : IValueConverter
    {
       
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int integer = (int)value;
            if (integer == int.Parse(parameter.ToString()))
                return true;
            else
                return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }

    }


    public class TextInputToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Always test MultiValueConverter inputs for non-null 
            // (to avoid crash bugs for views in the designer) 
            if (values[0] is bool && values[1] is bool)
            {
                bool hasText = !(bool)values[0];
                bool hasFocus = (bool)values[1];
                if (hasFocus || hasText)
                    return Visibility.Collapsed;
            }
            return Visibility.Visible;
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    //public class RestartVisibilityConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var data = (ApplicationStatus)value;
    //        return data == ApplicationStatus.Stopped ? Visibility.Hidden : Visibility.Visible;
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return Binding.DoNothing;
    //    }
    //}

    //public class BooleanVisibilityConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return (bool)value ? Visibility.Visible : Visibility.Hidden;
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return Binding.DoNothing;
    //    }
    //}
    //public class ModeTextConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var data = (ApplicationStatus)value;
    //        switch (data)
    //        {
    //            case ApplicationStatus.Disable:
    //                return "Disabled";
    //            case ApplicationStatus.Running:
    //                return "Running";
    //            case ApplicationStatus.ShuttingDown:
    //                return "Shutting Down";
    //            case ApplicationStatus.Starting:
    //                return "Starting";
    //            case ApplicationStatus.Stopped:
    //                return "Stopped";
    //        }
    //        return Binding.DoNothing;
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return Binding.DoNothing;
    //    }
    //}

    //public class StatusConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var data = (ApplicationStatus)value;
    //        switch (data)
    //        {
    //            case ApplicationStatus.Disable:
    //                return Binding.DoNothing;
    //            case ApplicationStatus.Running:
    //                return new SolidColorBrush(Colors.Green);
    //            case ApplicationStatus.ShuttingDown:
    //                return new SolidColorBrush(Colors.Orange);
    //            case ApplicationStatus.Starting:
    //                return new SolidColorBrush(Colors.Yellow);
    //            case ApplicationStatus.Stopped:
    //                return new SolidColorBrush(Colors.Red);
    //        }
    //        return Binding.DoNothing;
    //    }

    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return Binding.DoNothing;
    //    }
    //}
    //public class CheckboxTextConverter : IValueConverter
    //{
    //    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        var data = (bool)value;
    //        return data ? "Enabled" : "Disabled";
    //    }
    //    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    //    {
    //        return Binding.DoNothing;
    //    }
    //}
}
