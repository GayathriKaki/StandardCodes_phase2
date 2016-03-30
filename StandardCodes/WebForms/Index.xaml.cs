using System;
using System.Collections.Generic;

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StandardCodes.BusinessLayer;
using System.Diagnostics;



namespace StandardCodes
{
    /// <summary>
    /// Interaction logic for index.xaml
    /// </summary>
    public partial class index : Page
    {

        MenuItem mnuUnselect = new MenuItem();
        public index()
        {
            try
            {
                InitializeComponent();                
                lblheader.Content = " Standard Code Forms - Welcome " + Constants.strLogin.strUserName + " ; " + " Role : " + Constants.strLogin.strRole;
 
            }
            catch (Exception ex)
            {
            }
           
        }

        private void BindMenuItemIcon(MenuItem mi, MenuItem mnuUnselect)
        {

          
           

            ////'Close Icon  for menu item 
            BitmapImage imgClose = new BitmapImage();
            Uri myCloseUri;
            Image iconCloseImage = new Image();

            imgClose.BeginInit();
            myCloseUri = new Uri("../Images/closeico.gif", UriKind.RelativeOrAbsolute);
            imgClose.UriSource = myCloseUri;
            imgClose.EndInit();
            iconCloseImage.Source = imgClose;
            iconCloseImage.Width = 12;
            iconCloseImage.Height = 12;


            //'open Icon for menu item 
            BitmapImage imgOpen = new BitmapImage();
            Uri myOpenUri;
            Image iconOpenImage = new Image();

            imgOpen.BeginInit();
            myOpenUri = new Uri("../Images/openico.gif", UriKind.RelativeOrAbsolute);
            imgOpen.UriSource = myOpenUri;
            imgOpen.EndInit();
            iconOpenImage.Source = imgOpen;
            iconOpenImage.Width = 12;
            iconOpenImage.Height = 12;

            mnuUnselect.Icon = iconCloseImage;
            mnuUnselect.EndInit();
            mnuUnselect.Background = Brushes.Transparent;
            LinearGradientBrush lgb = new LinearGradientBrush();
            lgb.StartPoint = new Point(0.5, 0);
            lgb.EndPoint = new Point(0.5, 1);
            lgb.GradientStops.Add(new GradientStop(Color.FromRgb(129, 164, 210), 1));
            lgb.GradientStops.Add(new GradientStop(Color.FromRgb(129, 164, 210), 0));
            lgb.GradientStops.Add(new GradientStop(Color.FromRgb(188, 211, 243), 0.5));
            mnuUnselect.Background = lgb;
            mnuUnselect.Foreground = Brushes.Black;
          
            mi.Icon = iconOpenImage;
            mi.EndInit();
            mi.Background = Brushes.Navy;
            mi.Foreground = Brushes.White;
           
       

           
        }

       


        private void mnuItemBA_Click(object sender, RoutedEventArgs e)
        {

            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new  WebForms .BAForms());  

        }
        
        private void mnuItemFieldR_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new  WebForms.FieldReservForms ());

        }

        private void mnuItemMisc_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms .MiscRequest());

        }

        private void mnuItemQA_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            //this.frame2.NavigationService.Navigate(new WebForms .QualityAnalysis());

        }

        private void mnuItemAbout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted)
                {
                    var serverURL = System.Windows.Interop.BrowserInteropHelper.Source.ToString();
                    var x = serverURL.Substring(0, serverURL.LastIndexOf("/"));
                    Process.Start(x + "/WebHelp/Standard_Codes_V1.0.htm");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Help file not found.");
            }




           
        }

        private void mnuItemSearch_Click(object sender, RoutedEventArgs e)
        {

            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.SearchStandardCode());



        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Constants.connectStr = "";
            this.NavigationService.Navigate(new Uri("/WebForms/Login.xaml", UriKind.RelativeOrAbsolute));
        }

        private void mnuItemBA0_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("BA","BA All",sender, e));
        }

        private void mnuItemBA_Pending_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("BA", "Pending", sender, e));

        }

        private void mnuItemBA_Research_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("BA", "Research", sender, e));

        }

        private void mnuItemBA_Completed_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("BA", "Completed", sender, e));


        }

        private void mnuItemBA_Onhold_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("BA", "Onhold", sender, e));

        }

        private void mnuItemFR_All_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("FRF", "All", sender, e));
        }

        private void mnuItemFR_Pending_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("FRF", "Pending", sender, e));
        }

        private void mnuItemFR_Research_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("FRF", "Research", sender, e));

        }

        private void mnuItemFR_Completed_click(object sender, RoutedEventArgs e)
        {

            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("FRF", "Completed", sender, e));
        }

        private void mnuItemFR_Onhold_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("FRF", "Onhold", sender, e));
        }

        private void mnuItemMISC_All_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("Misc", "All", sender, e));
        }

        private void mnuItemMISC_Pending_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("Misc", "Pending", sender, e));
        }

        private void mnuItemMISC_Research_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("Misc", "Research", sender, e));
        }

        private void mnuItemMISC_Completed_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("Misc", "Completed", sender, e));
        }

        private void mnuItemMISC_Onhold_click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);
            mnuUnselect = (MenuItem)sender;
            this.frame2.NavigationService.Navigate(new WebForms.QualityAnalysis("Misc", "Onhold", sender, e));
        }




       
    }
}
