using System;
using System.Collections.ObjectModel;
using System.Windows.Media;
using System.ComponentModel;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Windows.Data;
using System.IO;
using StandardCodes.BusinessLayer;
using System.Globalization;
using System.Text.RegularExpressions;

namespace StandardCodes
{
    /// Interaction logic for DataSource Class

    //****************************************************************************************************************************
    //Module Name:  DataSource
    //Created By:   Gayathri 
    //Created Date:
    //Modified By: Raja Sekhar
    //Date:        13/Sep/10

    //****************************************************************************************************************************

   
    public class BaSource
    {
    }

    public class Address
    {
        public string City { get; set; }
        public string State { get; set; }
    } 

    public class ColorDataSource
    {
        private ObservableCollection<ColorItem> _colorItems;

        public ObservableCollection<ColorItem> ColorItems
        {
            get { return _colorItems; }
            set { _colorItems = value; }
        }

        public ColorDataSource()
        {
            _colorItems = new ObservableCollection<ColorItem>();
            _colorItems.Add(new ColorItem(Brushes.Transparent));
            _colorItems.Add(new ColorItem(Brushes.Black));
            _colorItems.Add(new ColorItem(Brushes.AliceBlue));
            _colorItems.Add(new ColorItem(Brushes.AntiqueWhite));
            _colorItems.Add(new ColorItem(Brushes.Aqua));
            _colorItems.Add(new ColorItem(Brushes.Aquamarine));
            _colorItems.Add(new ColorItem(Brushes.Azure));
            _colorItems.Add(new ColorItem(Brushes.Beige));
            _colorItems.Add(new ColorItem(Brushes.Bisque));
            _colorItems.Add(new ColorItem(Brushes.BlanchedAlmond));
            _colorItems.Add(new ColorItem(Brushes.Blue));
            _colorItems.Add(new ColorItem(Brushes.BlueViolet));
            _colorItems.Add(new ColorItem(Brushes.Brown));
            _colorItems.Add(new ColorItem(Brushes.BurlyWood));
            _colorItems.Add(new ColorItem(Brushes.CadetBlue));
            _colorItems.Add(new ColorItem(Brushes.Chartreuse));
            _colorItems.Add(new ColorItem(Brushes.Chocolate));
            _colorItems.Add(new ColorItem(Brushes.Coral));
            _colorItems.Add(new ColorItem(Brushes.CornflowerBlue));
            _colorItems.Add(new ColorItem(Brushes.Cornsilk));
            _colorItems.Add(new ColorItem(Brushes.Crimson));
            _colorItems.Add(new ColorItem(Brushes.Cyan));
            _colorItems.Add(new ColorItem(Brushes.DarkBlue));
            _colorItems.Add(new ColorItem(Brushes.DarkCyan));
            _colorItems.Add(new ColorItem(Brushes.DarkGoldenrod));
            _colorItems.Add(new ColorItem(Brushes.DarkGray));
            _colorItems.Add(new ColorItem(Brushes.DarkGreen));
            _colorItems.Add(new ColorItem(Brushes.DarkKhaki));
            _colorItems.Add(new ColorItem(Brushes.DarkMagenta));
            _colorItems.Add(new ColorItem(Brushes.DarkOliveGreen));
            _colorItems.Add(new ColorItem(Brushes.DarkOrange));
        }
    }

    public class ColorItem
    {
        public ColorItem(Brush brush)
        {
            MyBrush = brush;
        }

        public Brush MyBrush { get; set; }
    }

    public class Customers
    {

    }

    public class Employees
    {
   
    }

    public class OrdersDataSource
    {

    }
    public class TempdDataSet
    {

