using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SuperService_FrontEnd.GUIHelpers
{
    public class Common
    {
        public static void OnPropertyChanged(PropertyChangedEventHandler propertyChanged, object obj, [CallerMemberName] string propertyName = null)
        {
            propertyChanged?.Invoke(obj, new PropertyChangedEventArgs(propertyName));
        }
        public static IEnumerable<Button> GetAllButtonsInWindow(Window window)
        {
            var grid = LogicalTreeHelper.GetChildren(window).OfType<Grid>().First();
            return LogicalTreeHelper.GetChildren(grid).OfType<Button>();
        }
    }
}
