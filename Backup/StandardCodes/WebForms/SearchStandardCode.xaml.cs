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
using System.Collections;
using System.ComponentModel;
using System.Collections.Specialized;
using Microsoft.Windows.Controls;
using System.Diagnostics;
using Microsoft.Windows.Controls.Primitives;
using StandardCodes.BusinessLayer;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Windows.Controls.Primitives;
using IHS.WPF.UI.ProgressIndicator;

namespace StandardCodes.WebForms
{
    /// <summary>
    /// Interaction logic for SearchStandardCode.xaml
    /// </summary>
    public partial class SearchStandardCode : Page
    {
         Thickness  mgBA; 
        Thickness mgFRF;
        Thickness mgMISC;
        Thickness mglblBA;
        Thickness mglblFRF;
        Thickness mglblMISC;


        Thickness defmgBA;
        Thickness defmgFRF;
        Thickness defmgMISC;
        Thickness defmglblBA;
        Thickness defmglblFRF;
        Thickness defmglblMISC;

        private string ba_id;

        private string frf_id;
        private string misc_id;

        //private GroupStyle _defaultGroupStyle;
        //private IEditableCollectionView iecv;
        //private bool _initialColumnSetup = false;
        //private ControlTemplate _newRowControlTemplate;
        //private ControlTemplate _defaultRowControlTemplate;

       // private string ba_id;

     //   private string frf_id;
       // private string misc_id;
        MenuItem mnuUnselect = new MenuItem();

        private CollectionViewSource CountryCollection;
        public DataTable Countrys { get; private set; }
        public DataTable TempCountrys { get; private set; }

        private CollectionViewSource DistrictCollection;
        public DataTable District { get; private set; }

        private CollectionViewSource StateCollection;
        public DataTable State { get; private set; }
        private CollectionViewSource StateCollectionField;
        public DataTable StateField { get; private set; }

        private CollectionViewSource RegionCollection;
        public DataTable Region { get; private set; }

        private CollectionViewSource ownerCollection;
        public DataTable owner { get; private set; }
        private readonly Regex numberEx = new Regex(@"^[0-9]+$");
        private readonly Regex floatEx = new Regex(@"^[0-9.]+$");
        private static string strBAStatus = "";
        private static string strFRFStatus = "";
        private static string strMISCStatus = "";
        private static int intFirst = 0;
        public int BArowindex;
        public int FRFrowindex;
        public int Miscrowindex;

        private CollectionViewSource SCodeCollection;

        public DataTable SCode { get; private set; }
           
        public SearchStandardCode()
        {
            InitializeComponent();
           
            //Margin mgBA;
            //Margin mgFRF;
            //Margin mgMisc;
            ProgressViewer.startProgress(100.0, 100.0);

            mgBA = DataGrid_BA.Margin;
            mgFRF = DataGrid_FRF.Margin;
            mgMISC = DataGrid_MISC.Margin;
            mglblBA = lblBA.Margin;
            mglblFRF = lblFRF.Margin;
            mglblMISC = lblMisc.Margin;

            defmgBA = DataGrid_BA.Margin;
            defmgFRF = DataGrid_FRF.Margin;
            defmgMISC = DataGrid_MISC.Margin;
            defmglblBA = lblBA.Margin;
            defmglblFRF = lblFRF.Margin;
            defmglblMISC = lblMisc.Margin;
            ownerCollection = (CollectionViewSource)this.FindResource("OwnerCollection");
            owner = LoadOwner();
            ownerCollection.Source = ((IListSource)owner).GetList();

            DataSet dsFRF = new DataSet();
            dsFRF = LoadData();
            // Binding Country List to the Country Dropdownlist
            CountryCollection = (CollectionViewSource)this.FindResource("CountryCollection");
            Countrys = AddSelectItem(dsFRF.Tables[0]);
            TempCountrys = dsFRF.Tables[0];
            CountryCollection.Source = ((IListSource)Countrys).GetList();

            // Binding District List to the District Dropdownlist
            DistrictCollection = (CollectionViewSource)this.FindResource("DistrictCollection");
            District = AddSelectItem(dsFRF.Tables[3]);
            DistrictCollection.Source = ((IListSource)District).GetList();
            // Binding State List to the State Dropdownlist
            StateCollection = (CollectionViewSource)this.FindResource("StateCollection");
            State = AddSelectItem(dsFRF.Tables[1]);
            StateCollection.Source = ((IListSource)State).GetList();

            StateCollectionField = (CollectionViewSource)this.FindResource("StateCollectionField");
            StateField = AddSelectItem(dsFRF.Tables[1]);
            StateCollectionField.Source = ((IListSource)StateField).GetList();
            // Binding Region List to the Region Dropdownlist
            RegionCollection = (CollectionViewSource)this.FindResource("RegionCollection");
            Region = AddSelectItem(dsFRF.Tables[2]);
            RegionCollection.Source = ((IListSource)Region).GetList();


            SCodeCollection = (CollectionViewSource)this.FindResource("SCodeCollection");
            SCode = LoadStandardCodes();
            SCodeCollection.Source = ((IListSource)SCode).GetList();
        
            ProgressViewer.endProgress();
            
        }

