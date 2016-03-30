using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Windows.Controls.Primitives;
using StandardCodes.BusinessLayer;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Text.RegularExpressions;
using IHS.WPF.UI.ProgressIndicator;

namespace StandardCodes.WebForms
{
    /// Interaction logic for QualityAnalysis.xaml

    //****************************************************************************************************************************
    //Module Name:  QualityAnalysis
    //Created By:   Gayathri 
    //Created Date:
    //Modified By: Raja Sekhar
    //Date:        05/Oct/10
    //Modified By: Pavan Battu
    //Date:        09/sep/10
    //****************************************************************************************************************************

    public partial class QualityAnalysis : Page
    {
        #region Private Data

        private ControlTemplate _newRowControlTemplate;
        private ControlTemplate _defaultRowControlTemplate;
        private string ba_id;
        private string frf_id;
        private string misc_id;
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

        private CollectionViewSource BasinCollection;
        public DataTable Basin { get; private set; }

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
        #endregion Private Data

        #region QualityAnalysis
        public QualityAnalysis()
        {
            try
            {
                InitializeComponent();

                ProgressViewer.startProgress(100.0, 100.0);

              
                DataGrid_BA.Width = Constants.QAGridWidth;
                DataGrid_BA.Height = Constants.QAGridHeight;

                DataGrid_FRF.Width = Constants.QAGridWidth;
                DataGrid_FRF.Height = Constants.QAGridHeight;

                DataGrid_MISC.Width = Constants.QAGridWidth;
                DataGrid_MISC.Height = Constants.QAGridHeight;

               
                DataGrid_FRF.Visibility = Visibility.Hidden;
                DataGrid_MISC.Visibility = Visibility.Hidden;
                DataGrid_BA.Visibility = Visibility.Visible;



                lblname.Text = "BUSINESS ASSOCIATE - ALL REQUESTS";

                // Binding the Business Associate data to the Gridview

                BindData(1, "BA", "", DataGrid_BA);
              

                // Binding Owner List to the Owner Dropdownlist
                ownerCollection = (CollectionViewSource)this.FindResource("OwnerCollection");
                if (Constants.DsOwner == null)
                {
                    Constants.DsOwner = DBClass.LoadOwner();
                }
                owner = Constants.DsOwner;
                ownerCollection.Source = ((IListSource)owner).GetList();              

                if (Constants.IsOwner == true || Constants.IsAdmin == true)
                {

                    btnSubmit.Visibility = Visibility.Visible;
                    btnMISCSubmit.Visibility = Visibility.Visible;
                    btnFRFsave.Visibility = Visibility.Visible;

                    DataGrid_BA.Columns[9].Visibility = Visibility.Visible;
                    DataGrid_FRF.Columns[9].Visibility = Visibility.Visible;
                    DataGrid_MISC.Columns[9].Visibility = Visibility.Visible;

                }
                else
                {

                    btnSubmit.Visibility = Visibility.Hidden;
                    btnMISCSubmit.Visibility = Visibility.Hidden;
                    btnFRFsave.Visibility = Visibility.Hidden;
                    DataGrid_BA.Columns[9].Visibility = Visibility.Hidden;
                    DataGrid_FRF.Columns[9].Visibility = Visibility.Hidden;
                    DataGrid_MISC.Columns[9].Visibility = Visibility.Hidden;

                }
           
                ProgressViewer.endProgress();
            }
            catch (Exception ex)
            {
                ProgressViewer.endProgress();
            }

        }
       
