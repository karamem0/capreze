using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace Karamem0.Capreze.Interactivity
{

    public class WindowHandle : DependencyObject
    {

        public static readonly DependencyProperty HandleProperty = DependencyProperty.RegisterAttached(
            "Handle",
            typeof(IntPtr),
            typeof(DependencyObject),
            new PropertyMetadata(IntPtr.Zero)
        );

        public static void SetHandle(DependencyObject element, IntPtr value)
        {
            element.SetValue(HandleProperty, value);
        }

        public static IntPtr GetHandle(DependencyObject element)
        {
            return (IntPtr)element.GetValue(HandleProperty);
        }

    }

}
