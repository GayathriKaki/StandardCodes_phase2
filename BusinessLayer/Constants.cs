// Created on        Created By
// JULY 2010      Gayathri (16727)

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace StandardCodes.BusinessLayer
{
    public class Constants
    {
       
        public static string Userid;
        public static string User_id;      
        public static string defaultconnStr = "Data Source=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (COMMUNITY = pitcp.world)(PROTOCOL = TCP)(Host = massive.den.ihsenergy.com)(Port = 1525)))(CONNECT_DATA = (SERVICE_NAME = " + System.Configuration.ConfigurationSettings.AppSettings["DatabaseName"] + ")));";
        public static string connectStr; 
        public static string strUserEmail;
        public static Boolean IsOwner;
        public static Boolean IsAdmin;
        public static Boolean IsReadOnly;
        public static bool IsClientSupport;
        public static bool IsCustomerCare;
        public static int varRequestOrder;
        public static StructureClass.StrucLogInData strLogin;
        public static string AdminSchemaName;
        public static DataSet dsFRFTables;

        public static int QAGridWidth=750;
        public static int QAGridHeight = 400;

        public static DataTable DsBA_Service_Type;

        public static DataTable DsBA_State;
        public static DataTable DsBA_Schema;

        public static DataSet DsFRF_Schema;

        public static DataTable DsOwner;



        public static string GetAdminSchemaName()
        {
            if (System.Configuration.ConfigurationSettings.AppSettings["AdminSchemaName"] == "")
                return "";
            else
                return System.Configuration.ConfigurationSettings.AppSettings["AdminSchemaName"] + ".";
        }

        public enum RESULT
        {
            NO_ERROR = 1,
            INSERT_ERROR = 2,
            DELETE_ERROR = 3,
            UPDATE_ERROR = 4,
            OTHER_ERROR = 5,
            LOGIN_SUCCESSFUL = 6,
            LOGIN_UNSUCCESSFUL = 7,
            CONNECTION_SUCCESSFUL = 8,
            CONNECTION_UNSUCCESSFUL = 9


        }
        public static DataSet GetProductsInCategory2(string varState)
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
                strData.strQuerytype = 14;
                string strState = varState;
                string[] strStateNo = strState.Split(new char[] { ' ' });
                strData.strPROVINCE_STATE_FIELD = strStateNo[0];

                Dbobject.Connect();
                dsState = (DataSet)Dbobject.SpFRF_NEW(ref strData, 2, "Products");
                Dbobject.close();
                
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

        public static DataSet GetAllOrdersFromProduct2(string varState)
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
                strData.strQuerytype = 14;
                strData.strPROVINCE_STATE_FIELD = varState;

                Dbobject.Connect();
                dsState = (DataSet)Dbobject.SpFRF_NEW(ref strData, 2, "Orders");
                Dbobject.close();

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


        public static DataSet GetCategories2()
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
                strData.strQuerytype = 15;
                dsState = (DataSet)Dbobject.SpFRF_NEW(ref strData, 2, "Categories");
                Dbobject.close();

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
        public static DataSet LoadData()
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

            catch (Exception ex)
            {
                return dsState;
            }
            finally
            {
                Dbobject.close();
                dsFRFTables = dsState;
            }
        }

    }
}
