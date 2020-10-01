using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace SuperService_FrontEnd.GUIHelpers
{
    public class Common
    {
        public static void OnPropertyChanged(PropertyChangedEventHandler propertyChanged, object obj, [CallerMemberName] string propertyName = null)
        {
            propertyChanged?.Invoke(obj, new PropertyChangedEventArgs(propertyName));
        }
    }
}