        public DataSet getFieldTable()
        {
            DataTable DTPool = new DataTable();
            DTPool.Columns.Add(new DataColumn("FIELDNAME", typeof(string)));
            DTPool.Columns.Add(new DataColumn("State_F", typeof(string)));
            DTPool.Columns.Add(new DataColumn("COUNTY_NAME", typeof(string)));
            DTPool.Columns.Add(new DataColumn("DISTRICT", typeof(string)));
            DTPool.Columns.Add(new DataColumn("MMSFieldName", typeof(string)));
            DTPool.Columns.Add(new DataColumn("MMS_SOURCE", typeof(string)));
            DTPool.Columns.Add(new DataColumn("TempGradient", typeof(string)));
            DTPool.Columns.Add(new DataColumn("FieldStateCode", typeof(string)));
            DTPool.Columns.Add(new DataColumn("AltState", typeof(string)));
            DTPool.Columns.Add(new DataColumn("StandardCode", typeof(string)));
            DTPool.Columns.Add(new DataColumn("Comments_F", typeof(string)));
            DTPool.Columns.Add(new DataColumn("Pool_ID", typeof(string)));
            DTPool.Columns.Add(new DataColumn("Field_ID", typeof(string)));
            DTPool.Columns.Add(new DataColumn("State_p", typeof(string)));
            DTPool.Columns.Add(new DataColumn("Form_ID", typeof(string)));
            DTPool.Columns.Add(new DataColumn("Comments_P", typeof(string)));
            DTPool.Columns.Add(new DataColumn("ReservoirName", typeof(string)));
            DTPool.Columns.Add(new DataColumn("StateRSVRCode", typeof(string)));
            DTPool.Columns.Add(new DataColumn("STATE_NAME", typeof(string)));
            DTPool.Columns.Add(new DataColumn("StandardCode_R", typeof(string)));
            DTPool.Columns.Add(new DataColumn("FormationName", typeof(string)));
            DTPool.Columns.Add(new DataColumn("API", typeof(string)));
            DTPool.Columns.Add(new DataColumn("REGION", typeof(string)));
            DTPool.Columns.Add(new DataColumn("Top", typeof(string)));
            DTPool.Columns.Add(new DataColumn("Bottom", typeof(string)));
            DTPool.Columns.Add(new DataColumn("TD", typeof(string)));
            DTPool.Columns.Add(new DataColumn("GeoAge", typeof(string)));
            DTPool.Columns.Add(new DataColumn("Basin", typeof(string)));
            DTPool.Columns.Add(new DataColumn("Comments_FR", typeof(string)));
            DTPool.Columns.Add(new DataColumn("ACTIVE_IND", typeof(bool)));
            DataSet ds = new DataSet();
            ds.Tables.Add(DTPool);
            return ds;

        }
        public DataSet GetBA()
        {
            DataSet dsBA = new DataSet();
            DataTable dtTemp = new DataTable();

             dtTemp.Columns.Add(new DataColumn("BACODE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_ID", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_ABBREVIATION", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_CATEGORY", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_CODE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_NAME", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_SHORT_NAME", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_TYPE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("CURRENT_STATUS", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("EFFECTIVE_DATE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("EMAIL_ADDRESS", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("EXPIRY_DATE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("FAX_NUM", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("FIRST_NAME", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("LAST_NAME", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PHONE_NUM", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("WEB_URL", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("MIDDLE_INITIAL", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("ORGANIZATION_BA", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("ORGANZATION_SEQ_NO", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("REMARK", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("SOURCE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PI_USER_ID", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PI_REC_UPD_DATE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PI_ROW_ADD_DATE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("ACTIVE_IND", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("NEW_CODE_REASON", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("NEW_BA_ID", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_ID_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("SOURCE_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("ADDRESS_OBS_NO", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("ADDRESSEE_TEXT", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("FIRST_ADDRESS_LINE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("SECND_ADDRESS_LINE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("THIRD_ADDRESS_LINE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("ADDRESS_TYPE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("CITY", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("COUNTRY", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("EMAIL_ADDRESS_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("FAX_NUM_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("OFFICE_TYPE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PHONE_NUM_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("POSTAL_ZIP_CODE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PROVINCE_STATE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("REMARK_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("WEB_URL_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("WITHHOLDNG_TAX_IND", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PI_REC_UPD_DATE_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PI_USER_ID_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PI_ROW_ADD_DATE_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("ACTIVE_IND_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("EXPIRY_DATE_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("CONTACT_TITLE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("COUNTRY_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PROVINCE_STATE_1", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_ID_ALIAS", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("SOURCE_2", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_SERVICE_TYPE", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_ID_2", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("BA_NAME_ALIAS", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("REMARK_2", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PI_REC_UPD_DATE_2", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PI_USER_ID_2", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("PI_ROW_ADD_DATE_2", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("ACTIVE_IND_2", typeof(string)));
             dtTemp.Columns.Add(new DataColumn("EXPIRY_DATE_2", typeof(string)));       
            //dtTemp.Columns.Add(new DataColumn("BA_ID", typeof(string)));
            //dtTemp.Columns.Add(new DataColumn("RequestDescription", typeof(string)));
            //dtTemp.Columns.Add(new DataColumn("Remarks", typeof(string)));
            //dtTemp.Columns.Add(new DataColumn("TexasSurveyNumber", typeof(string)));
            //dtTemp.Columns.Add(new DataColumn("LongName", typeof(string)));
            //dtTemp.Columns.Add(new DataColumn("TRemarks", typeof(string)));
            //dtTemp.Columns.Add(new DataColumn("MonumentID", typeof(string)));
            //dtTemp.Columns.Add(new DataColumn("MonumentLatitude", typeof(string)));
            //dtTemp.Columns.Add(new DataColumn("MonumentLongitude", typeof(string)));
            //dtTemp.Columns.Add(new DataColumn("MonumentName", typeof(string)));
            //dtTemp.Columns.Add(new DataColumn("MonumentRemarks", typeof(string)));
            dsBA.Tables.Add(dtTemp);
            return dsBA;
        }
        public DataSet GetMISC()
        {
            DataSet dsMISC = new DataSet();
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add(new DataColumn("BA_ID", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("RequestDescription", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("Remarks", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("TexasSurveyNumber", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("LongName", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("TRemarks", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("MonumentID", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("MonumentLatitude", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("MonumentLongitude", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("MonumentName", typeof(string)));
            dtTemp.Columns.Add(new DataColumn("MonumentRemarks", typeof(string)));
            dsMISC.Tables.Add(dtTemp);
            return dsMISC;
        }

    }



    public class Categories
    {
        public DataView GetCategories()
        {
            return Constants.GetCategories2().Tables["Categories"].DefaultView;
        }
        public DataView GetDistrict()
        {
            return Constants.LoadData().Tables[3].DefaultView;
        }
    }

    public class ProductCategory : INotifyPropertyChanged
    {
        private readonly Regex numberEx = new Regex(@"^[0-9]+$");
        private string _FIELDNAME;
        public string FIELDNAME
        {
            get { return _FIELDNAME; }
            set
            {
                _FIELDNAME = value;

            }
        }

        private string _GEOLOGIC_PROVINCE;
        public string GEOLOGIC_PROVINCE
        {
            get { return _GEOLOGIC_PROVINCE; }
            set
            {
                _GEOLOGIC_PROVINCE = value;

            }
        }
        private string _LONG_NAME;
        public string LONG_NAME
        {
            get { return _LONG_NAME; }
            set
            {
                _LONG_NAME = value;

            }
        }


        private string _State_F;
        public string State_F
        {
            get { return _State_F; }
            set
            {
                _State_F = value;

            }
        }

        private string _COUNTY_NAME;
        public string COUNTY_NAME
        {
            get { return _COUNTY_NAME; }
            set
            {
                _COUNTY_NAME = value;

            }
        }

        private string _DISTRICT;
        public string DISTRICT
        {
            get { return _DISTRICT; }
            set
            {
                int intTest = 0;
                if (CurrentCategory.Equals("42 TEXAS TX"))
                {
                    DataTable dt = Constants.dsFRFTables.Tables[3];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray[0].ToString() == value)
                        {
                            intTest = 1;
                        }
                    }
                    if (intTest == 0)
                    {
                        _DISTRICT = "";
                    }
                    else
                    {
                        _DISTRICT = value;
                    }
                }
                else
                {
                    _DISTRICT = "";
                }
                OnPropertyChanged("DISTRICT");
            }


        }

        private string _MMSFieldName;
        public string MMSFieldName
        {
            get { return _MMSFieldName; }
            set
            {
                _MMSFieldName = value;

            }
        }

        private string _MMS_SOURCE;
        public string MMS_SOURCE
        {
            get { return _MMS_SOURCE; }
            set
            {
                _MMS_SOURCE = value;

            }
        }

        private string _TempGradient;
        public string TempGradient
        {
            get { return _TempGradient; }
            set
            {
                _TempGradient = value;

            }
        }

        private string _FieldStateCode;
        public string FieldStateCode
        {
            get { return _FieldStateCode; }
            set
            {
                _FieldStateCode = value;

            }
        }

        private string _AltState;
        public string AltState
        {
            get { return _AltState; }
            set
            {
                _AltState = value;

            }
        }

        private string _StandardCode;
        public string StandardCode
        {
            get { return _StandardCode; }
            set
            {
                _StandardCode = value;

            }
        }

        private string _Comments_F;
        public string Comments_F
        {
            get { return _Comments_F; }
            set
            {
                _Comments_F = value;

            }
        }

        private string _Pool_ID;
        public string Pool_ID
        {
            get { return _Pool_ID; }
            set
            {
                if (!numberEx.IsMatch(value))
                {
                    _Pool_ID = "";
                }
                else
                {
                    _Pool_ID = value;
                }
                

            }
        }

        private string _Field_ID;
        public string Field_ID
        {
            get { return _Field_ID; }
            set
            {
                _Field_ID = value;

            }
        }

        private string _State_p;
        public string State_p
        {
            get { return _State_p; }
            set
            {
                _State_p = value;

            }
        }

        private string _Form_ID;
        public string Form_ID
        {
            get { return _Form_ID; }
            set
            {
                _Form_ID = value;

            }
        }

        private string _Comments_P;
        public string Comments_P
        {
            get { return _Comments_P; }
            set
            {
                _Comments_P = value;

            }
        }

        private string _ReservoirName;
        public string ReservoirName
        {
            get { return _ReservoirName; }
            set
            {
                _ReservoirName = value;

            }
        }


        private string _StateRSVRCode;
        public string StateRSVRCode
        {
            get { return _StateRSVRCode; }
            set
            {
                _StateRSVRCode = value;

            }
        }

        private string _STATE_NAME;
        public string STATE_NAME
        {
            get { return _STATE_NAME; }
            set
            {
                _STATE_NAME = value;

            }
        }

        private string _StandardCode_R;
        public string StandardCode_R
        {
            get { return _StandardCode_R; }
            set
            {
                _StandardCode_R = value;

            }
        }

        private string _FormationName;
        public string FormationName
        {
            get { return _FormationName; }
            set
            {
                _FormationName = value;

            }
        }

        private string _API;
        public string API
        {
            get { return _API; }
            set
            {
                _API = value;

            }
        }

        private string _REGION;
        public string REGION
        {
            get { return _REGION; }
            set
            {
                _REGION = value;

            }
        }

        private string _Top;
        public string Top
        {
            get { return _Top; }
            set
            {
                _Top = value;

            }
        }

        private string _Bottom;
        public string Bottom
        {
            get { return _Bottom; }
            set
            {
                _Bottom = value;

            }
        }

        private string _TD;
        public string TD
        {
            get { return _TD; }
            set
            {
                _TD = value;

            }
        }

        private string _GeoAge;
        public string GeoAge
        {
            get { return _GeoAge; }
            set
            {
                _GeoAge = value;

            }
        }
        private string _Basin;
        public string Basin
        {
            get { return _Basin; }
            set
            {
                _Basin = value;

            }
        }

        private string _Comments_FR;
        public string Comments_FR
        {
            get { return _Comments_FR; }
            set
            {
                _Comments_FR = value;

            }
        }

        private Boolean _ACTIVE_IND;
        public Boolean ACTIVE_IND
        {
            get { return true; }
            set
            {
                _ACTIVE_IND = value;

            }
        }


        private string _currentCategory;
        public string CurrentCategory
        {
            get { return _currentCategory; }
            set
            {
                try
                {
                    DataView dv = (DataView)Constants.dsFRFTables.Tables[1].DefaultView;
                    dv.RowFilter = "STATE_NAME='" + value + "'";
                    if (dv.Count > 0)
                    {
                        _currentCategory = value;
                    }
                    else
                    {
                        _currentCategory = "";
                    }

                    ProductsInCategory = Constants.GetProductsInCategory2(_currentCategory).Tables["Products"].DefaultView;
                    OnPropertyChanged("CurrentCategory");
                }
                catch (Exception ex)
                {
                    _currentCategory = "";
                }
            }
        }

        private string _currentProduct;
        public string CurrentProduct
        {
            get { return _currentProduct; }
            set
            {
                int intTest = 0;
                DataTable dt = (DataTable)_productsInCategory.Table;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i].ItemArray[0].ToString() == value)
                        {
                            intTest = 1;
                        }
                    }
                    if (intTest == 0)
                    {
                        _currentProduct = "";
                    }
                    else
                    {
                        _currentProduct = value;
                    }
               


                OrdersFromProduct = Constants.GetAllOrdersFromProduct2(_currentProduct).Tables["Orders"].DefaultView;
                OnPropertyChanged("CurrentProduct");
            }
        }

        private int _currentOrder;
        public int CurrentOrder
        {
            get { return _currentOrder; }
            set
            {
                _currentOrder = value;
                OnPropertyChanged("CurrentOrder");
            }
        }

        private DataView _productsInCategory;
        public DataView ProductsInCategory
        {
            get { return _productsInCategory; }
            private set
            {
                _productsInCategory = value;
                OnPropertyChanged("ProductsInCategory");
            }
        }

        private DataView _ordersFromProduct;
        public DataView OrdersFromProduct
        {
            get { return _ordersFromProduct; }
            private set
            {
                _ordersFromProduct = value;
                OnPropertyChanged("OrdersFromProduct");
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged
    }

    public class DebugConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine(string.Format("Convert called. value: {0}, valueType: {1}, targetType: {2}", value, value != null ? value.GetType() : null, targetType));
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Debug.WriteLine(string.Format("ConvertBack called. value: {0}, valueType: {1}, targetType: {2}", value, value != null ? value.GetType() : null, targetType));
            return value;
        }

        #endregion
    }
}

