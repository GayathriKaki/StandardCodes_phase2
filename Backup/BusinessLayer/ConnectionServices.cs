// Created on        Created By
// July 2010      Gayathri (16727)

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using Microsoft.CSharp;
using System.Data.OracleClient;
using StandardCodes.BusinessLayer;
using System.Configuration;

namespace StandardCodes.BusinessLayer
{

    public class ConnectionServices : DBClass
    {
        public clsCon clsConobj = new clsCon();
      
        public clsCon GetConInfo(string constring)
        {
            //---Returns the PlaneDetails serializable object 
            clsConobj = new clsCon(constring);
            return clsConobj;
        }


        public clsCon GetconnectionObj()
        {
              
                ConnectionServices s = new ConnectionServices();
                clsCon con = new clsCon(); 
                con = s.GetConInfo(Constants.connectStr.ToString());
                return con;            
        } 

        public bool ConnectionStatus(clsCon value)
        {

            try
            {
                DBClass Dbobject = new DBClass();
                Dbobject.Connectionstring = value.Connectionstring;
                Dbobject.Connect();
                return true;
            }
            catch (Exception exp)
            {
                return false;
            }
        }
    }
 // [Serializable()]
    public class clsCon
    {
        //non-default constructor, takes input parameters 
        string strConnection;

        public string Connectionstring
        {
            get { return strConnection; }
            set { strConnection = value; }
        }
        public clsCon(string value)
        {

            strConnection = value;
        }



        public clsCon()
        {

        } 

    }
}
