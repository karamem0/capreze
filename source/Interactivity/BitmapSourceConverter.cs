//
// Copyright (c) 2021 karamem0
//
// This software is released under the MIT License.
//
// https://github.com/karamem0/capreze/blob/master/LICENSE
//

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Karamem0.Capreze.Interactivity
{

    public class BitmapSourceConverter : IValueConverter
    {

        public BitmapSourceConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string path)
            {
                var hicon = Icon.ExtractAssociatedIcon(path).Handle;
                var rect = Int32Rect.Empty;
                var options = BitmapSizeOptions.FromEmptyOptions();
                return Imaging.CreateBitmapSourceFromHIcon(hicon, rect, options);
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

    }

}
