using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace IHS.WPF.UI.ProgressIndicator
{
    /// <summary>
    /// Interaction logic for ProgressIndicatorWndow.xaml
    /// </summary>
    public partial class ProgressIndicatorWindow : Window
    {
        public ProgressIndicatorWindow()
        {
            InitializeComponent();
            this.Background = Brushes.White;
            this.Height = 75;
            this.Width = 75;
            System.Windows.Data.Binding bi = new System.Windows.Data.Binding();
            bi.Source = new Uri("../Images/twizzle.gif", UriKind.RelativeOrAbsolute);
            icoDisplay.SetBinding(Image.SourceProperty, bi);
            icoDisplay.SetValue(Image.WidthProperty, 75.0);
            icoDisplay.SetValue(Image.HeightProperty, 75.0);
            icoDisplay.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            icoDisplay.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            myButton_Click();
        }
        public void myButton_Click()
        {
            DoubleAnimation da = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(1)));
            RotateTransform rt = new RotateTransform();
            icoDisplay.RenderTransform = rt;
            icoDisplay.RenderTransformOrigin = new Point(0.5, 0.5);
            da.RepeatBehavior = RepeatBehavior.Forever;
            rt.BeginAnimation(RotateTransform.AngleProperty, da);
        }
    }
}
