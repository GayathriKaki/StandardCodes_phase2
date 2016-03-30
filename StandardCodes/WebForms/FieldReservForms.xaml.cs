using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Collections;
using System.ComponentModel;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using StandardCodes.BusinessLayer;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Windows.Forms.Integration;
using System.Data.OracleClient;
using IHS.WPF.UI.ProgressIndicator;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace StandardCodes.WebForms
{
    /// Interaction logic for FieldReservForms.xaml

    //****************************************************************************************************************************
    //Module Name:  FieldReservForms
    //Created By:   Raja Sekhar 
    //Created Date:
    //Modified By: Raja Sekhar
    //Date:        05/Oct/10

    //****************************************************************************************************************************
    
    public partial class FieldReservForms : Page
    {

        #region Private Data

        private GroupStyle _defaultGroupStyle;
        private IEditableCollectionView iecv;
        private bool _initialColumnSetup = false;

        private CollectionViewSource CountryCollection;
        public DataTable Countrys { get; private set; }

        private CollectionViewSource DistrictCollection;
        public DataTable District { get; private set; }

        private CollectionViewSource StateCollection;
        public DataTable State { get; private set; }

        private CollectionViewSource RegionCollection;
        public DataTable Region { get; private set; }

        private CollectionViewSource BasinCollection;
        public DataTable Basin { get; private set; }

        MenuItem mnuUnselect = new MenuItem();
        private readonly Regex numberEx = new Regex(@"^[0-9]+$");
        private readonly Regex floatEx = new Regex(@"^[0-9.]+$");
        public string strCaption = "Standard Codes";
        private string strError = "0";
        private static int intMenuCount = 0;

        #endregion Private Data

        #region FieldReservForms
        public FieldReservForms()
        {
            try
            {
                InitializeComponent();
                ProgressViewer.startProgress(100.0, 100.0);

                //_defaultGroupStyle = (GroupStyle)myGrid.FindResource("gs_Default");
                //if (_defaultGroupStyle == null)
                //{
                //    throw new NullReferenceException("Unable to find gs_Default from the resources.");
                //}
                // Binding the default structure Field table to DataGrid
                BindField();
                ColumnDisplay(0, 11);

                //BindMenuItemIcon(mnuItemFiled, mnuUnselect);

                //mnuUnselect = mnuItemFiled;
                lblHeader.Content = "Field Reservoir Formation  - Field";
                // Showing first 12 cells(Only Field items Showing)
               
                intMenuCount = 0;

                DataSet dsFRF = new DataSet();
                if (Constants.DsFRF_Schema == null)
                {
                    Constants.DsFRF_Schema = DBClass.LoadFRFDataSchema();
                }
                dsFRF = Constants.DsFRF_Schema;
                // Binding the Country,District,State,Region dropdowns
                CountryCollection = (CollectionViewSource)this.FindResource("CountryCollection");
               Countrys = dsFRF.Tables[0];
               CountryCollection.Source = ((IListSource)Countrys).GetList();

                DistrictCollection = (CollectionViewSource)this.FindResource("DistrictCollection");
                District = dsFRF.Tables[3];
                DistrictCollection.Source = ((IListSource)District).GetList();

                StateCollection = (CollectionViewSource)this.FindResource("StateCollection");
                State = dsFRF.Tables[1];
                StateCollection.Source = ((IListSource)State).GetList();

                RegionCollection = (CollectionViewSource)this.FindResource("RegionCollection");
                Region = dsFRF.Tables[2];
                RegionCollection.Source = ((IListSource)Region).GetList();
                ProgressViewer.endProgress();


                BasinCollection = (CollectionViewSource)this.FindResource("BasinCollection");
                Basin = dsFRF.Tables[4];
                BasinCollection.Source = ((IListSource)Basin).GetList();
                ProgressViewer.endProgress();

            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    ProgressViewer.endProgress();
                    MessageBox.Show("Please check your network connectivity");
                }
            }
            catch (Exception ex)
            {
                ProgressViewer.endProgress();
                ex.Message.ToString();
            }

        }
        #endregion

      

      

        #region Bind Field Method
        // Bindind the Default Field Stucture table to Field DataGrid
        public void BindField()
        {

            DataGrid_Field.ItemsSource = new ObservableCollection<ProductCategory>();

        }
        #endregion

        #region Saving Record Method
        // Saving the record in the Backend 
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            ProgressViewer.startProgress(100.0, 100.0);
            DBClass Dbobject = new DBClass();
            ConnectionServices conService = new ConnectionServices();
            clsCon conobj = new clsCon();
            try
            {

                
                DataSet dsState = new DataSet();
                conobj = conService.GetconnectionObj();
                int ival = 0;
                int inul = 0;
                int irows = 0;
                Dbobject.Connectionstring = conobj.Connectionstring;
   
                StructureClass.StrucFRF strData = new StructureClass.StrucFRF();
              
                var row = GetDataGridRows(DataGrid_Field);
                /// go through each row in the datagrid 
                for (int i = 0; i < DataGrid_Field.Items.Count; i++)
                {
                    ProductCategory rowData = DataGrid_Field.Items[i] as ProductCategory;
                   
                    irows = 0;  


                    if (rowData != null)
                    {
                        ival = 1;
                        //FIELD_NAME

                        if (rowData.FIELDNAME != string.Empty && rowData.FIELDNAME != null)
                        {

                            if (rowData.FIELDNAME.Length > 60)
                            {
                                MessageBox.Show("FIELD NAME length cannot be more than 60");
                                return;
                            }
                            else
                            {
                                strData.strFIELD_NAME = rowData.FIELDNAME;
                            }
                            if (rowData.FIELDNAME.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FIELD STATE
                        if (!string.IsNullOrEmpty(rowData.CurrentCategory))
                        {

                            if (rowData.CurrentCategory.Length > 0)
                            {
                                string strState = rowData.CurrentCategory;
                                string[] strStateNo = strState.Split(new char[] { ' ' });
                                strData.strPROVINCE_STATE_FCXS = strStateNo[0];
                                     
                            }
                            else
                            {
                                strData.strPROVINCE_STATE_FCXS = string.Empty;
                            }
     
                        }

                        ////FIELD COUNTY

                        if(!string.IsNullOrEmpty(rowData.CurrentProduct))
                        {
                            string strState = rowData.CurrentProduct;
                            string[] strStateNo = strState.Split(new char[] { ' ' });
                            strData.strCOUNTY = strStateNo[0] ;

                         if (rowData.CurrentProduct.Length > 0)
                          {
                            inul = 1;
                            irows = 1;
                          }
                        }


                        ////FIELD DISTRICT
                        if (rowData.DISTRICT != string.Empty && rowData.DISTRICT != null)
                        {

  
                                string strState = rowData.DISTRICT;
                                string[] strStateNo = strState.Split(new char[] { ' ' });
                                strData.strDISTRICT = strStateNo[2];
                            
                            if (rowData.DISTRICT.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }

                        }
                        ////FIELD MMS
                        if (rowData.MMSFieldName != string.Empty && rowData.MMSFieldName != null)
                        {
                            if (rowData.MMSFieldName.Length > 12)
                            {
                                MessageBox.Show("MMS length cannot be more than 12");
                                return;
                            }
                            else
                            {
                                strData.strFIELD_ALIAS_FA = rowData.MMSFieldName;
                            }
                            if (rowData.MMSFieldName.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FIELD SOURCE
                        if (rowData.MMS_SOURCE != string.Empty && rowData.MMS_SOURCE != null)
                        {

                            if (rowData.MMS_SOURCE == "PI" || rowData.MMS_SOURCE == "MMS")
                            {
                                strData.strSOURCE_MMS_FA = rowData.MMS_SOURCE;
                            }
                            if (rowData.MMS_SOURCE.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FIELD TEMP GRADIENT
                        if (rowData.TempGradient != string.Empty && rowData.TempGradient != null)
                        {
                            if (rowData.TempGradient != string.Empty && rowData.TempGradient != null)
                            {
                                string cellValue = rowData.TempGradient;
                                if (cellValue.Length > 9)
                                {
                                    MessageBox.Show("TempGradient length cannot be more than 9");
                                    return;
                                }
                                else
                                {
                                    if (!floatEx.IsMatch(cellValue))
                                    {
                                        MessageBox.Show("TempGradient allows Only Decimal Numbers");
                                        return;
                                    }
                                    else
                                    {
                                        string[] strTemp = cellValue.Split('.');
                                        if (strTemp[0].Length > 6)
                                        {
                                            MessageBox.Show("TempGradient allows only 6 digits number with 2 decimal number");
                                            return;
                                        }
                                        else if (strTemp.Length == 2)
                                        {
                                            if (strTemp[1].Length > 2)
                                            {
                                                MessageBox.Show("TempGradient allows only 6 digits number with 2 decimal number");
                                                return;
                                            }
                                            else
                                            {
                                                strData.strPI_TEMP_GRADIENT = rowData.TempGradient;
                                            }
                                        }
                                        else
                                        {
                                            strData.strPI_TEMP_GRADIENT = rowData.TempGradient;
                                        }

                                    }
                                }
                            }
                            if (rowData.TempGradient.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FIELD STATE CODE
                        if (rowData.FieldStateCode != string.Empty && rowData.FieldStateCode != null)
                        {
                            if (rowData.FieldStateCode.Length > 12)
                            {
                                MessageBox.Show("FIELD STATE CODE length cannot be more than 12");
                                return;
                            }
                            else
                            {
                                strData.strFIELD_ALIAS_FCXS = rowData.FieldStateCode;
                            }
                            if (rowData.FieldStateCode.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FIELD ALT STATE
                        if (rowData.AltState != string.Empty && rowData.AltState != null)
                        {

                            if (rowData.AltState.Length > 12)
                            {
                                MessageBox.Show("ALT STATE length cannot be more than 12");
                                return;
                            }
                            else
                            {
                                strData.strPROVINCE_STATE_FIELD = rowData.AltState;
                            }
                            if (rowData.AltState.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FIELD ID
                        if (rowData.StandardCode != string.Empty && rowData.StandardCode != null)
                        {
                            if (rowData.StandardCode != string.Empty && rowData.StandardCode != null)
                            {
                                if (rowData.StandardCode.Length > 12)
                                {
                                    MessageBox.Show("Field Standard Code length cannot be more than 12");
                                    return;
                                }
                                else
                                {
                                    if (!numberEx.IsMatch(rowData.StandardCode))
                                    {
                                        MessageBox.Show("Field Standard Code should be numeric");
                                        return;
                                    }
                                    else
                                    {
                                        strData.intFIELD_ID = rowData.StandardCode;
                                    }
                                }
                            }
                            if (rowData.StandardCode.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }


                        ////FIELD COMMENTS

                        if (rowData.Comments_F != string.Empty && rowData.Comments_F != null)
                        {
                            if (rowData.Comments_F.Length > 240)
                            {
                                MessageBox.Show("FIELD COMMENTS Length cannot be more than 240");
                                return;
                            }
                            else
                            {
                                strData.strREMARK_FIELD = rowData.Comments_F;
                            }
                            if (rowData.Comments_F.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////POOL ID 
                        if (rowData.Pool_ID != string.Empty && rowData.Pool_ID != null)
                        {
                            if (rowData.Pool_ID != string.Empty && rowData.Pool_ID != null)
                            {
                                if (rowData.Pool_ID.Length > 12)
                                {
                                    MessageBox.Show("POOL ID Length cannot be more than 12");
                                    return;
                                }
                                else
                                {
                                    if (!numberEx.IsMatch(rowData.Pool_ID))
                                    {
                                        MessageBox.Show("POOL ID should be numeric");
                                        return;
                                    }
                                    else
                                    {
                                        strData.intPOOL_ID = rowData.Pool_ID;
                                    }
                                }
                            }
                            if (rowData.Pool_ID.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FIELD ID 
                        if (rowData.Field_ID != string.Empty && rowData.Field_ID != null)
                        {
                            if (rowData.Field_ID != string.Empty && rowData.Field_ID != null)
                            {
                                if (rowData.Field_ID.Length > 12)
                                {
                                    MessageBox.Show("FIELD ID length cannot be more than 12");
                                    return;
                                }
                                else
                                {
                                    if (!numberEx.IsMatch(rowData.Field_ID))
                                    {
                                        MessageBox.Show("FIELD ID should be numeric");
                                        return;
                                    }
                                    else
                                    {
                                        if (rowData.StandardCode != rowData.Field_ID && !string.IsNullOrEmpty(rowData.StandardCode))
                                        {
                                            MessageBox.Show("StandardCode of Field and Field_ID of Pool must be same");
                                            return;
                                        }
                                        strData.intFIELD_ID_P = rowData.Field_ID;
                                    }
                                }
                            }
                            if (rowData.Field_ID.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////POOL STATE
                        if (rowData.State_p != string.Empty && rowData.State_p != null)
                        {

                            if (rowData.State_p.Length > 12)
                            {
                                MessageBox.Show("POOL STATE length cannot be more than 12");
                                return;
                            }
                            else
                            {
                                strData.strPROVINCE_STATE_POOL = rowData.State_p;
                            }

                            if (rowData.State_p.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FORM ID

                        if (rowData.Form_ID != string.Empty && rowData.Form_ID != null)
                        {
                            if (rowData.Form_ID.Length > 20)
                            {
                                MessageBox.Show("FORM ID length cannot be more than 20");
                                return;
                            }
                            else
                            {
                                if (!numberEx.IsMatch(rowData.Form_ID))
                                {
                                    MessageBox.Show("FORM ID should be numeric");
                                    return;
                                }
                                else
                                {
                                    strData.intFORM_ID = rowData.Form_ID;
                                }
                            }
                            if (rowData.Form_ID.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }

                        }
                       

                        ////POOL REMARKS
                        if (rowData.Comments_P != string.Empty && rowData.Comments_P != null)
                        {

                            if (rowData.Comments_P.Length > 240)
                            {
                                MessageBox.Show("POOL REMARKS length cannot be more than 240");
                                return;
                            }
                            else
                            {
                                strData.strREMARKS_POOL = rowData.Comments_P;
                            }
                            if (rowData.Comments_P.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////POOL NAME
                        if (rowData.ReservoirName != string.Empty && rowData.ReservoirName != null)
                        {
                            if (rowData.ReservoirName.Length > 60)
                            {
                                MessageBox.Show("POOL NAME length cannot be more than 60");
                                return;
                            }
                            else
                            {
                                strData.strPOOL_NAME = rowData.ReservoirName;
                            }
                            if (rowData.ReservoirName.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }

                        }
                        ////STATE RSVR CODE
                        if (rowData.StateRSVRCode != string.Empty && rowData.StateRSVRCode != null)
                        {
                            if (rowData.StateRSVRCode.Length > 60)
                            {
                                MessageBox.Show("STATE RSVR CODE length cannot be more than 60");
                                return;
                            }
                            else
                            {
                                strData.strPOOL_ALIAS_PCXS = rowData.StateRSVRCode;
                            }
                            if (rowData.StateRSVRCode.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////Reservoir STATE
                        if (rowData.STATE_NAME != string.Empty && rowData.STATE_NAME != null)
                        {

                            if (rowData.STATE_NAME != string.Empty && rowData.STATE_NAME != null)
                            {
                                string strState = rowData.STATE_NAME;
                                string[] strStateNo = strState.Split(new char[] { ' ' });
                                if (strStateNo[0].Length > 12)
                                {
                                }
                                else
                                {
                                    strData.strPROVINCE_STATE_PCXS = strStateNo[0];
                                }
                            }
                            if (rowData.STATE_NAME.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }


                        ////POOL ID
                        if (rowData.StandardCode_R != string.Empty && rowData.StandardCode_R != null)
                        {

                            if (rowData.StandardCode_R != string.Empty && rowData.StandardCode_R != null)
                            {
                                if (rowData.StandardCode_R.Length > 12)
                                {
                                    MessageBox.Show("Reservoir StandardCode length cannot be more than 12");
                                    return;
                                }
                                else
                                {
                                    if (!numberEx.IsMatch(rowData.StandardCode_R))
                                    {
                                        MessageBox.Show("Reservoir StandardCode should be Numeric");
                                        return;
                                    }
                                    else
                                    {
                                        if (rowData.Pool_ID != rowData.StandardCode_R && !string.IsNullOrEmpty(rowData.Pool_ID))
                                        {
                                            MessageBox.Show("Pool_ID of Pool and StandardCode of Reservoir must be same");
                                            return;
                                        }
                                        strData.intPOOL_ID_R = rowData.StandardCode_R;
                                    }
                                }
                            }
                            if (rowData.StandardCode_R.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FORMATION NAME

                        if (rowData.FormationName != string.Empty && rowData.FormationName != null)
                        {

                            if (rowData.FormationName.Length > 60)
                            {
                                MessageBox.Show("NAME length cannot be more than 60");
                                return;
                            }
                            else
                            {
                                strData.strFORM_NAME = rowData.FormationName;
                            }
                            if (rowData.FormationName.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FORMATION API
                        if (rowData.API != string.Empty && rowData.API != null)
                        {
                            if (rowData.API.Length > 50)
                            {
                                MessageBox.Show("API length cannot be more than 50");
                                return;
                            }
                            else
                            {
                                strData.intAPI = rowData.API;
                            }
                            if (rowData.API.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }


                        ////FORMATION REGION
                        if (rowData.REGION != string.Empty && rowData.REGION != null)
                        {
                            if (rowData.REGION != string.Empty && rowData.REGION != null)
                            {
                                string strRegion = rowData.REGION;
                                string[] strRegionNo = strRegion.Split(new char[] { ' ' });
                                if (strRegionNo[0].Length > 12)
                                {
                                }
                                else
                                {
                                    strData.strFORM_ALIAS = strRegionNo[0];
                                }
                            }
                            if (rowData.REGION.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FORMATION PERF INTERVAL TOP
                        if (rowData.Top != string.Empty && rowData.Top != null)
                        {
                            if (rowData.Top.Length > 50)
                            {
                                MessageBox.Show("PERF INTERVAL TOP length cannot be more than 50");
                                return;
                            }
                            else
                            {
                                strData.strPERFTOP = rowData.Top;
                            }
                            if (rowData.Top.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FORMATION PERF INTERVAL BOTTOM
                        if (rowData.Bottom != string.Empty && rowData.Bottom != null)
                        {
                            if (rowData.Bottom.Length > 50)
                            {
                                MessageBox.Show("PERF INTERVAL BOTTOM length cannot be more than 50");
                                return;
                            }
                            else
                            {
                                strData.strPERFBOTTOM = rowData.Bottom;
                            }
                            if (rowData.Bottom.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FORMATION TD
                        if (rowData.TD != string.Empty && rowData.TD != null)
                        {
                            if (rowData.TD != string.Empty && rowData.TD != null)
                            {
                                string cellValue1 = rowData.TD;
                                if (cellValue1.Length > 11)
                                {
                                    MessageBox.Show("TD length cannot be more than 11");
                                    return;
                                }
                                else
                                {
                                    if (!floatEx.IsMatch(cellValue1))
                                    {
                                        MessageBox.Show("TD allows Only Decimal Numbers");
                                        return;
                                    }
                                    else
                                    {
                                        string[] strTemp = cellValue1.Split('.');
                                        if (strTemp[0].Length > 5)
                                        {
                                            MessageBox.Show(" TD allows only 5 digits number with 5 decimal number");
                                            return;
                                        }
                                        else if (strTemp.Length == 2)
                                        {
                                            if (strTemp[1].Length > 5)
                                            {
                                                MessageBox.Show("TD allows only 5 digits number with 5 decimal number");
                                                return;
                                            }
                                            else
                                            {
                                                strData.strPI_MD = rowData.TD;
                                            }
                                        }
                                        else if (strTemp.Length == 1)
                                        {
                                            strData.strPI_MD = rowData.TD;
                                        }
                                    }
                                }
                            }
                            if (rowData.TD.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        ////FORMATION GEO AGE

                        if (rowData.GeoAge != string.Empty && rowData.GeoAge != null)
                        {

                            if (rowData.GeoAge.Length > 3 || rowData.GeoAge.Length < 3)
                                {
                                    MessageBox.Show("Geo Age should be 3 Digit Number");
                                    return;
                                }
                                else
                                {
                                    if (!numberEx.IsMatch(rowData.GeoAge))
                                    {
                                        MessageBox.Show("GEO AGE allows Only Numbers");
                                        return;
                                    }
                                    else
                                    {
                                        strData.strFORM_AGE = rowData.GeoAge;
                                    }
                                }
                            
                            if (rowData.GeoAge.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }


                        //FORMATION GEOLOGIC_PROVINCE 
                        if (rowData.GEOLOGIC_PROVINCE != string.Empty && rowData.GEOLOGIC_PROVINCE != null)
                        {


                            string strBasin="";
                            //DataRow[] dv;
                            //dv = Constants.DsFRF_Schema.Tables[4].Select("LONG_NAME = '" + rowData.GEOLOGIC_PROVINCE + "'");
                            //strBasin = dv[0]["GEOLOGIC_PROVINCE"].ToString();

                            DataGridRow row1 = (DataGridRow)DataGrid_Field.ItemContainerGenerator.ContainerFromIndex(i);

                            ComboBox ele = this.DataGrid_Field.Columns[27].GetCellContent(row1) as ComboBox;
                           // if (ele.SelectedValue != null)
                           // {
                                strBasin = ele.SelectedValue.ToString();
                           // }

                                //DBClass.SaveBasin(rowData.GEOLOGIC_PROVINCE).Rows[0]["GEOLOGIC_PROVINCE"].ToString();
                            if (strBasin.Length  > 12)
                            {
                                MessageBox.Show("Basin Length cannot be more than 12");
                                return;
                            }
                            else
                            {
                                strData.strGEOLOGIC_PROVINCE = strBasin;
                            }
                            if (rowData.GEOLOGIC_PROVINCE.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }

                        //FORMATION Comments

                        if (rowData.Comments_FR != string.Empty && rowData.Comments_FR != null)
                        {

                            if (rowData.Comments_FR.Length > 240)
                            {
                                MessageBox.Show("FORMATION Comments Length cannot be more than 240");
                                return;
                            }
                            else
                            {
                                strData.strREMARK = rowData.Comments_FR;
                            }
                            if (rowData.Comments_FR.Length > 0)
                            {
                                inul = 1;
                                irows = 1;
                            }
                        }


                        // }


                        // }

                        if (irows == 1)
                        {
                            Dbobject.Connect();

                            strData.strQuerytype = 4;
                            strData.strOwner = Dbobject.GetDefaultOwner();
                            strData.intRequestOrder = Constants.varRequestOrder;

                            strData.strPI_USER_ID = Constants.User_id;

                            dsState = (DataSet)Dbobject.SpFRF_NEW(ref strData, 2, "FRF");
                            strData = new StructureClass.StrucFRF();
                        }

                        
                    }
                }
                if (ival == 1)
                {
                    ProgressViewer.endProgress();
                    MessageBox.Show("Request Sent Successfully");
                    DataGrid_Field.ItemsSource = null;
                    BindField();
                }
                else
                {
                    ProgressViewer.endProgress();
                    MessageBox.Show("Please enter the details");
                }


                if (intMenuCount == 0)
                {
                    ColumnDisplay(0, 11);
                }
                else if (intMenuCount == 1)
                {
                    ColumnDisplay(11, 16);
                }
                else if (intMenuCount == 2)
                {
                    ColumnDisplay(16, 20);
                }
                else if (intMenuCount == 3)
                {
                    ColumnDisplay(20, 29);
                }
                else
                {
                    ColumnDisplay(0, 29);
                }
            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    ProgressViewer.endProgress();
                    MessageBox.Show("Please check your network connectivity");
                }
            }
            catch (Exception ex)
            {
                ProgressViewer.endProgress();
               // MessageBox.Show(ex.ToString());
            }
            finally
            {
                Dbobject.close();
            }

        }
        #endregion

        #region GetDataGridRows
        public IEnumerable<Microsoft.Windows.Controls.DataGridRow> GetDataGridRows(Microsoft.Windows.Controls.DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;

            if (null == itemsSource) yield return null;

            foreach (var item in itemsSource)
            {

                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as Microsoft.Windows.Controls.DataGridRow;

                if (null != row) yield return row;

            }
        }
        #endregion

        #region DropDowns DataRetrieve Method
        //Retreving the DropDown lists data From backend 
        //private static DataSet LoadData()
        //{
        //    DataSet dsState = new DataSet();
        //    DBClass Dbobject = new DBClass();
        //    try
        //    {
        //        ConnectionServices conService = new ConnectionServices();
        //        clsCon conobj = new clsCon();                
        //        StructureClass.StrucFRF strData = new StructureClass.StrucFRF();

        //        conobj = conService.GetconnectionObj();
        //        Dbobject.Connectionstring = conobj.Connectionstring;

        //        Dbobject.Connect();
        //        dsState = (DataSet)Dbobject.SpFRF_DataRetreive(ref strData, "FieldReservoirFormation");
        //        Dbobject.close();

        //        return dsState;
        //    }
        //    catch (OracleException ex)
        //    {
        //        if (ex.Code == 12170)
        //        {
        //            MessageBox.Show("Please check your network connectivity");
        //        }
        //        return dsState;
        //    }
        //    catch (Exception ex)
        //    {
        //        return dsState;
        //    }
        //    finally
        //    {
        //        Dbobject.close();
        //    }
        //}
        #endregion

        #region Cells Display Methon
        // Cells Display Method --Based Upon Request cells will visible and Collapse
        private void ColumnDisplay(int intFirst, int intLast)
        {

            for (int i = 0; i < 29; i++)
            {
                DataGrid_Field.Columns[i].Visibility = Visibility.Collapsed;

            }
            for (int i = intFirst; i < intLast; i++)
            {
                DataGrid_Field.Columns[i].Visibility = Visibility.Visible;
            }
          }
        #endregion

        #region BindMenuItemIcon
        private void BindMenuItemIcon(MenuItem mi, MenuItem mnuUnselect)
        {
            try
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

                mi.Icon = iconOpenImage;
                mi.EndInit();


                mi.Background = Brushes.Navy;
                mi.Foreground = Brushes.White;
                mnuUnselect.Background = Brushes.Transparent;

                LinearGradientBrush lgb = new LinearGradientBrush();
                lgb.StartPoint = new Point(0.5, 0);
                lgb.EndPoint = new Point(0.5, 1);
                lgb.GradientStops.Add(new GradientStop(Color.FromRgb(129, 164, 210), 1));
                lgb.GradientStops.Add(new GradientStop(Color.FromRgb(129, 164, 210), 0));
                lgb.GradientStops.Add(new GradientStop(Color.FromRgb(188, 211, 243), 0.5));

                mnuUnselect.Background = lgb;
                mnuUnselect.Foreground = Brushes.Black;
            }
            catch (Exception ex)
            {
            }


        }
        #endregion

        #region Menu Items Events
        //Pool menu Item Click Event
        private void mnuItemPool_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);

            mnuUnselect = (MenuItem)sender;
            lblHeader.Content = "Field Reservoir Formation  - Pool In Field";
            // Showing cells from 12 to 17 in DataGrid(Only Pool In Field items Showing)
            ColumnDisplay(11, 16);
            intMenuCount = 1;
        }
        //Reservoir menu Item Click Event
        private void mnuItemReservoir_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);

            mnuUnselect = (MenuItem)sender;
            lblHeader.Content = "Field Reservoir Formation  - Reservoir";
            // Showing cells from 17 to 21 in DataGrid(Only Reservoir items Showing)
            ColumnDisplay(16, 20);
            intMenuCount = 2;
        }
        //Formation menu Item Click Event
        private void mnuItemFormation_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);

            mnuUnselect = (MenuItem)sender;
            lblHeader.Content = "Field Reservoir Formation  - Formation";
            // Showing cells from 21 to 30 in DataGrid(Only Formation items Showing)
            ColumnDisplay(20, 29);
            intMenuCount = 3;
        }
        //All Menu Click Event
        private void mnuItemAll_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);

            mnuUnselect = (MenuItem)sender;
            lblHeader.Content = "Field Reservoir Formation";
            // Showing all cells 
            ColumnDisplay(0, 29);
            intMenuCount = 4;
        }
        //Field Menu Click Event
        private void mnuItemFiled_Click(object sender, RoutedEventArgs e)
        {
            BindMenuItemIcon((MenuItem)sender, mnuUnselect);

            mnuUnselect = (MenuItem)sender;
            lblHeader.Content = "Field Reservoir Formation  - Field";
            // Showing first 12 cells(Only Field items Showing)
            ColumnDisplay(0, 11);
            intMenuCount = 0;
        }
        #endregion

        #region Cell Validations
        // DataGrid Cell validations 
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
              
            
                if (e.Column.GetType().FullName.ToString().Equals("Microsoft.Windows.Controls.DataGridComboBoxColumn")){                      
                    }                    
               
                else
                {
                if (((System.Windows.Controls.TextBox)(e.EditingElement)).Text != string.Empty || ((System.Windows.Controls.TextBox)(e.EditingElement)).Text != "")
                {

                    // Temp Gradient Validation part : It allows Only 6 digits number with 2 floating number
                    if (e.Column.Header.ToString() == "Temp Gradient")
                    {
                        DataGrid dr = (DataGrid)sender;
                        string cellValue = ((System.Windows.Controls.TextBox)(e.EditingElement)).Text;
                        if (!floatEx.IsMatch(cellValue))
                        {
                            strError = "1";
                            MessageBox.Show("TempGradient allows Only Decimal Numbers");
                            DataGrid_Field.BeginEdit();
                            ((TextBox)(e.EditingElement)).Text = "";
                            ((TextBox)(e.EditingElement)).Focus();
                            ((System.Windows.Controls.TextBox)(e.EditingElement)).Focus();

                            return;

                        }
                        else
                        {
                            string[] strTemp = cellValue.Split('.');
                            if (strTemp[0].Length > 6)
                            {
                                strError = "1";
                                MessageBox.Show("TempGradient allows only 6 digits number with 2 decimal number");
                                ((TextBox)(e.EditingElement)).Text = "";
                                ((TextBox)(e.EditingElement)).Focus();

                            }
                            else if (strTemp.Length == 2)
                            {
                                if (strTemp[1].Length > 2)
                                {
                                    strError = "1";
                                    MessageBox.Show("TempGradient allows only 6 digits number with 2 decimal number");
                                    ((TextBox)(e.EditingElement)).Text = "";
                                    ((TextBox)(e.EditingElement)).Focus();

                                }
                            }
                        }

                    }
                    // Standard Code Validations part : It allows Only Numeric Value
                    if (e.Column.Header.ToString() == "Standard Code")
                    {
                        DataGrid dr = (DataGrid)sender;
                        if (!numberEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                        {
                            strError = "1";
                            MessageBox.Show("Standard Code should be numeric");
                            DataGrid_Field.BeginEdit();
                            ((TextBox)(e.EditingElement)).Text = "";
                            ((TextBox)(e.EditingElement)).Focus();
                            ((System.Windows.Controls.TextBox)(e.EditingElement)).Focus();
                            return;
                        }
                    }
                    // Pool ID Validations part : It allows Only Numeric Value
                    if (e.Column.Header.ToString() == "Pool_ID")
                    {
                        DataGrid dr = (DataGrid)sender;
                        if (!numberEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                        {
                            strError = "1";
                            MessageBox.Show("Pool ID should be numeric", strCaption);
                            DataGrid_Field.BeginEdit();
                            ((TextBox)(e.EditingElement)).Text = "";
                            ((TextBox)(e.EditingElement)).Focus();
                            return;
                        }
                    }
                    // FIELD ID Validations part : It allows Only Numeric Value
                    if (e.Column.Header.ToString() == "Field_ID")
                    {
                        DataGrid dr = (DataGrid)sender;
                        if (!numberEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                        {
                            strError = "1";
                            MessageBox.Show("Field_ID should be numeric");
                            DataGrid_Field.BeginEdit();
                            ((TextBox)(e.EditingElement)).Text = "";
                            ((TextBox)(e.EditingElement)).Focus();
                            return;
                        }
                    }

                    // FORM ID Validations part : It allows Only Numeric Value
                    if (e.Column.Header.ToString() == "Form_ID")
                    {
                        DataGrid dr = (DataGrid)sender;

                        if (!numberEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                        {
                            strError = "1";
                            MessageBox.Show("Form_ID should be numeric");
                            DataGrid_Field.BeginEdit();
                            ((TextBox)(e.EditingElement)).Text = "";
                            ((TextBox)(e.EditingElement)).Focus();

                            return;

                        }
                    }
                    
                   
                    // TD Validation part : It allows Only 5 digits number with 5 floating number
                    if (e.Column.Header.ToString() == "TD")
                    {
                        DataGrid dr = (DataGrid)sender;
                        string cellValue = ((System.Windows.Controls.TextBox)(e.EditingElement)).Text;
                        if (!floatEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                        {
                            strError = "1";
                            MessageBox.Show("TD allows Only Decimal Numbers");
                            DataGrid_Field.BeginEdit();
                            ((TextBox)(e.EditingElement)).Text = "";
                            ((TextBox)(e.EditingElement)).Focus();

                            return;

                        }
                        else
                        {
                            string[] strTemp = cellValue.Split('.');
                            if (strTemp[0].Length > 5)
                            {
                                strError = "1";
                                MessageBox.Show("TD allows only 5 digits number with 5 decimal number");
                                ((TextBox)(e.EditingElement)).Text = "";
                                ((TextBox)(e.EditingElement)).Focus();

                            }
                            else if (strTemp.Length == 2)
                            {

                                if (strTemp[1].Length > 5)
                                {
                                    strError = "1";
                                    MessageBox.Show("TD allows only 5 digits number with 5 decimal number");
                                    ((TextBox)(e.EditingElement)).Text = "";
                                    ((TextBox)(e.EditingElement)).Focus();

                                }
                            }

                        }
                    }
                    // Geo Age Validations part : It allows Only 3 Numeric Value 
                    if (e.Column.Header.ToString() == "Geo Age")
                    {
                        DataGrid dr = (DataGrid)sender;
                        string cellValue = ((System.Windows.Controls.TextBox)(e.EditingElement)).Text;
                        if (!numberEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                        {
                            strError = "1";
                            MessageBox.Show("Geo Age should be Numeric");
                            DataGrid_Field.BeginEdit();
                            ((TextBox)(e.EditingElement)).Text = "";
                            ((TextBox)(e.EditingElement)).Focus();
                            return;
                        }
                        else
                        {
                            if (cellValue.Length != 3)
                            {
                                strError = "1";
                                MessageBox.Show("Geo Age should be 3 Digit Number");
                                DataGrid_Field.BeginEdit();
                                ((TextBox)(e.EditingElement)).Text = "";
                                ((TextBox)(e.EditingElement)).Focus();
                                return;
                            }
                        }
                    }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region Mouse Leave Event
        private void DataGrid_Field_MouseLeave(object sender, MouseEventArgs e)
        {
            // Checking the ERROR Message Box,is it already Popup or not 
            if (strError != "1")
            {
                DataGrid_Field.CommitEdit();
            }
            else
            {
                strError = "0"; ;
            }
        }
        #endregion

        #region GotFocus Event
        // This event used for changing District colmun Isreadonly Property depend upon on the state value 
        private void DataGrid_Field_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                ProductCategory obj = new ProductCategory();
                DataGridRow row = sender as DataGridRow;
                if (((StandardCodes.ProductCategory)(DataGrid_Field.CurrentCell.Item)).CurrentCategory.Equals("42 TEXAS TX"))
                {
                    DataGrid_Field.Columns[3].IsReadOnly = false;
                }
                else
                {
                    ((StandardCodes.ProductCategory)(DataGrid_Field.CurrentCell.Item)).DISTRICT = string.Empty;

                    DataGridColumn comboCol = this.DataGrid_Field.Columns[3];
                    DataGridColumn comboCol1 = this.DataGrid_Field.Columns[1];
                    for (int i = 0; i < this.DataGrid_Field.Items.Count-1; i++)
                    {
                        ComboBox myCmBox1 = (comboCol1.GetCellContent(this.DataGrid_Field.Items[i]) as ComboBox);
                        if(myCmBox1!=null)
                        {
                            if (myCmBox1.SelectedValue.ToString() != "42 TEXAS TX")
                            {
                                ComboBox myCmBox = (comboCol.GetCellContent(this.DataGrid_Field.Items[i]) as ComboBox);
                                if (myCmBox!=null)
                                {
                                    myCmBox.Text = string.Empty;
                                    myCmBox.SelectedValue = string.Empty;
                                }
                            }

                        }
                        else
                        {
                            ComboBox myCmBox = (comboCol.GetCellContent(this.DataGrid_Field.Items[i]) as ComboBox);
                            if (myCmBox!=null)
                            {
                                myCmBox.Text = string.Empty;
                                myCmBox.SelectedValue = string.Empty;
                            }
                        }
     
                    }
                  ((StandardCodes.ProductCategory)(DataGrid_Field.CurrentCell.Item)).DISTRICT = string.Empty;
                   
                   DataGrid_Field.Columns[3].IsReadOnly = true;
                   
                }

            }
            catch (Exception ex)
            {
                DataGrid_Field.Columns[3].IsReadOnly = true;
            }


        }

        #endregion

        private void RdField_Checked(object sender, RoutedEventArgs e)
        {
          //  BindMenuItemIcon((MenuItem)sender, mnuUnselect);

          //  mnuUnselect = (MenuItem)sender;
            lblHeader.Content = "Field Reservoir Formation  - Field";
            // Showing first 12 cells(Only Field items Showing)
            ColumnDisplay(0, 11);
            intMenuCount = 0;
        }

        private void RdPoolInField_Checked(object sender, RoutedEventArgs e)
        {
           // BindMenuItemIcon((MenuItem)sender, mnuUnselect);

           // mnuUnselect = (MenuItem)sender;
            lblHeader.Content = "Field Reservoir Formation  - Pool In Field";
            // Showing cells from 12 to 17 in DataGrid(Only Pool In Field items Showing)
            ColumnDisplay(11, 16);
            intMenuCount = 1;
        }

        private void RdReservoir_Checked(object sender, RoutedEventArgs e)
        {
           // BindMenuItemIcon((MenuItem)sender, mnuUnselect);

          //  mnuUnselect = (MenuItem)sender;
            lblHeader.Content = "Field Reservoir Formation  - Reservoir";
            // Showing cells from 17 to 21 in DataGrid(Only Reservoir items Showing)
            ColumnDisplay(16, 20);
            intMenuCount = 2;
        }

        private void RdFormation_Checked(object sender, RoutedEventArgs e)
        {

           // BindMenuItemIcon((MenuItem)sender, mnuUnselect);

           // mnuUnselect = (MenuItem)sender;
            lblHeader.Content = "Field Reservoir Formation  - Formation";
            // Showing cells from 21 to 30 in DataGrid(Only Formation items Showing)
            ColumnDisplay(20, 29);
            intMenuCount = 3;
        }

        private void RdAll_Checked(object sender, RoutedEventArgs e)
        {
           // BindMenuItemIcon((MenuItem)sender, mnuUnselect);

            //mnuUnselect = (MenuItem)sender;
            lblHeader.Content = "Field Reservoir Formation";
            // Showing all cells 
            ColumnDisplay(0, 29);
            intMenuCount = 4;
        }

    }
}