        public QualityAnalysis(String strGrid,string strMenu, Object sender, RoutedEventArgs en)
        {
            try
            {
                InitializeComponent();
                
                ProgressViewer.startProgress(100.0, 100.0); 
              
                DataGrid_FRF.Visibility = Visibility.Hidden;
                DataGrid_MISC.Visibility = Visibility.Hidden;
                DataGrid_BA.Visibility = Visibility.Visible;

                ownerCollection = (CollectionViewSource)this.FindResource("OwnerCollection");
                if (Constants.DsOwner == null)
                {
                    Constants.DsOwner = DBClass.LoadOwner();
                }
                owner = Constants.DsOwner;
                ownerCollection.Source = ((IListSource)owner).GetList();              

                if (Constants.IsOwner == true || Constants.IsAdmin == true)
                {

                    btnSubmit.Visibility = Visibility.Visible;
                    btnMISCSubmit.Visibility = Visibility.Visible;
                    btnFRFsave.Visibility = Visibility.Visible;

                    DataGrid_BA.Columns[9].Visibility = Visibility.Visible;
                    DataGrid_FRF.Columns[9].Visibility = Visibility.Visible;
                    DataGrid_MISC.Columns[9].Visibility = Visibility.Visible;

                }
                else
                {

                    btnSubmit.Visibility = Visibility.Hidden;
                    btnMISCSubmit.Visibility = Visibility.Hidden;
                    btnFRFsave.Visibility = Visibility.Hidden;
                    DataGrid_BA.Columns[9].Visibility = Visibility.Hidden;
                    DataGrid_FRF.Columns[9].Visibility = Visibility.Hidden;
                    DataGrid_MISC.Columns[9].Visibility = Visibility.Hidden;

                }


                if (strGrid == "BA")
                {
                    DataGrid_BA.Width = Constants.QAGridWidth;
                    DataGrid_BA.Height = Constants.QAGridHeight;

                    if (strMenu == "BA All")
                        mnuItemBA_All_click(sender, en);
                    else if (strMenu == "Pending")
                        mnuItemBA_Pending_click(sender, en);
                    else if (strMenu == "Research")
                        mnuItemBA_Research_click(sender, en);
                    else if (strMenu == "Completed")
                        mnuItemBA_Completed_click(sender, en);
                    else if (strMenu == "Onhold")
                        mnuItemBA_Onhold_click(sender, en);
                }
                else if (strGrid == "FRF")
                {
                    DataGrid_FRF.Width = Constants.QAGridWidth;
                    DataGrid_FRF.Height = Constants.QAGridHeight;

                  
                    if (strMenu == "All")
                        mnuItemFR_All_click(sender, en);
                    else if (strMenu == "Pending")
                        mnuItemFR_Pending_click(sender, en);
                    else if (strMenu == "Research")
                        mnuItemFR_Research_click(sender, en);
                    else if (strMenu == "Completed")
                        mnuItemFR_Completed_click(sender, en);
                    else if (strMenu == "Onhold")
                        mnuItemFR_Onhold_click(sender, en);
                }
                else if (strGrid == "Misc")
                {
                    DataGrid_MISC.Width = Constants.QAGridWidth;
                    DataGrid_MISC.Height = Constants.QAGridHeight;
                    if (strMenu == "All")
                        mnuItemMISC_All_click(sender, en);
                    else if (strMenu == "Pending")
                        mnuItemMISC_Pending_click(sender, en);
                    else if (strMenu == "Research")
                        mnuItemMISC_Research_click(sender, en);
                    else if (strMenu == "Completed")
                        mnuItemMISC_Completed_click(sender, en);
                    else if (strMenu == "Onhold")
                        mnuItemMISC_Onhold_click(sender, en);
                }

               
                ProgressViewer.endProgress();
            }
            catch (Exception ex)
            {
                ProgressViewer.endProgress();
            }

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
                    frfFormationbasin.SelectedValue  = dsFRF.Tables[0].Rows[0]["GEOLOGIC_PROVINCE"].ToString();
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

        #region DataGrid Binding Event
        //Binding BA or FRF or MISC Data depend upon Request
        public void BindData(int varQuery, string varTableName, string varRequestType, DataGrid dgGrid)
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
        #endregion

        #region BAclose click Event
        // BA Closing Event
        private void popBAclose_click(object sender, System.Windows.RoutedEventArgs e)
        {
            PopupBA.IsOpen = false;
            BindData(1, "BA", "", DataGrid_BA);
        }
        #endregion

        #region MISCclose click Event
        // MISC Closing Event
        private void popMISCclose_click(object sender, System.Windows.RoutedEventArgs e)
        {
            PopupMISC.IsOpen = false;
            BindData(1, "MISC", "", DataGrid_MISC);
        }
        #endregion

        #region FRFclose click Event
        // FRF Closing Event
        private void popFRFclose_click(object sender, System.Windows.RoutedEventArgs e)
        {
            PopupFRF.IsOpen = false;
            BindData(1, "FRF", "", DataGrid_FRF);
        }
        #endregion



        #region Functional Events




        // Retreving Mail Address
        public string GetEmail(string id, int type)
        {
            DBClass Dbobject = new DBClass();
            string stremail = "";
            try
            {
                StructureClass.StrucLogInData strucBAdata = new StructureClass.StrucLogInData();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();

                DataSet ds = new DataSet();

                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;




                strucBAdata.strUserID = id;
                Dbobject.Connect();
                if (type == 1)
                {
                    strucBAdata.QueryType = 3;
                }


                ds = (DataSet)Dbobject.SpUser_DataRetreive(ref strucBAdata, "BA-insert");

                Dbobject.close();
                if (ds.Tables[0].Rows.Count > 0)
                {

                    stremail = ds.Tables[0].Rows[0]["EMAIL_ADDRESS"].ToString();

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
            }
            finally
            {
                Dbobject.close();
            }
            return stremail;
        }

        //Saving BA Record
        private void SaveBAButton_Click(object sender, RoutedEventArgs e)
        {
            DBClass Dbobject = new DBClass();
            try
            {
                int x;
                x = BArowindex; string var_txtid = ((System.Data.DataRowView)(DataGrid_BA.SelectedItem)).Row.ItemArray[0].ToString();
                string var_txtStCode = ((System.Data.DataRowView)(DataGrid_BA.SelectedItem)).Row.ItemArray[20].ToString();
                string var_txtComments = ((System.Data.DataRowView)(DataGrid_BA.SelectedItem)).Row.ItemArray[6].ToString();
                string var_cmbStatus = ((System.Data.DataRowView)(DataGrid_BA.SelectedItem)).Row.ItemArray[3].ToString();
                string var_cmbOwner = ((System.Data.DataRowView)(DataGrid_BA.SelectedItem)).Row.ItemArray[22].ToString();
                string baname = ((System.Data.DataRowView)(DataGrid_BA.SelectedItem)).Row.ItemArray[25].ToString();
                string pi_user_id = ((System.Data.DataRowView)(DataGrid_BA.SelectedItem)).Row.ItemArray[26].ToString();
                bool b = GridMessage(DataGrid_BA, x, 4);

                if (b == false)
                {

                    return;

                }

                string email = GetEmail(pi_user_id, 0);

                if (var_cmbStatus == "Completed")
                {
                    if (var_txtStCode.Length == 0)
                    {
                        MessageBox.Show("Standard Code cannot be empty");
                        return;
                    }

                    SendMail.SendMail ml = new SendMail.SendMail();

                    string strMailBody;
                    if (email != "")
                    {
                        strMailBody = "Hi " + baname + " ,<br><br>";
                        strMailBody = strMailBody + "Your Standard Code Request has been added to the table as <b>" + baname + "- SC " + var_txtStCode + "</b>.";
                        strMailBody = strMailBody + "<br><br>Thanks<br>";
                        strMailBody = strMailBody + "" + Constants.Userid + "<br>";

                        ml.MailSend(email, Constants.strUserEmail, "Standard Code Request ID : " + var_txtid + "- Standard Code : " + var_txtStCode, strMailBody);
                    }
                }


                StructureClass.StrucBAInsert strucBAdata = new StructureClass.StrucBAInsert();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                DataSet ds = new DataSet();
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strucBAdata.strBAID = var_txtid;
                strucBAdata.Querytype = 18;
                strucBAdata.strStandardCode = var_txtStCode;
                strucBAdata.BA_SERVICE_TYPE = var_cmbStatus;
                strucBAdata.strRemark = var_txtComments;
                strucBAdata.strOwner = var_cmbOwner;
                Dbobject.Connect();
                ds = (DataSet)Dbobject.SpBusinessAssociate(ref strucBAdata, 2, "BA-insert");
                Dbobject.close();

                MessageBox.Show("Saved Successfully");
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
                MessageBox.Show("There is some Problem updating the information.Please  try again.");
            }
            finally
            {
                Dbobject.close();
            }

        }


        //Saving FRF Record
        private void SaveFRFButton_Click(object sender, RoutedEventArgs e)
        {
            DBClass Dbobject = new DBClass();
            try
            {
                int x;
                x = FRFrowindex;
                string var_txtid = ((System.Data.DataRowView)(DataGrid_FRF.SelectedItem)).Row.ItemArray[0].ToString();
                string var_cmbStatus = ((System.Data.DataRowView)(DataGrid_FRF.SelectedItem)).Row.ItemArray[1].ToString();
                string var_txtComments = ((System.Data.DataRowView)(DataGrid_FRF.SelectedItem)).Row.ItemArray[26].ToString();
                string var_txtStCode = ((System.Data.DataRowView)(DataGrid_FRF.SelectedItem)).Row.ItemArray[5].ToString();
                string var_cmbOwner = ((System.Data.DataRowView)(DataGrid_FRF.SelectedItem)).Row.ItemArray[34].ToString();
                string baname = ((System.Data.DataRowView)(DataGrid_FRF.SelectedItem)).Row.ItemArray[2].ToString();
                string pi_user_id = ((System.Data.DataRowView)(DataGrid_FRF.SelectedItem)).Row.ItemArray[38].ToString();
                bool b = GridMessage(DataGrid_FRF, x, 4);

                if (b == false)
                {

                    return;

                }
                string email = GetEmail(pi_user_id, 0);
                if (var_cmbStatus == "Completed")
                {
                    SendMail.SendMail ml = new SendMail.SendMail();

                    string strMailBody;
                    if (email != "")
                    {

                        strMailBody = "Hi " + baname + " ,<br><br>";
                        strMailBody = strMailBody + "Your Standard Code Request has been added to the table as <b>" + baname + "- SC " + var_txtStCode + "</b>.";
                        strMailBody = strMailBody + "<br><br>Thanks<br>";
                        strMailBody = strMailBody + "" + Constants.Userid + "<br>";
                        ml.MailSend(email, Constants.strUserEmail, "Standard Code Request ID : " + var_txtid + "- Standard Code : " + var_txtStCode, strMailBody);

                    }


                }

                StructureClass.StrucFRF strucBAdata = new StructureClass.StrucFRF();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                DataSet ds = new DataSet();
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strucBAdata.strFLD_KEY = var_txtid;
                strucBAdata.strQuerytype = 13;
                strucBAdata.intFIELD_ID = var_txtStCode;
                strucBAdata.strSTATUS = var_cmbStatus;
                strucBAdata.strREMARK = var_txtComments;
                strucBAdata.strOwner = var_cmbOwner;
                Dbobject.Connect();
                ds = (DataSet)Dbobject.SpFRF_NEW(ref strucBAdata, 2, "FRF-insert");
                Dbobject.close();
                MessageBox.Show("Saved Successfully");
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
                MessageBox.Show("There is some Problem updating the information.Please try again.");
            }
            finally
            {
                Dbobject.close();
            }

        }
        //Saving MISC Record
        private void SaveMISCButton_Click(object sender, RoutedEventArgs e)
        {
            DBClass Dbobject = new DBClass();
            try
            {
                int x;
                x = Miscrowindex;
                string var_txtStCode = "";
                string var_txtComments = "";
                string var_txtid = ((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[16].ToString();
                if (((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[2].ToString().Length > 50)
                {
                    MessageBox.Show("The Standard Code Length cannot be more than 50");
                    return;
                }
                else
                {
                    var_txtStCode = ((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[2].ToString();
                }
                if (((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[3].ToString().Length > 240)
                {
                    MessageBox.Show("Comments Length cannot be more than 240");
                    return;
                }
                else
                {
                    var_txtComments = ((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[3].ToString();
                }
                string var_cmbStatus = ((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[1].ToString();
                string var_cmbOwner = ((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[18].ToString();
                string baname = ((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[20].ToString();
                string pi_user_id = ((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[20].ToString();
                bool b = GridMessage(DataGrid_MISC, x, 4);
                if (b == false)
                {

                    return;

                }

                string email = GetEmail(pi_user_id, 0);

                if (var_cmbStatus == "Completed")
                {
                    SendMail.SendMail ml = new SendMail.SendMail();

                    string strMailBody;
                    if (email != "")
                    {

                        strMailBody = "Hi " + baname + " ,<br><br>";
                        strMailBody = strMailBody + "Your Standard Code Request has been added to the table as <b>" + baname + "- SC " + var_txtStCode + "</b>.";
                        strMailBody = strMailBody + "<br><br>Thanks<br>";
                        strMailBody = strMailBody + "" + Constants.Userid + "<br>";
                        ml.MailSend(email, Constants.strUserEmail, "Standard Code Request ID : " + var_txtid + "- Standard Code : " + var_txtStCode, strMailBody);

                    }


                }

                StructureClass.StrucMISCInsert strucBAdata = new StructureClass.StrucMISCInsert();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                DataSet ds = new DataSet();
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strucBAdata.strMISC_KEY = int.Parse(var_txtid);
                strucBAdata.intQuerytype = 9;
                strucBAdata.strBAID = var_txtStCode;
                strucBAdata.strREQUEST_TYPE = var_cmbStatus;
                strucBAdata.strRemarks = var_txtComments;
                strucBAdata.strOwner = var_cmbOwner;
                Dbobject.Connect();
                ds = (DataSet)Dbobject.SpMISC(ref strucBAdata, 2, "misc-insert");
                Dbobject.close();
                MessageBox.Show("Saved Successfully");
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
                MessageBox.Show("There is some Problem updating the information.Please  try again.");
            }
            finally
            {
                Dbobject.close();
            }

        }
        private void popclose_click(object sender, System.Windows.RoutedEventArgs e)
        {
            PopupBA.IsOpen = false;
        }
        //Viewing BA Record
        private void ViewBAButton_Click(object sender, RoutedEventArgs e)
        {
            ProgressViewer.startProgress(100.0, 100.0);
            string var_txtid = ((System.Data.DataRowView)(DataGrid_BA.SelectedItem)).Row.ItemArray[0].ToString();
            PopupBA.Placement = PlacementMode.Center;
            PopupBA.Width = 740;
            PopupBA.IsOpen = true;
            Service_Binding();
            BindStateCode();
            BindBAData(var_txtid);
            ProgressViewer.endProgress();
        }

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
        //Saving MISC Record
        private void btnMISCSubmit_click(object sender, System.Windows.RoutedEventArgs e)
        {
            DBClass Dbobject = new DBClass();
            try
            {
                StructureClass.StrucMISCInsert strucMiscdata = new StructureClass.StrucMISCInsert();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                DataSet ds = new DataSet();
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strucMiscdata.intQuerytype = 7;
                strucMiscdata.strMISC_KEY = int.Parse(misc_id);

                //field
                //---

                strucMiscdata.strRequestDescription = txtReqDiscription.Text.ToString();
                if (txtMiscID.Text != "" && txtMiscID.Text != null)
                {
                    if (!numberEx.IsMatch(txtMiscID.Text))
                    {
                        MessageBox.Show("MISC ID should be numeric");
                        return;
                    }
                    else
                    {
                        strucMiscdata.strBAID = txtMiscID.Text.ToString();
                    }
                }

                strucMiscdata.strUserName = txtUserName.Text.ToString();
                strucMiscdata.strRemarks = txtRemarks.Text.ToString();
                //STANDARDCODE
                //    MISC_ID
                strucMiscdata.strTEXASSURVEY_NUMBER = txtTexasSurveyNumber.Text.ToString();
                strucMiscdata.strTEXASSURVEY_LONGNAME = txtLongName.Text.ToString();
                strucMiscdata.strTEXASSURVEY_REMARKS = txtTeaxsRemarks.Text.ToString();
                strucMiscdata.strMONUMENT_ID = txtMonumentID.Text.ToString();
                strucMiscdata.strMONUMENT_LATITUDE = txtMonumentLatitude.Text.ToString();
                strucMiscdata.strMONUMENT_LONGITUDE = txtMonumentLongitude.Text.ToString();
                strucMiscdata.strMONUMENT_NAME = txtMonumentName.Text.ToString();
                strucMiscdata.strMONUMENT_REMARKS = txtMonumentRemarks.Text.ToString();

                Dbobject.Connect();

                ds = (DataSet)Dbobject.SpMISC(ref strucMiscdata, 2, "MISC");

                Dbobject.close();

                MessageBox.Show("Saved Successfully");

                PopupMISC.IsOpen = false;
                if (strMISCStatus == "Pending")
                {
                    BindData(0, "MISC", "Pending", DataGrid_MISC);
                }
                else if (strMISCStatus == "Research")
                {
                    BindData(0, "MISC", "Research", DataGrid_MISC);
                }
                else if (strMISCStatus == "On Hold")
                {
                    BindData(0, "MISC", "On Hold", DataGrid_MISC);
                }
                else if (strMISCStatus == "Completed")
                {
                    BindData(0, "MISC", "Completed", DataGrid_MISC);
                }
                else
                {
                    BindData(1, "MISC", "", DataGrid_MISC);
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
                MessageBox.Show("There is some Problem updating the information.Please  try again.");
            }
            finally
            {
                Dbobject.close();
            }
        }

        //Saving FRF Record
        private void btnFRFsave_click(object sender, System.Windows.RoutedEventArgs e)
        {
            DBClass Dbobject = new DBClass();
            try
            {
                if (frfFormationGeoAge.Text.Length > 0)
                {
                    if (frfFormationGeoAge.Text.Length != 3)
                    {
                        MessageBox.Show("Geo age should be 3 digits");
                        return;
                    }
                }

                StructureClass.StrucFRF strucFRFdata = new StructureClass.StrucFRF();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();

                DataSet ds = new DataSet();

                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strucFRFdata.strQuerytype = 11;

                strucFRFdata.strFLD_KEY = frf_id;
                //field
                //---

                strucFRFdata.strFIELD_NAME = frfFieldName.Text;
                if (frfcmbState.SelectedValue != null)
                {
                    strucFRFdata.strPROVINCE_STATE_FCXS = frfcmbState.SelectedValue.ToString();
                }
                if (frfcmbCountry.SelectedValue != null)
                {
                    strucFRFdata.strCOUNTY = frfcmbCountry.SelectedValue.ToString();
                }
                if (frfcmbDistrict.SelectedValue != null)
                {
                    strucFRFdata.strDISTRICT = frfcmbDistrict.SelectedValue.ToString();
                }
                strucFRFdata.strFIELD_ALIAS_FA = frfMMS.Text;
                if (frfcmbSource.SelectionBoxItem != null)
                {
                    strucFRFdata.strSOURCE_MMS_FA = frfcmbSource.SelectionBoxItem.ToString();
                }
                if (frfGrad.Text != "" && frfGrad.Text != null)
                {
                    if (!floatEx.IsMatch(frfGrad.Text))
                    {
                        MessageBox.Show("TempGradient should be Floating Point Number. Eg: 2.0");
                        frfGrad.Focus();
                        return;
                    }
                    else
                    {
                        string[] strTemp = frfGrad.Text.Split('.');
                        if (strTemp[0].Length > 6)
                        {
                            MessageBox.Show("TempGradient allows Only 6 digits number with 2 floating number. Eg: 2.22");
                            frfGrad.Focus();
                            return;
                        }
                        else if (strTemp.Length == 2)
                        {
                            if (strTemp[1].Length > 2)
                            {
                                MessageBox.Show("TempGradient allows Only 6 digits number with 2 floating number. Eg: 2.22");
                                frfGrad.Focus();
                                return;
                            }
                            else
                            {
                                strucFRFdata.strPI_TEMP_GRADIENT = frfGrad.Text.ToString();
                            }
                        }
                        else
                        {
                            strucFRFdata.strPI_TEMP_GRADIENT = frfGrad.Text.ToString();
                        }

                    }


                }
                strucFRFdata.strFIELD_ALIAS_FCXS = frfStateCode.Text;
                strucFRFdata.strPROVINCE_STATE_FIELD = frfAltState.Text;
                if (frfStandardCode.Text != "" && frfStandardCode.Text != null)
                {
                    if (!numberEx.IsMatch(frfStandardCode.Text))
                    {
                        MessageBox.Show("Field Standard Code should be numeric");
                        frfStandardCode.Focus();
                        return;
                    }
                    else
                    {
                        strucFRFdata.intFIELD_ID = frfStandardCode.Text.ToString();
                    }

                }

                strucFRFdata.strPI_USER_ID = frfusername.Text;
                frfActive.IsChecked = true;

                strucFRFdata.strREMARK_FIELD = frfComments.Text;

                //Reservoir
                //------------
                strucFRFdata.strPOOL_NAME = frfPoolName.Text;
                strucFRFdata.strPOOL_ALIAS_PCXS = frfRSVR.Text;
                if (frfcmbReservState.SelectedValue != null)
                {
                    strucFRFdata.strPROVINCE_STATE_PCXS = frfcmbReservState.SelectedValue.ToString();
                }
                if (frfReservStandardCode.Text != "" && frfReservStandardCode.Text != null)
                {
                    if (!numberEx.IsMatch(frfReservStandardCode.Text))
                    {
                        MessageBox.Show("Reservoir Standard Code should be numeric");
                        frfReservStandardCode.Focus();
                        return;
                    }
                    else
                    {
                        strucFRFdata.intPOOL_ID_R = frfReservStandardCode.Text.ToString();
                    }

                }
                frfReservoirActive.IsChecked = true;

                //Pool
                //-----
                if (frfPoolId.Text != "" && frfPoolId.Text != null)
                {
                    if (!numberEx.IsMatch(frfPoolId.Text))
                    {
                        MessageBox.Show("Pool Pool_ID should be numeric");
                        frfPoolId.Focus();
                        return;
                    }
                    else
                    {
                        strucFRFdata.intPOOL_ID = frfPoolId.Text.ToString();
                    }
                }

                if (frfPoolFieldsID.Text != "" && frfPoolFieldsID.Text != null)
                {
                    if (!numberEx.IsMatch(frfPoolFieldsID.Text))
                    {
                        MessageBox.Show("Pool Filed_ID should be numeric");
                        frfPoolFieldsID.Focus();
                        return;
                    }
                    else
                    {
                        strucFRFdata.intFIELD_ID_P = frfPoolFieldsID.Text.ToString();
                    }
                }

                strucFRFdata.strPROVINCE_STATE_POOL = frfPoolState.Text;

                if (frfPoolFormid.Text != "" && frfPoolFormid.Text != null)
                    strucFRFdata.intFORM_ID = frfPoolFormid.Text.ToString();

                frfPoolActive.IsChecked = true;
                strucFRFdata.strREMARKS_POOL = frfPoolComments.Text;

                //Formation
                //------
                strucFRFdata.strFORM_NAME = frfFormationName.Text;

                if (frfFormationApi.Text != "" && frfFormationApi.Text != null)
                    strucFRFdata.intAPI = frfFormationApi.Text.ToString();
                if (frfcmbFormationRegion.SelectedValue != null)
                {
                    strucFRFdata.strFORM_ALIAS = frfcmbFormationRegion.SelectedValue.ToString();
                }
                if (frfFormationbasin.SelectedValue != null)
                {
                    strucFRFdata.strGEOLOGIC_PROVINCE = frfFormationbasin.SelectedValue.ToString();
                }
                strucFRFdata.strPERFTOP = frfFormationTop.Text;
                strucFRFdata.strPERFBOTTOM = frfFormationBottom.Text;

                if (frfFormationTD.Text != "" && frfFormationTD.Text != null)
                {
                    if (!floatEx.IsMatch(frfFormationTD.Text))
                    {
                        MessageBox.Show("TD should be Floating Point Number. Eg: 2.0");
                        frfFormationTD.Focus();
                        return;
                    }
                    else
                    {
                        string[] strTemp = frfFormationTD.Text.Split('.');
                        if (strTemp[0].Length > 5)
                        {
                            MessageBox.Show("TD allows Only 5 digits number with 5 floating number. Eg: 2.0");
                            frfFormationTD.Focus();
                            return;
                        }
                        else if (strTemp.Length == 2)
                        {
                            if (strTemp[1].Length > 6)
                            {
                                MessageBox.Show("TD allows Only 5 digits number with 5 floating number. Eg: 2.0");
                                frfFormationTD.Focus();
                                return;
                            }
                            else
                            {
                                strucFRFdata.strPI_MD = frfFormationTD.Text.ToString();
                            }
                        }
                        else
                        {
                            strucFRFdata.strPI_MD = frfFormationTD.Text.ToString();
                        }

                    }

                }

                strucFRFdata.strFORM_AGE = frfFormationGeoAge.Text;
                frfFormationActive.IsChecked = true;
                strucFRFdata.strREMARK = frfFormationComments.Text;
                if (frfPoolFieldsID.Text != frfStandardCode.Text && frfPoolFieldsID.Text.Trim() != "" && frfStandardCode.Text.Trim() != "")
                {
                    MessageBox.Show("Field - Standard Code and Pool - FieldID Must be same");
                    frfPoolFieldsID.Focus();
                    return;
                }
                if (frfPoolId.Text != frfReservStandardCode.Text && frfPoolId.Text.Trim() != "" && frfReservStandardCode.Text.Trim() != "")
                {
                    MessageBox.Show("Pool - PoolID and Reservoir - Standard Code Must be same");
                    frfPoolId.Focus();
                    return;
                }
                Dbobject.Connect();

                ds = (DataSet)Dbobject.SpFRF_NEW(ref strucFRFdata, 2, "FRF");

                Dbobject.close();

                MessageBox.Show("Saved Successfully");

                PopupFRF.IsOpen = false;
                if (strFRFStatus == "Pending")
                {
                    BindData(0, "FRF", "Pending", DataGrid_FRF);
                }
                else if (strFRFStatus == "Research")
                {
                    BindData(0, "FRF", "Research", DataGrid_FRF);
                }
                else if (strFRFStatus == "On Hold")
                {
                    BindData(0, "FRF", "On Hold", DataGrid_FRF);
                }
                else if (strFRFStatus == "Completed")
                {
                    BindData(0, "FRF", "Completed", DataGrid_FRF);
                }
                else
                {
                    BindData(1, "FRF", "", DataGrid_FRF);
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
                //MessageBox.Show("There is some problem updating  ");
            }
            finally
            {
                Dbobject.close();
            }

        }
        //Saving BA Record

        private void btnBASave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            DBClass Dbobject = new DBClass();
            try
            {


                //attachment();

                StructureClass.StrucBAInsert strucBAdata = new StructureClass.StrucBAInsert();
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();

                DataSet ds = new DataSet();

                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strucBAdata.strBAID = ba_id;
                strucBAdata.strBACode = txtMnemonic.Text;
                if (txtStandardCode.Text == "")
                {
                    MessageBox.Show("Standard Code cannot be Empty");
                    return;
                }
                else
                {
                    if (!numberEx.IsMatch(txtStandardCode.Text))
                    {
                        MessageBox.Show("Standard Code should be numeric");
                        return;
                    }
                    else
                    {
                        if (txtStandardCode.Text.Length > 20)
                        {
                            MessageBox.Show("Enter Standard Code Length 20 or less digits");
                            txtStandardCode.Focus();
                            return;
                        }
                        else
                        {
                            strucBAdata.strStandardCode = txtStandardCode.Text;
                        }
                    }
                }

                strucBAdata.strBAIdAlias = txtState2.Text;
                string strTempBAName = strucBAdata.strLastName + strucBAdata.strFirstName + strucBAdata.strMiddleInitial;
                if (strTempBAName.Length > 60)
                {
                    strucBAdata.strBAName = strTempBAName.Remove(60, strTempBAName.Length - 60);
                }
                else
                {
                    strucBAdata.strBAName = strTempBAName;

                }
                if (cboServiceType.SelectedValue != null)
                {
                    strucBAdata.strBaServiceType = cboServiceType.SelectedValue.ToString();
                }
                strucBAdata.strBAShortName = txtBAShortName.Text;
                strucBAdata.strBAStateCode = txtStateCode.Text;
                strucBAdata.strBAUserId = txtUserId_BA.Text;
                if (txtCity.Text.Length > 12)
                {
                    MessageBox.Show("Enter City Length 12 or less chars");
                    txtCity.Focus();
                    return;
                }
                else
                {
                    strucBAdata.strCity = txtCity.Text;
                }
                strucBAdata.strFaxNum = txtFax.Text;
                strucBAdata.strFirstAddressLine = txtAdress1.Text;
                strucBAdata.strFirstName = txtFirstName.Text;
                strucBAdata.strLastName = txtLastName.Text;
                strucBAdata.strMiddleInitial = txtBAMiddleName.Text;

                strucBAdata.strPhoneNum = txtPhone.Text;
                if (txtZip.Text.Length > 12)
                {
                    MessageBox.Show("Enter Postal Zip Length 12 or less chars");
                    txtZip.Focus();
                    return;
                }
                else
                {
                    strucBAdata.strPostalZipCode = txtZip.Text;
                }
                if (cbostate.SelectedValue != null)
                {
                    strucBAdata.strProvinceState = cbostate.SelectedValue.ToString();
                }
                strucBAdata.strRemark = txtComments.Text;
                strucBAdata.strSecondAddressLine = txtAddress2.Text;
                strucBAdata.Querytype = 17;

                Dbobject.Connect();

                ds = (DataSet)Dbobject.SpBusinessAssociate(ref strucBAdata, 2, "BA-insert");

                Dbobject.close();

                MessageBox.Show("Saved Successfully");

                PopupBA.IsOpen = false;
                if (strBAStatus == "Pending")
                {
                    BindData(2, "BA", "", DataGrid_BA);
                }
                else if (strBAStatus == "Research")
                {
                    BindData(3, "BA", "", DataGrid_BA);
                }
                else if (strBAStatus == "On Hold")
                {
                    BindData(4, "BA", "", DataGrid_BA);
                }
                else if (strBAStatus == "Completed")
                {
                    BindData(5, "BA", "", DataGrid_BA);
                }
                else
                {
                    BindData(1, "BA", "", DataGrid_BA);
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

        //Viewing FRF Record
        private void ViewFRFButton_Click(object sender, RoutedEventArgs e)
        {
            ProgressViewer.startProgress(100.0, 100.0);
            DataSet dsFRF = new DataSet();
            if (Constants.DsFRF_Schema == null)
            {
                Constants.DsFRF_Schema = DBClass.LoadFRFDataSchema();
            }
            dsFRF = Constants.DsFRF_Schema;
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

            BasinCollection = (CollectionViewSource)this.FindResource("BasinCollection");
            Basin = dsFRF.Tables[4];
            BasinCollection.Source = ((IListSource)Basin).GetList();
           

            string var_txtid = ((System.Data.DataRowView)(DataGrid_FRF.SelectedItem)).Row.ItemArray[0].ToString();
            PopupFRF.Placement = PlacementMode.Center;
            PopupFRF.Width = 740;
            PopupFRF.IsOpen = true;
            BindFRFData(var_txtid);
            ProgressViewer.endProgress();

        }
        //Viewing MISC Record
        private void ViewMISCButton_Click(object sender, RoutedEventArgs e)
        {
            ProgressViewer.startProgress(100.0, 100.0);
            string var_txtid = ((System.Data.DataRowView)(DataGrid_MISC.SelectedItem)).Row.ItemArray[16].ToString();
            PopupMISC.Placement = PlacementMode.Center;
            PopupMISC.Width = 740;
            PopupMISC.IsOpen = true;
            BindMISCData(var_txtid);
            ProgressViewer.endProgress();
        }

        private void DataGrid_BA_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (_defaultRowControlTemplate == null)
            {
                _defaultRowControlTemplate = e.Row.Template;
            }

            if (e.Row.Item == CollectionView.NewItemPlaceholder)
            {
                e.Row.Template = _newRowControlTemplate;
                e.Row.UpdateLayout();
            }
        }

        private void DataGrid_BA_UnloadingRow(object sender, DataGridRowEventArgs e)
        {
            if (e.Row.Item == CollectionView.NewItemPlaceholder && e.Row.Template != _defaultRowControlTemplate)
            {
                e.Row.Template = _defaultRowControlTemplate;
                e.Row.UpdateLayout();
            }
        }

        private void Row_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row.Item == CollectionView.NewItemPlaceholder && row.Template == _newRowControlTemplate)
            {
                // for a new row update the template and open for edit
                row.Template = _defaultRowControlTemplate;
                row.UpdateLayout();

                DataGrid_BA.CurrentItem = row.Item;
                DataGridCell cell = Helper.GetCell(DataGrid_BA, DataGrid_BA.Items.IndexOf(row.Item), 0);
                cell.Focus();
                DataGrid_BA.BeginEdit();
            }
        }

        private void DataGrid_BA_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            IEditableCollectionView iecv = CollectionViewSource.GetDefaultView((sender as DataGrid).ItemsSource) as IEditableCollectionView;
            if (iecv.IsAddingNew)
            {
                // need to wait till after the operation as the NewItemPlaceHolder is added after
                Dispatcher.Invoke(new DispatcherOperationCallback(ResetNewItemTemplate), DispatcherPriority.ApplicationIdle, DataGrid_BA);
            }
        }

        private object ResetNewItemTemplate(object arg)
        {
            DataGridRow row = Helper.GetRow(DataGrid_BA, DataGrid_BA.Items.Count - 1);
            if (row.Template != _newRowControlTemplate)
            {
                row.Template = _newRowControlTemplate;
                row.UpdateLayout();
            }
            return null;
        }

        #endregion Functional Events

        #region BA Menu Items Click Events
        //Viewing BA ALL Record
        private void mnuItemBA_All_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "BUSINESS ASSOCIATE - ALL REQUESTS";

            DataGrid_MISC.Visibility = Visibility.Hidden;
            DataGrid_FRF.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Visible;
            strBAStatus = "";

            BindData(1, "BA", "", DataGrid_BA);
        }
        //Viewing BA Pending Record
        private void mnuItemBA_Pending_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "BUSINESS ASSOCIATE - PENDING REQUESTS";

            DataGrid_MISC.Visibility = Visibility.Hidden;
            DataGrid_FRF.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Visible;
            strBAStatus = "Pending";
            BindData(2, "BA", "", DataGrid_BA);
        }
        //Viewing BA Research Record
        private void mnuItemBA_Research_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "BUSINESS ASSOCIATE - RESEARCH";

            DataGrid_MISC.Visibility = Visibility.Hidden;
            DataGrid_FRF.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Visible;
            strBAStatus = "Research";
            BindData(3, "BA", "", DataGrid_BA);
        }
        //Viewing BA Completed Record
        private void mnuItemBA_Completed_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "BUSINESS ASSOCIATE - COMPLETED REQUESTS";

            DataGrid_MISC.Visibility = Visibility.Hidden;
            DataGrid_FRF.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Visible;

            strBAStatus = "Completed";
            BindData(5, "BA", "", DataGrid_BA);
        }
        //Viewing BA OnHold Record
        private void mnuItemBA_Onhold_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "BUSINESS ASSOCIATE - ONHOLD REQUESTS";

            DataGrid_MISC.Visibility = Visibility.Hidden;
            DataGrid_FRF.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Visible;
            strBAStatus = "On Hold";

            BindData(4, "BA", "", DataGrid_BA);
        }

        private void mnuItemBA_Click_1(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);

            mnuUnselect = (MenuItem)sender;

        }

        #endregion

        #region FRF Menu Items Click Events
        //Viewing FRF ALL Record
        private void mnuItemFR_All_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "FIELD RESERVOIR FORMATION - ALL REQUESTS";
            DataGrid_MISC.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Hidden;
            DataGrid_FRF.Visibility = Visibility.Visible;

            strFRFStatus = "FRF";
            BindData(1, "FRF", "", DataGrid_FRF);
        }
        //Viewing FRF Pending Record
        private void mnuItemFR_Pending_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "FIELD RESERVOIR FORMATION - PENDING REQUESTS";
            DataGrid_MISC.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Hidden;
            DataGrid_FRF.Visibility = Visibility.Visible;
            strFRFStatus = "Pending";
            BindData(0, "FRF", "Pending", DataGrid_FRF);
        }
        //Viewing FRF Research Record
        private void mnuItemFR_Research_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "FIELD RESERVOIR FORMATION - RESEARCH";
            DataGrid_MISC.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Hidden;
            DataGrid_FRF.Visibility = Visibility.Visible;
            strFRFStatus = "Research";
            BindData(0, "FRF", "Research", DataGrid_FRF);
        }
        //Viewing FRF Completed Record
        private void mnuItemFR_Completed_click(object sender, RoutedEventArgs e)
        {

            lblname.Text = "FIELD RESERVOIR FORMATION - COMPLETED";
            DataGrid_MISC.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Hidden;
            DataGrid_FRF.Visibility = Visibility.Visible;
            strFRFStatus = "Completed";
            BindData(0, "FRF", "Completed", DataGrid_FRF);

        }
        //Viewing FRF OnHold Record
        private void mnuItemFR_Onhold_click(object sender, RoutedEventArgs e)
        {

            lblname.Text = "FIELD RESERVOIR FORMATION - ONHOLD REQUESTS";
            DataGrid_MISC.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Hidden;
            DataGrid_FRF.Visibility = Visibility.Visible;
            strFRFStatus = "On Hold";
            BindData(0, "FRF", "On Hold", DataGrid_FRF);
        }

        private void mnuItemFieldR_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);

            mnuUnselect = (MenuItem)sender;


        }
        #endregion

        #region MISC Menu Items Click Events
        //Viewing MISC ALL Record
        private void mnuItemMISC_All_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "MISC REQUEST - ALL REQUESTS";
            DataGrid_FRF.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Hidden;
            DataGrid_MISC.Visibility = Visibility.Visible;
            strMISCStatus = "";
            BindData(1, "MISC", "", DataGrid_MISC);
        }
        //Viewing MISC Pending Record
        public void mnuItemMISC_Pending_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "MISC REQUEST - PENDING REQUESTS";
            DataGrid_FRF.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Hidden;
            DataGrid_MISC.Visibility = Visibility.Visible;
            strMISCStatus = "Pending";
            BindData(0, "MISC", "Pending", DataGrid_MISC);
        }
        //Viewing MISC Research Record
        private void mnuItemMISC_Research_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "MISC REQUEST - RESEARCH";
            DataGrid_FRF.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Hidden;
            DataGrid_MISC.Visibility = Visibility.Visible;
            strMISCStatus = "Research";
            BindData(0, "MISC", "Research", DataGrid_MISC);
        }
        //Viewing MISC Completed Record
        private void mnuItemMISC_Completed_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "MISC REQUEST - COMPLETED REQUESTS";
            DataGrid_FRF.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Hidden;
            DataGrid_MISC.Visibility = Visibility.Visible;
            strMISCStatus = "Completed";
            BindData(0, "MISC", "Completed", DataGrid_MISC);

        }
        //Viewing MISC OnHold Record
        private void mnuItemMISC_Onhold_click(object sender, RoutedEventArgs e)
        {
            lblname.Text = "MISC REQUEST - ONHOLD";
            DataGrid_FRF.Visibility = Visibility.Hidden;
            DataGrid_BA.Visibility = Visibility.Hidden;
            DataGrid_MISC.Visibility = Visibility.Visible;
            strMISCStatus = "On Hold";
            BindData(0, "MISC", "On Hold", DataGrid_MISC);

        }

        private void mnuItemMisc_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);

            mnuUnselect = (MenuItem)sender;
        }

        #endregion

        #region BindMenuItemIcon
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

            mnuUnselect.Background = Brushes.Transparent;

            LinearGradientBrush lgb = new LinearGradientBrush();
            lgb.StartPoint = new Point(0.5, 0);
            lgb.EndPoint = new Point(0.5, 1);
            lgb.GradientStops.Add(new GradientStop(Color.FromRgb(129, 164, 210), 1));
            lgb.GradientStops.Add(new GradientStop(Color.FromRgb(129, 164, 210), 0));
            lgb.GradientStops.Add(new GradientStop(Color.FromRgb(188, 211, 243), 0.5));

            mnuUnselect.Background = lgb;
            mnuUnselect.Foreground = Brushes.Black;
            mnuUnselect.Icon = iconCloseImage;
            mnuUnselect.EndInit();

            mi.Icon = iconOpenImage;
            mi.Background = Brushes.Navy;
            mi.Foreground = Brushes.White;
            mi.EndInit();

            //mnuMain.Visibility = Visibility.Hidden;
            //myGrid.ColumnDefinitions[0].Width = new GridLength(0);
        }
        #endregion

        #region BA CellEditEnding Event
        // BA Validations Event
        private void DataGrid_BA_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {


            if (e.Column.Header.ToString() == "Standard Code")
            {
                DataGrid dr = (DataGrid)sender;

                if (((System.Windows.Controls.TextBox)(e.EditingElement)).Text == "")
                {
                    MessageBox.Show("Standard Code cannot be Empty");
                    ((System.Windows.Controls.TextBox)(e.EditingElement)).Undo();
                    return;
                }


                if (!numberEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                {
                    MessageBox.Show("Standard Code should be numeric");
                    DataGrid_BA.BeginEdit();
                    ((System.Windows.Controls.TextBox)(e.EditingElement)).Undo();
                    ((TextBox)(e.EditingElement)).Focus();
                    return;

                }
            }


        }
        #endregion

        #region FRF CellEditEnding Event
        // FRF Validations Event
        private void DataGridFRF_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Standard Code")
            {
                DataGrid dr = (DataGrid)sender;

                if (((System.Windows.Controls.TextBox)(e.EditingElement)).Text == "")
                {
                    MessageBox.Show("Standard Code cannot be Empty");
                    ((TextBox)(e.EditingElement)).Text = "";
                    ((TextBox)(e.EditingElement)).Focus();

                    return;
                }


                if (!numberEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                {
                    MessageBox.Show("Standard Code should be numeric");
                    DataGrid_FRF.BeginEdit();
                    ((TextBox)(e.EditingElement)).Text = "";

                    return;

                }
            }



        }
        #endregion

        #region MISC CellEditEnding Event
        // MISC Validations Event
        private void DataGridMISC_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Standard Code")
            {
                DataGrid dr = (DataGrid)sender;

                if (((System.Windows.Controls.TextBox)(e.EditingElement)).Text == "")
                {
                    MessageBox.Show("Standard Code cannot be Empty");
                    ((System.Windows.Controls.TextBox)(e.EditingElement)).Undo();
                    return;
                }


                if (!numberEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                {
                    MessageBox.Show("Standard Code should be numeric");
                    DataGrid_MISC.BeginEdit();
                    ((TextBox)(e.EditingElement)).Text = "";

                    return;

                }
            }



        }
        #endregion

        #region FRF State SelectionChanged Event
        // This Method is used for the Changing Country Combox Items Depend upon State Selection

        private void frfcmbState_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (intFirst.Equals(1))
                {
                    if (frfcmbState.SelectedValue != null)
                    {
                        DataView dr = (DataView)(TempCountrys.DefaultView);
                        dr.RowFilter = "province_state =" + frfcmbState.SelectedValue.ToString();
                        Countrys = dr.Table.DefaultView.Table;
                        CountryCollection.Source = ((IListSource)Countrys).GetList();
                        frfcmbCountry.SelectedIndex = -1;

                    }
                    else
                    {
                        DataTable dr = (DataTable)(TempCountrys.Clone());

                        Countrys = dr;
                        CountryCollection.Source = ((IListSource)Countrys).GetList();
                        frfcmbCountry.SelectedIndex = -1;
                    }

                }
                if (frfcmbState.SelectedValue != null)
                {
                    if (frfcmbState.SelectedValue.ToString().Equals("42"))
                    {
                        frfcmbDistrict.IsEnabled = true;
                    }
                    else
                    {
                        frfcmbDistrict.IsEnabled = false;
                        frfcmbDistrict.SelectedIndex = -1;
                    }
                }
                else
                {
                    frfcmbDistrict.IsEnabled = false;
                    frfcmbDistrict.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

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


        public bool GridMessage(DataGrid dg, int rowindex, int colindex)
        {
            if (rowindex > -1)
            {


                DataGridRow row = Helper.GetRow(dg, rowindex);

                ComboBox txt = Helper.GetCell(dg, rowindex, colindex).Content as ComboBox;


                if (txt.SelectionBoxItem.ToString() == "Completed")
                {
                    if (MessageBox.Show("Are you sure you want to complete the Request?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.No)
                    {

                        txt.Text = ((System.Windows.Controls.ComboBox)(Helper.GetCell(dg, rowindex, dg.Columns.Count - 1).Content)).Text;


                        return false;
                    }
                    else
                        return true;
                }
                else
                    return true;
            }
            else
                return true;
        }

        private void DataGrid_BA_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BArowindex = DataGrid_BA.SelectedIndex;

        }

        private void DataGrid_FRF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FRFrowindex = DataGrid_FRF.SelectedIndex;
        }

        private void DataGrid_MISC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Miscrowindex = DataGrid_MISC.SelectedIndex;
        }


    }
}
