// Created on        Created By
// JULY 2010      Gayathri (16727)

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Collections;
using System.ComponentModel;
using Microsoft.Windows.Controls;
using Microsoft.Windows.Controls.Primitives;
using StandardCodes.BusinessLayer;
using System.Data;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using ProgressControls;
using System.Timers;
using IHS.WPF.UI.ProgressIndicator;

namespace StandardCodes.WebForms
{
    /// Interaction logic for BAForms.xaml

    //****************************************************************************************************************************
    //Module Name:  BAForms
    //Created By:   Gayathri 
    //Created Date:
    //Modified By: Raja Sekhar
    //Date:        14/Sep/10

    //****************************************************************************************************************************
    public partial class BAForms : Page
    {
        

        #region Private Data 

            private CollectionViewSource namedAgesCollection;
            public DataTable namedAges { get; private set; }
            private CollectionViewSource stateCollection;
            public DataTable state { get; private set; }
            private readonly Regex numberEx = new Regex(@"^[0-9]+$");

        #endregion Private Data

        public BAForms()
        {

            try
            {

                InitializeComponent();

                ProgressViewer.startProgress(100.0, 100.0);

                BindData();
                if (Constants.DsBA_Service_Type == null)
                {
                    Constants.DsBA_Service_Type = DBClass.LoadNamedAges();
                }

                if (Constants.DsBA_State == null)
                {
                    Constants.DsBA_State = DBClass.LoadState();
                }


                namedAgesCollection = (CollectionViewSource)this.FindResource("NamedAgesCollection");
                namedAges = Constants.DsBA_Service_Type;
                namedAgesCollection.Source = ((IListSource)namedAges).GetList();

                stateCollection = (CollectionViewSource)this.FindResource("StateCollection");
                state = Constants.DsBA_State;
                stateCollection.Source = ((IListSource)state).GetList();

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
                MessageBox.Show("Problem binding the Data.");
            }
            finally
            {
                
            }
        }

      
        // Validation method(Checking it is numeric or not) for Standard code field
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
               
                if (e.Column.Header.ToString() == "Standard Code")
                {
                    DataGrid dr = (DataGrid)sender;
                    if (((System.Windows.Controls.TextBox)(e.EditingElement)).Text != "")
                    {

                        if (!numberEx.IsMatch(((System.Windows.Controls.TextBox)(e.EditingElement)).Text))
                        {
                            MessageBox.Show("Standard Code should be numeric");
                            DataGrid_Standard.BeginEdit();
                            ((TextBox)(e.EditingElement)).Text = "";
                            ((TextBox)(e.EditingElement)).Focus();
                            return;

                        }
                    }

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


        }


