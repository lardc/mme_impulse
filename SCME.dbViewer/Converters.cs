﻿using System;
using System.Windows.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SCME.dbViewer.ForParameters;
using System.Windows;
using System.Windows.Media;

namespace SCME.dbViewer
{
    public class IntToVisibilityConverter : IValueConverter
    {
        public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            int i = (int)Value;

            switch (i)
            {
                case 0:
                    return Visibility.Hidden;

                default:
                    return Visibility.Visible;
            }
        }

        public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
        {
            throw new InvalidOperationException("ConvertBack method is not implemented in IntToVisibilityConverter");
        }
    }

    /*
        public class TemperatureConditionToStrConverter : IValueConverter
        {        
            public object Convert(object Value, Type TargetType, object Parameter, CultureInfo Culture)
            {
                TemperatureCondition tc = (TemperatureCondition)Value;

                switch (tc)
                {
                    case TemperatureCondition.None:
                        return string.Empty;

                    default:
                        return tc.ToString();
                }
            }

            public object ConvertBack(object Value, Type TargetType, object Parameter, CultureInfo Culture)
            {
                throw new InvalidOperationException("ConvertBack method is not implemented in TemperatureConditionToStrConverter");
            }        
        }
    */
}
