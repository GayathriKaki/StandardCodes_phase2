// Created on        Created By
// JULY 2010      Gayathri (16727)

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Windows.Controls;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.Windows.Media;

namespace StandardCodes
{
    /// <summary>
    /// Custom Template column that adds a binding property like DataGridBoundColumn
    /// </summary>
    public class DataGridCustomTemplateColumn : DataGridTemplateColumn
    {
        private BindingBase _binding;

        public virtual BindingBase Binding
        {
            get { return _binding; }
            set
            {
                _binding = value;

                // If it is a standard Binding, then set the mode to TwoWay
                Binding binding = _binding as Binding;
                if (binding != null)
                {
                    if (binding.Mode != BindingMode.TwoWay)
                    {
                        binding.Mode = BindingMode.TwoWay;
                        binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    }
                }
            }
        }

        /// <summary>
        ///     Creates the visual tree that will become the content of a cell.
        /// </summary>
        protected override FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
        {
            FrameworkElement template = base.GenerateElement(cell, dataItem);

            ContentPresenter cp = template as ContentPresenter;
            FrameworkElement fe = null;
            if (cp != null)
            {
                fe = cp.ContentTemplate.LoadContent() as FrameworkElement;

                //TODO: can make generic
                TextBlock tb = null;
                tb = fe is TextBlock ? fe as TextBlock : Helper.GetVisualChild<TextBlock>(fe);
                if (tb != null)
                {
                    BindingOperations.SetBinding(tb, TextBlock.TextProperty, Binding);                                
                }
            }

            return fe;
        }

        /// <summary>
        ///     Creates the visual tree that will become the content of a cell.
        /// </summary>
        protected override FrameworkElement GenerateEditingElement(DataGridCell cell, object dataItem)
        {
            FrameworkElement template = base.GenerateEditingElement(cell, dataItem);

            ContentPresenter cp = template as ContentPresenter;
            FrameworkElement fe = null;
            if (cp != null)
            {
                fe = cp.ContentTemplate.LoadContent() as FrameworkElement;

                //TODO: can make generic
                TextBox tb = null;
                tb = fe is TextBox ? fe as TextBox : Helper.GetVisualChild<TextBox>(fe);
                if (tb != null)
                {
                    BindingOperations.SetBinding(tb, TextBox.TextProperty, Binding);
                }
            }

            return fe;
        }
    }
}