        /// <summary>
        /// Hook up to the Loaded event.
        /// </summary>
        /// <param name="sender">The source object.</param>
        /// <param name="e">The event data.</param>
       
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AutoCompleteBox acb = (AutoCompleteBox)sender;

            // In these sample scenarios, the Tag is the name of the content 
            // presenter to use to display the value.
            string contentPresenterName = (string)acb.Tag;
            ContentPresenter cp = FindName(contentPresenterName) as ContentPresenter;
            if (cp != null)
            {
                cp.Content = acb.SelectedItem;
            }
        }

        private static DataTable LoadStandardCodes()
        {
            DataSet dsState = new DataSet();
            DBClass Dbobject = new DBClass();

            try
            {

                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                StructureClass.StrucQAData strData = new StructureClass.StrucQAData();

                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strData.Querytype = 1;
                strData.strtable = "BA";


                Dbobject.Connect();
                //dsState = (DataSet)Dbobject.SpQA (ref strData, 2, "Business_Associate");
                dsState = (DataSet)Dbobject.ExecutQuery("Select STANDARDCODE from TEMP_BA");
                Dbobject.close();


            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    MessageBox.Show("Please check your network connectivity");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem populating the State.");

            }
            finally
            {
                Dbobject.close();
            }
            return dsState.Tables[0];
        }


        #region AddSelectItem Method
        // This method used for adding empty value to the combo box 
        private DataTable AddSelectItem(DataTable dtTable)
        {
            DataTable dtTemp = new DataTable();
            dtTemp = dtTable;
            DataRow dr = dtTemp.NewRow();
            dr[0] = " ";
            dtTemp.Rows.InsertAt(dr, 0);
            return dtTemp;
        }
        #endregion 

        #region LoadOwner Method

        public static DataTable LoadOwner()
        {
            DBClass Dbobject = new DBClass();
            DataSet dsOwner = new DataSet();
            try
            {
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                StructureClass.StrucLogInData strData = new StructureClass.StrucLogInData();
                strData.QueryType = 1;
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                Dbobject.Connect();
                dsOwner = (DataSet)Dbobject.SpUser_DataRetreive(ref strData, "user");
                Dbobject.close();


            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    MessageBox.Show("Please check your network connectivity");
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                Dbobject.close();
            }
            return dsOwner.Tables[0];

        }
        #endregion

