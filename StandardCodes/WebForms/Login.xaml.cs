// Created on        Created By
// JULY 2010      Gayathri (16727)

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
using System.Data;
using StandardCodes.BusinessLayer;
using System.Data.OracleClient;
using System.Diagnostics;
using IHS.WPF.UI.ProgressIndicator;
using ProgressControls;

namespace StandardCodes
{
    /// <summary>
    /// Interaction logic for index.xaml
    /// </summary>
    public partial class Login : Page
    {

        MenuItem mnuUnselect = new MenuItem();
        public Login()
        {
            try
            {              

                InitializeComponent();
                this.Loaded += delegate { NavigationService.Navigating += MyNavHandler; };               


            }
            catch (Exception ex)
            {
            }

        }
        void MyNavHandler(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                e.Cancel = true;
            }
            if (e.NavigationMode == NavigationMode.Forward)
            {
                e.Cancel = true;
            }
        }
        private void btnSignin_Click(object sender, RoutedEventArgs e)
        {
            ProgressViewer.startProgress(100.0, 100.0);
            DBClass Dbobject = new DBClass();
            try
            {
               
                if (txtUserName.Text != string.Empty && txtPassWord.Password != string.Empty)
                {
                    Constants.IsOwner = false;
                    Constants.IsAdmin = false;
                    Constants.IsReadOnly = false;
                    Constants.AdminSchemaName = Constants.GetAdminSchemaName();

                    Constants.connectStr = Constants.defaultconnStr + "User Id=" + txtUserName.Text.ToUpper().ToString() + "; Password=" + txtPassWord.Password + ";";
                    ConnectionServices conService = new ConnectionServices();
                    clsCon conobj = new clsCon();

                    DataSet dsState = new DataSet();
                    StructureClass.StrucLogInData strData = new StructureClass.StrucLogInData();
                    strData.strUserID = txtUserName.Text.ToUpper().ToString();
                    strData.strUserPassword = txtPassWord.Password;

                    conobj = conService.GetconnectionObj();
                    Dbobject.Connectionstring = conobj.Connectionstring;
                    Dbobject.Connect();

                    dsState = (DataSet)Dbobject.SpUser_DataRetreive(ref strData, "User");
                    Dbobject.close();
                    if (dsState != null && dsState.Tables[0].Rows.Count > 0)
                    {
                        Constants.strUserEmail = dsState.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();
                        strData.strUserName = dsState.Tables[0].Rows[0][0].ToString();
                        Constants.User_id = strData.strUserID;
                        Constants.Userid = strData.strUserName;
                        int ik = 0;

                        if (dsState.Tables[0].Rows[0]["IS_CUSTOMERCARE"].ToString() == "1")
                        {
                            strData.strRole = "CUSTOMER CARE";
                            ik = 1;
                            Constants.IsCustomerCare = true;
                            Constants.varRequestOrder = 1;

                        }
                        if (dsState.Tables[0].Rows[0]["IS_CLIENT_SUPPORT"].ToString() == "1")
                        {
                            strData.strRole = "CLIENT SUPPORT";
                            ik = 1;
                            Constants.IsClientSupport = true;
                            Constants.varRequestOrder = 2;

                        }
                        else if (dsState.Tables[0].Rows[0]["IS_ADMIN"].ToString() == "1")
                        {
                            strData.strRole = "ADMIN";
                            ik = 1;
                            Constants.IsAdmin = true;
                            Constants.varRequestOrder = 3;

                        }
                        else if (dsState.Tables[0].Rows[0]["IS_SCOUT"].ToString() == "1" && ik != 1)
                        {

                            strData.strRole = strData.strRole + "Scout";
                            ik = 1;
                            Constants.varRequestOrder = 4;
                        }
                        else if (dsState.Tables[0].Rows[0]["IS_USER"].ToString() == "1" && ik != 1)
                        {

                            strData.strRole = strData.strRole + "User";
                            ik = 1;
                            Constants.varRequestOrder = 5;
                        }
                        else if (dsState.Tables[0].Rows[0]["IS_READ_ONLY"].ToString() == "1" && ik != 1)
                        {

                            strData.strRole = strData.strRole + "Read only";
                            ik = 1;
                            Constants.IsReadOnly = true;
                        }
                        else if (dsState.Tables[0].Rows[0]["IS_OWNER"].ToString() == "1")
                        {


                            Constants.IsOwner = true;
                        }

                        Constants.strLogin = strData;
                        //------------------- Load Objects ----------------------
                        if (Constants.DsBA_Service_Type == null)
                        {
                            Constants.DsBA_Service_Type = DBClass.LoadNamedAges();
                        }

                        if (Constants.DsBA_State == null)
                        {
                            Constants.DsBA_State = DBClass.LoadState();
                        }
                        if (Constants.DsFRF_Schema == null)
                        {
                            Constants.DsFRF_Schema = DBClass.LoadFRFDataSchema();
                        }
                        if (Constants.DsOwner == null)
                        {
                            Constants.DsOwner = DBClass.LoadOwner();
                        }


                        //-------------------  Load Objects Ended ----------------------

                        ProgressViewer.endProgress();
                        this.NavigationService.Navigate(new Uri("/WebForms/Index.xaml", UriKind.RelativeOrAbsolute));
                    }
                    else
                    {
                       
                        MessageBox.Show(" The username or password you entered is incorrect");
                        txtUserName.Focus();
                    }

                }
                else if (txtUserName.Text == string.Empty)
                {
                    ProgressViewer.endProgress();
                    MessageBox.Show("Enter your username");
                    txtUserName.Focus();
                }
                else if (txtPassWord.Password == string.Empty)
                {
                    ProgressViewer.endProgress();
                    MessageBox.Show("Enter your password");
                    txtPassWord.Focus();
                }
            }
            catch (OracleException ex)
            {
                ProgressViewer.endProgress();
                switch (ex.Code)
                {

                    case 1017:
                        MessageBox.Show("Invalid username/password; logon denied");
                        break;

                    case 6550:
                       MessageBox.Show("You do not have grants to access the database.");
                        break;

                    case 12170:
                        MessageBox.Show("Please check your network connectivity");
                        break;

                    case 12545:
                         MessageBox.Show("Please check your network connectivity or you might not have oracle client installed.If you do not have oracle client installed click on the link 'Install Oracle Client' provided below.");
                        break;

                    default:
                        MessageBox.Show("Please check your network connectivity or you might not have oracle client installed.If you do not have oracle client installed click on the link 'Install Oracle Client' provided below.");
                        break;
                }
                
              

              
              
             

              
            }
            catch (Exception ex2)
            {
               // ProgressViewer.endProgress();
                MessageBox.Show("There is some problem logging In." + ex2.ToString());
            }
            finally
            {
                
                Dbobject.close();
                ProgressViewer.endProgress();
            }
        }

      

        private void btn_oracleclient_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Interop.BrowserInteropHelper.IsBrowserHosted)
            {
                var serverURL = System.Windows.Interop.BrowserInteropHelper.Source.ToString();
                var x = serverURL.Substring(0, serverURL.LastIndexOf("/"));
                Process.Start(x + "/OracleXEClient.exe");
            }
        }

    }
}
