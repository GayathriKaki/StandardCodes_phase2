using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.ComponentModel;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using System.Data;
using StandardCodes.BusinessLayer;
using System.Text.RegularExpressions;
using IHS.WPF.UI.ProgressIndicator;

namespace StandardCodes.WebForms
{
    /// Interaction logic for MiscRequest.xaml

    //****************************************************************************************************************************
    //Module Name:  MiscRequest
    //Created By:   Raja Sekhar 
    //Created Date:
    //Modified By: Raja Sekhar
    //Date:        31/Aug/10
    //Modified By: Gayathri
    //Date:        27/Oct/10

    //****************************************************************************************************************************
    public partial class MiscRequest : Page
    {

        #region Private Data
       
             private readonly Regex numberEx = new Regex(@"^[0-9]+$");

        #endregion Private Data

        #region MiscRequest
        public MiscRequest()
        {
            try
            {
                InitializeComponent();
                ProgressViewer.startProgress(100.0, 100.0);

                BindData();
              
                ProgressViewer.endProgress();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        #endregion

       
      

        #region Binding
        //Binding Default MISC table to the DataGrid
        public void BindData()
        {
            ConnectionServices conService = new ConnectionServices();
            clsCon conobj = new clsCon();
            TempdDataSet Dbobject = new TempdDataSet();
            DataSet dsState = new DataSet();
            dsState = (DataSet)Dbobject.GetMISC();
            DataGrid_MISC.ItemsSource = dsState.Tables[0].DefaultView;
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

        #region Save Click Event
        //Record Saving Event
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                ProgressViewer.startProgress(100.0, 100.0);
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                DBClass Dbobject = new DBClass();
                StructureClass.StrucMISCInsert strData = new StructureClass.StrucMISCInsert();
                DataSet dsState = new DataSet();
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                var row = GetDataGridRows(DataGrid_MISC);
                int ival = 0;
                int inul = 0;
                int irows = 0;
                /// go through each row in the datagrid 
                DataRowView miscRows;
                DataRow rowData;
                /// go through each row in the datagrid 
                for (int rowNumner = 0; rowNumner < DataGrid_MISC.Items.Count - 1; rowNumner++)
                {
                    miscRows = DataGrid_MISC.Items[rowNumner] as DataRowView;
                    rowData = miscRows.Row;
                    ival = 1;
                    irows = 0;
                    for (int i = 0; i < DataGrid_MISC.Columns.Count; i++)
                    {
                        switch (i)
                        {
                            // MISC ID
                            case 0:
                                if (rowData[i].ToString() != string.Empty && rowData[i].ToString() != null)
                                {
                                    if (rowData[i].ToString().Trim().Length > 50)
                                    {
                                        MessageBox.Show("The MISC ID Length cannot be more than 50");
                                        return;
                                    }
                                    else
                                    {
                                        if (!numberEx.IsMatch(rowData[i].ToString().Trim()))
                                        {
                                            MessageBox.Show("MISC ID allows Only Numbers");
                                            return;
                                        }
                                        else
                                        {
                                            strData.strBAID = rowData[i].ToString().Trim();
                                        }
                                    }
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                            //Request Description 
                            case 1:
                                if (rowData[i].ToString().Trim().Length > 240)
                                {
                                    MessageBox.Show("The Request Description Length cannot be more than 240");
                                    return;
                                }
                                else
                                {
                                    strData.strRequestDescription = rowData[i].ToString().Trim();
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                            //Remarks
                            case 2:
                                if (rowData[i].ToString().Trim().Length > 240)
                                {
                                    MessageBox.Show("The Remarks Length cannot be more than 240");
                                    return;
                                }
                                else
                                {
                                    strData.strRemarks = rowData[i].ToString().Trim();
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                            //Texas Survey Number
                            case 3:
                                if (rowData[i].ToString().Trim().Length > 200)
                                {
                                    MessageBox.Show("The Texas Survey Number Length cannot be more than 200");
                                    return;
                                }
                                else
                                {
                                    strData.strTEXASSURVEY_NUMBER = rowData[i].ToString().Trim();
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                            // Texas Survey Long Name
                            case 4:
                                if (rowData[i].ToString().Trim().Length > 200)
                                {
                                    MessageBox.Show("The Texas Survey Long Name Length cannot be more than 200");
                                    return;
                                }
                                else
                                {
                                    strData.strTEXASSURVEY_LONGNAME = rowData[i].ToString().Trim();
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                            // Texas Survey Remarks
                            case 5:
                                if (rowData[i].ToString().Trim().Length > 200)
                                {
                                    MessageBox.Show("The Texas Survey Remarks Length cannot be more than 200");
                                    return;
                                }
                                else
                                {
                                    strData.strTEXASSURVEY_REMARKS = rowData[i].ToString().Trim();
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                            //Monument ID
                            case 6:
                                if (rowData[i].ToString().Trim().Length > 200)
                                {
                                    MessageBox.Show("The Monument ID Length cannot be more than 200");
                                    return;
                                }
                                else
                                {
                                    strData.strMONUMENT_ID = rowData[i].ToString().Trim();
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                            //Monument Latitude
                            case 7:
                                if (rowData[i].ToString().Trim().Length > 200)
                                {
                                    MessageBox.Show("The Monument Latitude Length cannot be more than 200");
                                    return;
                                }
                                else
                                {
                                    strData.strMONUMENT_LATITUDE = rowData[i].ToString().Trim();
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                            //Monument Longitude
                            case 8:
                                if (rowData[i].ToString().Trim().Length > 200)
                                {
                                    MessageBox.Show("The Monument Longitude Length cannot be more than 200");
                                    return;
                                }
                                else
                                {
                                    strData.strMONUMENT_LONGITUDE = rowData[i].ToString().Trim();
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                            // Monument Name
                            case 9:
                                if (rowData[i].ToString().Trim().Length > 200)
                                {
                                    MessageBox.Show("The Monument Name Length cannot be more than 200");
                                    return;
                                }
                                else
                                {
                                    strData.strMONUMENT_NAME = rowData[i].ToString().Trim();
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                            //Monumenent Remarks
                            case 10:
                                if (rowData[i].ToString().Trim().Length > 200)
                                {
                                    MessageBox.Show("The Monumenent Remarks Length cannot be more than 200");
                                    return;
                                }
                                else
                                {
                                    strData.strMONUMENT_REMARKS = rowData[i].ToString().Trim();
                                }
                                if (rowData[i].ToString().Trim().Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                        }
                    }
                    if (irows == 1)
                    {
                        Dbobject.Connect();
                        strData.intQuerytype = 4;
                        strData.strOwner = Dbobject.GetDefaultOwner();
                        strData.intRequestOrder = Constants.varRequestOrder;
                        strData.strUserName = Constants.User_id;
                        dsState = (DataSet)Dbobject.SpMISC(ref strData, 2, "MISC");
                        Dbobject.close();
                    }

                }
                if (ival == 1 && inul == 1)
                {
                    ProgressViewer.endProgress();
                    DataGrid_MISC.ItemsSource = null;
                    BindData();
                    MessageBox.Show("Request Sent Successfully");
                }
                else
                {
                    ProgressViewer.endProgress();
                    MessageBox.Show("Please enter the details");
                }
              
            }
            catch (Exception ex)
            {
                ProgressViewer.endProgress();
                MessageBox.Show("The is some problem Updating." + ex.ToString());
            }



        }
        #endregion

        #region Cell Validations

        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            // Validating the MISC ID : It should allows only Numeric values
            try
            {
                if (((System.Windows.Controls.TextBox)(e.EditingElement)).Text != string.Empty || ((System.Windows.Controls.TextBox)(e.EditingElement)).Text != "")
                {
                    if (e.Column.Header.ToString() == "MISC ID")
                    {
                        DataGrid dr = (DataGrid)sender;

                        if (!numberEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                        {
                            MessageBox.Show("MISC ID should be numeric");
                            DataGrid_MISC.BeginEdit();
                            ((TextBox)(e.EditingElement)).Text = "";
                            ((TextBox)(e.EditingElement)).Focus();
                            return;

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }
        #endregion
    }
}
