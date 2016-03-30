// Created on        Created By
// JULY 2010      Gayathri (16727)

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.ComponentModel;
using Microsoft.Windows.Controls;
using System.Windows.Media;

namespace StandardCodes
{
    #region Converters

    public class SortDirectionConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            switch ((ListSortDirection)value)
            {
                case ListSortDirection.Ascending:
                    return "Ascending";
                case ListSortDirection.Descending:
                    return "Descending";
                default:
                    return null;
                    break;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((string)value)
            {
                case "null":
                    return null;
                case "Ascending":
                    return ListSortDirection.Ascending;
                case "Descending":
                    return ListSortDirection.Descending;
                default:
                    break;
            }

            return null;
        }

        #endregion
    }

    public class BackgroundConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                bool boolValue = (bool)value;
                if (boolValue)
                {
                    return Brushes.Aqua;
                }
                else
                {
                    return Brushes.BlanchedAlmond;
                }
                
            }
            return Brushes.LightGreen;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    public class ItemConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return CollectionView.NewItemPlaceholder;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        #endregion
    }

    public class ColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ColorItem colorItem = value as ColorItem;
            Brush brush = colorItem.MyBrush;
            return brush.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new ColorItem(Brushes.LightGreen);     
        }

        #endregion
    }    

    #endregion Converters
}