       // FIlling BA schema
        public void BindData()
        {
            ConnectionServices conService = new ConnectionServices();
            clsCon conobj = new clsCon();
            TempdDataSet Dbobject = new TempdDataSet();
            DataSet dsState = new DataSet();
            dsState = (DataSet)Dbobject.GetBA();
            DataGrid_Standard.ItemsSource = dsState.Tables[0].DefaultView;        
        }
           //Saving Record
         private void Save_Click(object sender, RoutedEventArgs e)
         {
            DBClass Dbobject = new DBClass();
            try
            {
                ProgressViewer.startProgress(100.0, 100.0);
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                StructureClass.StrucBAInsert strData = new StructureClass.StrucBAInsert();
                DataSet dsState = new DataSet();

                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                string cellheadval;
                int irows = 0;
                int ival = 0;
                int inul = 0;
                var row = GetDataGridRows(DataGrid_Standard);
                /// go through each row in the datagrid 
                foreach (Microsoft.Windows.Controls.DataGridRow r in row)
                {
                    ival = 1;
                    irows = 0;

                    DataRowView rv = (DataRowView)r.Item;
                    foreach (DataGridColumn column in DataGrid_Standard.Columns)
                    {
                        cellheadval = column.Header.ToString();

                        switch (cellheadval)
                        {

                            case "Standard Code":

                                TextBlock cellContent = column.GetCellContent(r) as TextBlock;
                                if (cellContent != null)
                                    strData.strBAID = cellContent.Text;
                                if (cellContent.Text.Length > 20)
                                {
                                    MessageBox.Show("The Standard code Length cannot be more than 20.");
                                    return;
                                }
                                if (cellContent.Text.Length > 0)
                                {
                                    irows = 1;
                                    inul = 1;
                                }
                                break;

                            case "First Name":
                                TextBlock cellContent2 = column.GetCellContent(r) as TextBlock;
                                if (cellContent2 != null)
                                    strData.strFirstName = cellContent2.Text;

                                if (cellContent2.Text.Length > 30)
                                {
                                    MessageBox.Show("First Name Length cannot be more than 30.");
                                    return;
                                }

                                if (cellContent2.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;

                            case "Last Name":
                                TextBlock cellContent3 = column.GetCellContent(r) as TextBlock;
                                if (cellContent3 != null)
                                    strData.strLastName = cellContent3.Text;
                                if (cellContent3.Text.Length > 40)
                                {
                                    MessageBox.Show("Last Name Length cannot be more than 40.");
                                    return;
                                }
                                if (cellContent3.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;

                            case "Middle Name":
                                TextBlock cellContent4 = column.GetCellContent(r) as TextBlock;
                                if (cellContent4 != null)
                                    strData.strMiddleInitial = cellContent4.Text;
                                if (cellContent4.Text.Length > 3)
                                {
                                    MessageBox.Show("Middle Name Length cannot be more than 3.");
                                    return;
                                }
                                if (cellContent4.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;

                            case "Address Line 1":

                                TextBlock cellContent5 = column.GetCellContent(r) as TextBlock;
                                if (cellContent5 != null)
                                    strData.strFirstAddressLine = cellContent5.Text;
                                if (cellContent5.Text.Length > 80)
                                {
                                    MessageBox.Show("Address Line 1 Length cannot be more than 80.");
                                    return;
                                }
                                if (cellContent5.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }

                                break;

                            case "Address Line 2":

                                TextBlock cellContent6 = column.GetCellContent(r) as TextBlock;
                                if (cellContent6 != null)
                                    strData.strSecondAddressLine = cellContent6.Text;

                                if (cellContent6.Text.Length > 80)
                                {
                                    MessageBox.Show("Address Line 2 Length cannot be more than 80.");
                                    return;
                                }
                                if (cellContent6.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }

                                break;

                            case "City":

                                TextBlock cellContent7 = column.GetCellContent(r) as TextBlock;
                                if (cellContent7 != null)
                                    strData.strCity = cellContent7.Text;

                                if (cellContent7.Text.Length > 12)
                                {
                                    MessageBox.Show("City Length cannot be more than 12.");
                                    return;
                                }
                                if (cellContent7.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }

                                break;

                            case "State":

                                ComboBox cellContent8 = column.GetCellContent(r) as ComboBox;
                                if (cellContent8 != null )
                                {
                                    if (cellContent8.SelectedValue!= null)
                                    {
                                        strData.strProvinceState = cellContent8.SelectedValue.ToString();
                                    }
                                }

                                if (cellContent8.Text.Length > 50)
                                {
                                    MessageBox.Show("State Length cannot be more than 50.");
                                    return;
                                }
                                if (cellContent8.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }


                                break;

                            case "Zip Code":

                                TextBlock cellContent9 = column.GetCellContent(r) as TextBlock;
                                if (cellContent9 != null)
                                    strData.strPostalZipCode = cellContent9.Text;
                                if (cellContent9.Text.Length > 12)
                                {
                                    MessageBox.Show("Zip Code Length cannot be more than 12.");
                                    return;
                                }

                                if (cellContent9.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;


                            case "Phone Number":
                                TextBlock cellContent10 = column.GetCellContent(r) as TextBlock;
                                if (cellContent10 != null)
                                    strData.strPhoneNum = cellContent10.Text;
                                if (cellContent10.Text.Length > 30)
                                {
                                    MessageBox.Show("Phone Number Length cannot be more than 30.");
                                    return;
                                }
                                if (cellContent10.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;

                            case "State BA Code":

                                TextBlock cellContent11 = column.GetCellContent(r) as TextBlock;
                                if (cellContent11 != null)
                                    strData.strBAStateCode = cellContent11.Text;
                                if (cellContent11.Text.Length > 12)
                                {
                                    MessageBox.Show("State BA Code Length cannot be more than 12.");
                                    return;
                                }
                                if (cellContent11.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;

                            case "Mnemonic":

                                TextBlock cellContent12 = column.GetCellContent(r) as TextBlock;
                                if (cellContent12 != null)
                                    strData.strBACode = cellContent12.Text;
                                if (cellContent12.Text.Length > 12)
                                {
                                    MessageBox.Show("Mnemonic Length cannot be more than 12.");
                                    return;
                                }
                                if (cellContent12.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;

                            case "BA Short Name":
                                TextBlock cellContent13 = column.GetCellContent(r) as TextBlock;
                                if (cellContent13 != null)
                                    strData.strBAShortName = cellContent13.Text;
                                if (cellContent13.Text.Length > 20)
                                {
                                    MessageBox.Show("BA Short Name Length cannot be more than 20.");
                                    return;
                                }
                                if (cellContent13.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }

                                break;

                            case "Fax":
                                TextBlock cellContent14 = column.GetCellContent(r) as TextBlock;
                                if (cellContent14 != null)
                                    strData.strFaxNum = cellContent14.Text;
                                if (cellContent14.Text.Length > 30)
                                {
                                    MessageBox.Show("Fax Length cannot be more than 30.");
                                    return;
                                }
                                if (cellContent14.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;

                            case "User Name":

                                TextBlock cellContent15 = column.GetCellContent(r) as TextBlock;
                                if (cellContent15 != null)
                                    strData.strBAUserId = cellContent15.Text;
                                if (cellContent15.Text.Length > 30)
                                {
                                    MessageBox.Show("User Name Length cannot be more than 30.");
                                    return;
                                }
                                if (cellContent15.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;

                            case "Comments":

                                TextBlock cellContent16 = column.GetCellContent(r) as TextBlock;
                                if (cellContent16 != null)
                                    strData.strRemark = cellContent16.Text;
                                if (cellContent16.Text.Length > 240)
                                {
                                    MessageBox.Show("Comments Length cannot be more than 240.");
                                    return;
                                }
                                if (cellContent16.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;

                            case "BA ID Alias":

                                TextBlock cellContent17 = column.GetCellContent(r) as TextBlock;
                                if (cellContent17 != null)
                                    strData.strBAIdAlias = cellContent17.Text;
                                if (cellContent17.Text.Length > 20)
                                {
                                    MessageBox.Show("BA ID Alias Length cannot be more than 20.");
                                    return;
                                }
                                if (cellContent17.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;


                            case "BA Service Type":
                                ComboBox cellContent18 = column.GetCellContent(r) as ComboBox;
                                if (cellContent18 != null && cellContent18.Text != null)
                                    strData.strBaServiceType = cellContent18.Text;
                                if (cellContent18.Text.Length > 30)
                                {
                                    MessageBox.Show("BA Service Type Length cannot be more than 30.");
                                    return;
                                }
                                if (cellContent18.Text.Length > 0)
                                {
                                    inul = 1;
                                    irows = 1;
                                }
                                break;
                        }                        
                    }


                    if (irows == 1)
                    {

                        strData.Querytype = 4;
                        strData.strOwner = Dbobject.GetDefaultOwner();
                        strData.intRequestOrder = Constants.varRequestOrder;
                        string strTempBAName = strData.strLastName + strData.strFirstName + strData.strMiddleInitial;
                        if (strTempBAName.Length > 60)
                        {
                            strData.strBAName = strTempBAName.Remove(60, strTempBAName.Length - 60);
                        }
                        else
                        {
                            strData.strBAName = strTempBAName;

                        }
                        strData.strBAUserId = Constants.User_id;

                        Dbobject.Connect();
                        dsState = (DataSet)Dbobject.SpBusinessAssociate(ref strData, 2, "Business_Associate");
                        Dbobject.close();
                    }




                }
                if (ival == 1 && inul == 1)
                {
                    ProgressViewer.endProgress();
                    MessageBox.Show("Request Sent Successfully");
                    BindData();
                }
                else
                {
                  ProgressViewer.endProgress();
                    MessageBox.Show("Please enter the details");
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

               
            }
            finally
            {
                ProgressViewer.endProgress();
                Dbobject.close();
            }

        }

        // Retreving DataGrid Rows
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

       


    }

   
    
}
