using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace IHS.WPF.UI.ProgressIndicator
{
    public class ProgressViewer
    {
        public static double topMargin = 0;
        public static double leftMargin = 0;
        public static Thread newWindowThread;
        public static ProgressIndicatorWindow progressIndicator;
        public static void startProgress(double IHSSkinnedWindowHeight, double IHSSkinnedWindowWidth)
        {
            try
            {
                double NewHeight = 0;
                double NewWidth = 0;
                if ((System.Configuration.ConfigurationManager.AppSettings["ApplicationType"] != null) &&
                (System.Configuration.ConfigurationManager.AppSettings["ApplicationType"].Equals("windows")))
                {
                    NewHeight = IHSSkinnedWindowHeight;
                    NewWidth = IHSSkinnedWindowWidth;
                }

                if ((NewHeight > 0) && (NewWidth > 0))
                {
                    leftMargin = System.Windows.Application.Current.MainWindow.Left;
                    leftMargin = leftMargin + (NewWidth / 2);
                    topMargin = System.Windows.Application.Current.MainWindow.Top;
                    topMargin = topMargin + (NewHeight / 2);
                }
                else
                {
                    leftMargin = 500;
                    //leftMargin = leftMargin + (NewWidth / 2);
                    topMargin = 400;
                   // topMargin = topMargin + (NewHeight / 2);
                }

                newWindowThread = new Thread(new ThreadStart(ThreadStartingPoint));
                newWindowThread.SetApartmentState(ApartmentState.STA);
                newWindowThread.IsBackground = true;
                newWindowThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ResultsViewer:startProgress(); Message: [" + ex.Message + "]");
                //System.Windows.MessageBox.Show("Error encountered: [" + ex.Message + "]", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
            }
        }
        public static void endProgress()
        {
            try
            {
                if (progressIndicator != null)
                {
                    newWindowThread.Abort();
                    newWindowThread = new Thread(new ThreadStart(ThreadStoppingPoint));
                    newWindowThread.SetApartmentState(ApartmentState.STA);
                    newWindowThread.IsBackground = true;
                    newWindowThread.Start();
                }
                // Comment out the call to Sleep() to improve performance. Doug 5/11/09
                System.Threading.Thread.Sleep(500);
                //System.Windows.Application.Current.MainWindow.BringIntoView();
               // System.Windows.Application.Current.MainWindow.Activate();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ResultsViewer:endProgress(); Message: [" + ex.Message + "]");
            }
            finally
            {
            }
        }
        public static void ThreadStartingPoint()
        {
            try
            {
                progressIndicator = new ProgressIndicatorWindow();
                progressIndicator.Left = leftMargin;
                progressIndicator.Top = topMargin;
                progressIndicator.Show();
                System.Windows.Threading.Dispatcher.ExitAllFrames();
                System.Windows.Threading.Dispatcher.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ResultsViewer:ThreadStaringPoint(); Message: [" + ex.Message + "]");
            }
            finally
            {
            }
        }
        public static void ThreadStoppingPoint()
        {
            try
            {
                System.Windows.Threading.Dispatcher.ExitAllFrames();
                System.Windows.Threading.Dispatcher.Run();
                progressIndicator.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in ResultsViewer:ThreadStoppingPoint(); Message: [" + ex.Message + "]");
            }
            finally
            {
            }

        }
    }
}