        #region LoadData Method
        private static DataSet LoadData()
        {
            DataSet dsState = new DataSet();
            DBClass Dbobject = new DBClass();
            try
            {
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();

                StructureClass.StrucFRF strData = new StructureClass.StrucFRF();

                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                Dbobject.Connect();
                dsState = (DataSet)Dbobject.SpFRF_DataRetreive(ref strData, "FieldReservoirFormation");
                Dbobject.close();
                return dsState;
            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    MessageBox.Show("Please check your network connectivity");
                }
                return dsState;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return dsState;
            }
            finally
            {
                Dbobject.close();
            }
        }
        #endregion

        //Viewing BA Record
        private void ViewBAButton_Click(object sender, RoutedEventArgs e)
        {

            string var_txtid = ((System.Data.DataRowView)(DataGrid_BA.SelectedItem)).Row.ItemArray[0].ToString();
            PopupBA.Placement = PlacementMode.Center;
            PopupBA.Width = 740;
            PopupBA.IsOpen = true;
            Service_Binding();
            BindStateCode();
            BindBAData(var_txtid);
        }

        #region BA Data Binding Event
        //Binding Business Associate Data 
        public void BindBAData(string baid)
        {
            DBClass Dbobject = new DBClass();
            try
            {
                DataSet DsBA = new DataSet();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();

                StructureClass.StrucBAInsert strData = new StructureClass.StrucBAInsert();
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                Dbobject.Connect();
                strData.Querytype = 16;
                strData.strBAID = baid;
                ba_id = baid;
                DsBA = (DataSet)Dbobject.SpBusinessAssociate(ref strData, 2, "QA");
                Dbobject.close();

                if (DsBA.Tables[0].Rows.Count > 0)
                {

                    this.txtMnemonic.Text = DsBA.Tables[0].Rows[0]["BA_CODE"].ToString();
                    this.txtState2.Text = DsBA.Tables[0].Rows[0]["BA_ID_ALIAS"].ToString();
                    this.txtLastName.Text = DsBA.Tables[0].Rows[0]["LAST_NAME"].ToString();
                    this.txtFirstName.Text = DsBA.Tables[0].Rows[0]["FIRST_NAME"].ToString();
                    this.txtBAMiddleName.Text = DsBA.Tables[0].Rows[0]["MIDDLE_INITIAL"].ToString();
                    this.cboServiceType.SelectedValue = DsBA.Tables[0].Rows[0]["BA_SERVICE_TYPE"].ToString();
                    this.txtBAShortName.Text = DsBA.Tables[0].Rows[0]["BA_SHORT_NAME"].ToString();
                    this.txtStateCode.Text = DsBA.Tables[0].Rows[0]["PROVINCE_STATE_CODE"].ToString();
                    this.txtUserId_BA.Text = DsBA.Tables[0].Rows[0]["PI_USER_ID"].ToString();
                    this.txtCity.Text = DsBA.Tables[0].Rows[0]["CITY"].ToString();
                    this.txtFax.Text = DsBA.Tables[0].Rows[0]["FAX_NUM"].ToString();
                    this.txtAdress1.Text = DsBA.Tables[0].Rows[0]["FIRST_ADDRESS_LINE"].ToString();
                    this.txtPhone.Text = DsBA.Tables[0].Rows[0]["PHONE_NUM"].ToString();
                    this.txtZip.Text = DsBA.Tables[0].Rows[0]["POSTAL_ZIP_CODE"].ToString();
                    this.cbostate.SelectedValue = DsBA.Tables[0].Rows[0]["PROVINCE_STATE"].ToString();
                    this.txtComments.Text = DsBA.Tables[0].Rows[0]["REMARKS"].ToString();
                    this.txtAddress2.Text = DsBA.Tables[0].Rows[0]["SECND_ADDRESS_LINE"].ToString();
                    this.txtStandardCode.Text = DsBA.Tables[0].Rows[0]["STANDARDCODE"].ToString();
                }

            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    MessageBox.Show("Please check your network connectivity");
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                Dbobject.close();
            }
        }
        #endregion
        #region FRFclose click Event
        // FRF Closing Event
        private void popFRFclose_click(object sender, System.Windows.RoutedEventArgs e)
        {
            PopupFRF.IsOpen = false;
           // BindPopupData(1, "FRF", "", DataGrid_FRF);
        }
        #endregion
        #region MISCclose click Event
        // MISC Closing Event
        private void popMISCclose_click(object sender, System.Windows.RoutedEventArgs e)
        {
            PopupMISC.IsOpen = false;
           // BindPopupData(1, "MISC", "", DataGrid_MISC);
        }
        #endregion

        #region BAclose click Event
        // BA Closing Event
        private void popBAclose_click(object sender, System.Windows.RoutedEventArgs e)
        {
            PopupBA.IsOpen = false;
            //BindPopupData(1, "BA", "", DataGrid_BA);
        }
        #endregion

        #region BA ServiceType ComboBox Data Binding Event
        // BA_SERVICE_TYPE Data Retreving from BackendTable
        void Service_Binding()
        {
            DBClass Dbobject = new DBClass();
            try
            {
                DataSet dsState = new DataSet();

                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();

                StructureClass.StrucBAInsert strData = new StructureClass.StrucBAInsert();

                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                Dbobject.Connect();

                strData.Querytype = 11;
                dsState = (DataSet)Dbobject.SpBusinessAssociate(ref strData, 2, "Business_Associate");

                Dbobject.close();
                DataTable dtTemp = new DataTable();
                dtTemp = dsState.Tables[0];
                DataRow dr = dtTemp.NewRow();
                dr[1] = " ";
                dtTemp.Rows.InsertAt(dr, 0);

                cboServiceType.ItemsSource = dtTemp.DefaultView;

                cboServiceType.DisplayMemberPath = dsState.Tables[0].Columns["BA_SERVICE_TYPE"].ToString();
                cboServiceType.SelectedValuePath = dsState.Tables[0].Columns["BA_SERVICE_TYPE"].ToString();
                cboServiceType.Items.Add("Select Service Type");
                cboServiceType.SelectedValue = "Select Service Type";
                //cboServiceType.data
            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    MessageBox.Show("Please check your network connectivity");
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                Dbobject.close();
            }
        }
        #endregion

        void BindStateCode()
        {
            DBClass Dbobject = new DBClass();
            try
            {

                DataSet dsState = new DataSet();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                StructureClass.StrucBAInsert strData = new StructureClass.StrucBAInsert();
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                Dbobject.Connect();
                strData.Querytype = 12;
                dsState = (DataSet)Dbobject.SpBusinessAssociate(ref strData, 2, "Business_Associate");
                Dbobject.close();
                cbostate.DisplayMemberPath = dsState.Tables[0].Columns["my_column"].ToString();
                cbostate.SelectedValuePath = dsState.Tables[0].Columns["province_state"].ToString();
                cbostate.ItemsSource = dsState.Tables[0].DefaultView;
                cbostate.SelectedIndex = -1;

            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    MessageBox.Show("Please check your network connectivity");
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                Dbobject.close();
            }
        }

        private void btn_searchByDate_Click(object sender, RoutedEventArgs e)
        {
            Boolean status = false;
            mgBA = defmgBA;
            mgFRF = defmgFRF;
            mgMISC = defmgMISC;
            mglblBA = defmglblBA;
            mglblFRF = defmglblFRF;
            mglblMISC = defmglblMISC;


              DataGrid_BA.Margin=defmgBA;
              DataGrid_FRF.Margin=defmgFRF;
              DataGrid_MISC.Margin=defmgMISC;
              lblBA.Margin=defmglblBA;
              lblFRF.Margin=defmglblFRF;
              lblMisc.Margin=defmglblMISC;

              lblBA.Visibility = Visibility.Visible;
              lblFRF.Visibility = Visibility.Visible;
              lblMisc.Visibility = Visibility.Visible;

            status = BindData(1, "SEARCH_BY_DATE", DataGrid_BA);

            //if(status == false)
            //{
            status = BindData(2, "SEARCH_BY_DATE", DataGrid_FRF);
            //DataGrid_FRF.Visibility = Visibility.Visible ;
            //DataGrid_MISC.Visibility = Visibility.Hidden;
            //DataGrid_BA.Visibility = Visibility.Hidden ;
            //  }
            //if (status == false)
            //{
            status = BindData(3, "SEARCH_BY_DATE", DataGrid_MISC);
            //DataGrid_FRF.Visibility = Visibility.Hidden;
            //DataGrid_MISC.Visibility = Visibility.Visible ;
            //DataGrid_BA.Visibility = Visibility.Hidden ;
            //   }

           
        }
        private void ViewFRFButton_Click(object sender, RoutedEventArgs e)
        {
            string var_txtid = ((System.Data.DataRowView)(DataGrid_FRF.SelectedItem)).Row.ItemArray[0].ToString();
            PopupFRF.Placement = PlacementMode.Center;
            PopupFRF.Width = 740;
            PopupFRF.IsOpen = true;
            BindFRFData(var_txtid);
        }
        private void ViewMISCButton_Click(object sender, RoutedEventArgs e)
        {
            string var_txtid = ((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[16].ToString();
            PopupMISC.Placement = PlacementMode.Center;
            PopupMISC.Width = 740;
            PopupMISC.IsOpen = true;
            BindMISCData(var_txtid);
        }

        #region MISC Data Binding Event
        //Binding MISC Data 
        public void BindMISCData(string miscid)
        {
            DBClass Dbobject = new DBClass();
            try
            {
                DataSet DsMisc = new DataSet();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                StructureClass.StrucMISCInsert strData = new StructureClass.StrucMISCInsert();
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                Dbobject.Connect();
                strData.intQuerytype = 6;
                strData.strMISC_KEY = int.Parse(miscid);
                misc_id = miscid;
                DsMisc = (DataSet)Dbobject.SpMISC(ref strData, 2, "MISC");
                Dbobject.close();
                if (DsMisc.Tables[0].Rows.Count > 0)
                {
                    txtReqDiscription.Text = DsMisc.Tables[0].Rows[0]["REQUEST_DESCRIPTION"].ToString();
                    txtMiscID.Text = DsMisc.Tables[0].Rows[0]["BA_ID"].ToString();
                    txtUserName.Text = DsMisc.Tables[0].Rows[0]["USER_NAME"].ToString();
                    txtRemarks.Text = DsMisc.Tables[0].Rows[0]["REMARKS"].ToString();
                    txtTexasSurveyNumber.Text = DsMisc.Tables[0].Rows[0]["TEXASSURVEY_NUMBER"].ToString();
                    txtLongName.Text = DsMisc.Tables[0].Rows[0]["TEXASSURVEY_LONGNAME"].ToString();
                    txtTeaxsRemarks.Text = DsMisc.Tables[0].Rows[0]["TEXASSURVEY_REMARKS"].ToString();
                    txtMonumentID.Text = DsMisc.Tables[0].Rows[0]["MONUMENT_ID"].ToString();
                    txtMonumentLatitude.Text = DsMisc.Tables[0].Rows[0]["MONUMENT_LATITUDE"].ToString();
                    txtMonumentLongitude.Text = DsMisc.Tables[0].Rows[0]["MONUMENT_LONGITUDE"].ToString();
                    txtMonumentName.Text = DsMisc.Tables[0].Rows[0]["MONUMENT_NAME"].ToString();
                    txtMonumentRemarks.Text = DsMisc.Tables[0].Rows[0]["MONUMENT_REMARKS"].ToString();

                }

            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    MessageBox.Show("Please check your network connectivity");
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                Dbobject.close();
            }
        }
        #endregion


        #region FRF Data Binding Event
        //Binding FRF Data 
        public void BindFRFData(string frfid)
        {
            DBClass Dbobject = new DBClass();
            try
            {
                DataSet dsFRF = new DataSet();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                StructureClass.StrucFRF strData = new StructureClass.StrucFRF();
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                Dbobject.Connect();
                strData.strQuerytype = 10;
                strData.strFLD_KEY = frfid;
                frf_id = frfid;
                dsFRF = (DataSet)Dbobject.SpFRF_NEW(ref strData, 2, "FRF");
                Dbobject.close();
                if (dsFRF.Tables[0].Rows.Count > 0)
                {
                    //field
                    //---
                    frfcmbDistrict.IsEnabled = true;
                    frfFieldName.Text = dsFRF.Tables[0].Rows[0]["FIELD_NAME"].ToString();
                    try
                    {
                        if (!string.IsNullOrEmpty(dsFRF.Tables[0].Rows[0]["PROVINCE_STATE_FCXS"].ToString()))
                        {
                            frfcmbState.SelectedValue = dsFRF.Tables[0].Rows[0]["PROVINCE_STATE_FCXS"].ToString();
                        }
                        else
                        {
                            frfcmbState.SelectedIndex = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        frfcmbState.SelectedIndex = -1;
                    }

                    try
                    {
                        if (frfcmbState.SelectedIndex != -1 && frfcmbState.Text != " ")
                        {
                            DataView dr = (DataView)(TempCountrys.DefaultView);
                            dr.RowFilter = "province_state =" + frfcmbState.SelectedValue.ToString();
                            Countrys = dr.Table.DefaultView.Table;
                            CountryCollection.Source = ((IListSource)Countrys).GetList();

                            if (!string.IsNullOrEmpty(dsFRF.Tables[0].Rows[0]["COUNTY"].ToString()))
                            {
                                frfcmbCountry.SelectedValue = dsFRF.Tables[0].Rows[0]["COUNTY"].ToString();
                            }
                            else
                            {
                                frfcmbCountry.SelectedIndex = -1;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        frfcmbCountry.SelectedIndex = -1;
                    }
                    try
                    {
                        if (frfcmbState.SelectedValue.ToString().Equals("42"))
                        {
                            if (!string.IsNullOrEmpty(dsFRF.Tables[0].Rows[0]["DISTRICT"].ToString()))
                            {
                                frfcmbDistrict.SelectedValue = dsFRF.Tables[0].Rows[0]["DISTRICT"].ToString();
                            }
                            else
                            {
                                frfcmbDistrict.SelectedIndex = -1;
                            }
                            frfcmbDistrict.IsEnabled = true;
                        }
                        else
                        {
                            frfcmbDistrict.SelectedIndex = -1;
                            frfcmbDistrict.IsEnabled = false;

                        }
                    }
                    catch (Exception ex)
                    {
                        frfcmbDistrict.IsEnabled = false;
                        frfcmbDistrict.SelectedIndex = -1;
                    }
                    frfMMS.Text = dsFRF.Tables[0].Rows[0]["FIELD_ALIAS_FA"].ToString();
                    frfcmbSource.Text = dsFRF.Tables[0].Rows[0]["SOURCE_MMS_FA"].ToString();
                    frfGrad.Text = dsFRF.Tables[0].Rows[0]["PI_TEMP_GRADIENT"].ToString();
                    frfStateCode.Text = dsFRF.Tables[0].Rows[0]["FIELD_ALIAS_FCXS"].ToString();
                    frfAltState.Text = dsFRF.Tables[0].Rows[0]["PROVINCE_STATE_FIELD"].ToString();
                    frfStandardCode.Text = dsFRF.Tables[0].Rows[0]["FIELD_ID"].ToString();
                    frfusername.Text = dsFRF.Tables[0].Rows[0]["PI_USER_ID"].ToString();
                    frfActive.IsChecked = true;

                    frfComments.Text = dsFRF.Tables[0].Rows[0]["REMARK_FIELD"].ToString();

                    //Reservoir
                    //------------
                    frfPoolName.Text = dsFRF.Tables[0].Rows[0]["POOL_NAME"].ToString();
                    frfRSVR.Text = dsFRF.Tables[0].Rows[0]["POOL_ALIAS_PCXS"].ToString();
                    try
                    {
                        if (!string.IsNullOrEmpty(dsFRF.Tables[0].Rows[0]["PROVINCE_STATE_PCXS"].ToString()))
                        {
                            frfcmbReservState.SelectedValue = dsFRF.Tables[0].Rows[0]["PROVINCE_STATE_PCXS"].ToString();
                        }
                        else
                        {
                            frfcmbReservState.SelectedIndex = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        frfcmbReservState.SelectedIndex = -1;
                    }
                    frfReservStandardCode.Text = dsFRF.Tables[0].Rows[0]["POOL_ID_R"].ToString();
                    frfReservoirActive.IsChecked = true;

                    //Pool
                    //-----
                    frfPoolId.Text = dsFRF.Tables[0].Rows[0]["POOL_ID"].ToString();
                    frfPoolFieldsID.Text = dsFRF.Tables[0].Rows[0]["FIELD_ID_P"].ToString();
                    frfPoolState.Text = dsFRF.Tables[0].Rows[0]["PROVINCE_STATE_POOL"].ToString();
                    frfPoolFormid.Text = dsFRF.Tables[0].Rows[0]["FORM_ID"].ToString();
                    frfPoolActive.IsChecked = true;
                    frfPoolComments.Text = dsFRF.Tables[0].Rows[0]["REMARKS_POOL"].ToString();

                    //Formation
                    //------
                    frfFormationName.Text = dsFRF.Tables[0].Rows[0]["FORM_NAME"].ToString();
                    frfFormationApi.Text = dsFRF.Tables[0].Rows[0]["API"].ToString();
                    try
                    {
                        if (dsFRF.Tables[0].Rows[0]["FORM_ALIAS"].ToString() != string.Empty && dsFRF.Tables[0].Rows[0]["FORM_ALIAS"].ToString() != null && dsFRF.Tables[0].Rows[0]["FORM_ALIAS"].ToString() != "")
                        {
                            frfcmbFormationRegion.SelectedValue = dsFRF.Tables[0].Rows[0]["FORM_ALIAS"].ToString();
                        }
                        else
                        {
                            frfcmbFormationRegion.SelectedIndex = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        frfcmbFormationRegion.SelectedIndex = -1;
                    }
                    frfFormationbasin.Text = dsFRF.Tables[0].Rows[0]["GEOLOGIC_PROVINCE"].ToString();
                    frfFormationTop.Text = dsFRF.Tables[0].Rows[0]["PERFTOP"].ToString();
                    frfFormationBottom.Text = dsFRF.Tables[0].Rows[0]["PERFBOTTOM"].ToString();

                    frfFormationTD.Text = dsFRF.Tables[0].Rows[0]["PI_MD"].ToString();
                    frfFormationGeoAge.Text = dsFRF.Tables[0].Rows[0]["FORM_AGE"].ToString();
                    frfFormationActive.IsChecked = true;
                    frfFormationComments.Text = dsFRF.Tables[0].Rows[0]["REMARK"].ToString();
                }

            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    MessageBox.Show("Please check your network connectivity");
                }

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            finally
            {
                intFirst = 1;
                Dbobject.close();
            }
        }
        #endregion

          public void BindPopupData(int varQuery, string varTableName, string varRequestType, DataGrid dgGrid)
        {
            DBClass Dbobject = new DBClass();
            try
            {
                dgGrid.ItemsSource = null;
                DataSet DsBA = new DataSet();

                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                
                StructureClass.StrucQAData strData = new StructureClass.StrucQAData();

             
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                Dbobject.Connect();

                strData.Querytype = varQuery;
                strData.strtable = varTableName;
                strData.strRequest_Type = varRequestType;


                DsBA = (DataSet)Dbobject.SpQA(ref strData, 2, "QA");


                Dbobject.close();

                dgGrid.ItemsSource = DsBA.Tables[0].DefaultView;
                
                
                dgGrid.HorizontalAlignment = HorizontalAlignment.Left;
               
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is some problem populating data");
            }
            finally
            {
                Dbobject.close();
            }
        }
       

        public Boolean  BindData(int intType, string strtable,DataGrid da)
        {
            DBClass Dbobject = new DBClass();
            DataSet dsState = new DataSet();
            try
            {
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                StructureClass.StrucQAData  strData = new StructureClass.StrucQAData();
              

                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strData.strtable = strtable;
                
                    strData.Querytype = intType;
                    strData.VAR_STANDARDCODE = TxtScode.Text;
                    strData.VAR_FIELD_ID = TxtScode.Text;
                    strData.strBAID = TxtScode.Text;
                    if (txtDate.Text.Length > 0)
                    {
                        string mon;
                        mon = Convert.ToDateTime(txtDate.Text).Date.Month.ToString();
                        if (mon.Length == 1)
                        {
                            mon = "0" + mon;
                        }

                        string day;
                        day = Convert.ToDateTime(txtDate.Text).Date.Day.ToString();
                        if (day.Length == 1)
                        {
                            day = "0" + day;
                        }
                        strData.var_DATE_ENTERED = mon + "/" + day + "/" + Convert.ToDateTime(txtDate.Text).Year;
                            //Convert.ToDateTime(txtDate.Text).Date.ToString();
                    }


                Dbobject.Connect();
                dsState = (DataSet)Dbobject.SpQA (ref strData, 2, "Business_Associate");
                Dbobject.close();
                if (dsState.Tables[0].Rows.Count > 0)
                {
                    da.Visibility = Visibility.Visible;
                    da.ItemsSource = dsState.Tables[0].DefaultView;

                }
                else
                {
                    da.Visibility = Visibility.Hidden;
                    UpdateCurrentLayoutLayout(da);
                    UpdateLayout();
                    //da.UpdateLayout();
                    //DataGrid_MISC.Margin = DataGrid_FRF.Margin;
                }
               


            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    MessageBox.Show("Please check your network connectivity");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occured while binding the data.");
            }
            finally
            {
                Dbobject.close();
            }

            if (dsState.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        public void UpdateCurrentLayoutLayout(DataGrid dg)
        {
            if (dg.Name == "DataGrid_BA")
            {
                lblBA.Visibility = Visibility.Hidden;
                lblFRF.Margin = mglblBA;
                lblFRF.Visibility = Visibility.Visible;
                lblMisc.Margin = mglblFRF;
                lblMisc.Visibility = Visibility.Visible;

                mglblFRF = mglblBA;
                mgFRF = mgBA;

                DataGrid_FRF.Margin = mgBA;
                DataGrid_MISC.Margin = mgFRF;
            }

            if (dg.Name == "DataGrid_FRF")
            {

                lblMisc.Margin = mglblFRF;
                DataGrid_MISC.Margin = mgFRF;
                lblFRF.Visibility = Visibility.Hidden;


               
            }

            if (dg.Name == "DataGrid_MISC")
            {
                lblMisc.Visibility = Visibility.Hidden;
               

            }


        }

       

        private void btnsearch_Click(object sender, RoutedEventArgs e)
        {
            Boolean status=false ;

            mgBA = defmgBA;
            mgFRF = defmgFRF;
            mgMISC = defmgMISC;
            mglblBA = defmglblBA;
            mglblFRF = defmglblFRF;
            mglblMISC = defmglblMISC;

            DataGrid_BA.Margin = defmgBA;
            DataGrid_FRF.Margin = defmgFRF;
            DataGrid_MISC.Margin = defmgMISC;
            lblBA.Margin = defmglblBA;
            lblFRF.Margin = defmglblFRF;
            lblMisc.Margin = defmglblMISC;


            lblBA.Visibility = Visibility.Visible ;
            lblFRF.Visibility = Visibility.Visible;
            lblMisc.Visibility = Visibility.Visible;

            //DataGrid_FRF.Visibility = Visibility.Hidden;
            //DataGrid_MISC.Visibility = Visibility.Hidden;
            //DataGrid_BA.Visibility = Visibility.Visible;
            status = BindData(1, "SEARCH", DataGrid_BA);

            //if(status == false)
            //{
                status = BindData(2, "SEARCH", DataGrid_FRF);
                //DataGrid_FRF.Visibility = Visibility.Visible ;
                //DataGrid_MISC.Visibility = Visibility.Hidden;
                //DataGrid_BA.Visibility = Visibility.Hidden ;
          //  }
            //if (status == false)
            //{
                status = BindData(3, "SEARCH", DataGrid_MISC);
                //DataGrid_FRF.Visibility = Visibility.Hidden;
                //DataGrid_MISC.Visibility = Visibility.Visible ;
                //DataGrid_BA.Visibility = Visibility.Hidden ;
         //   }



        }
    }
}
