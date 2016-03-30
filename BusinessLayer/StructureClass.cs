// Created on        Created By
// July 2010      Gayathri (16727)

// Modified On        Modified By
// 05-Oct-2010      Rajasekhar

// Modified On        Modified By
// 12-Oct-2010      Gayathri
using System;
using System.Collections.Generic;
using System.Text;
using  System.Data;
using System.Data .OracleClient;
using StandardCodes.BusinessLayer;

namespace StandardCodes.BusinessLayer
{
  public class StructureClass
    {
      
        public struct StrucData
        {
            public OracleDataAdapter adapter;
            public DataSet dataset;
        }

        public struct StrucBAInsert
        {
            
            public  string strBAID;
            public  string strFirstName;
            public  string strLastName;
            public  string strPhoneNum;
            public  string strFaxNum;
            public  string strRemark;
            public  string strCity;          
            public  string strProvinceState;
            public  string strPostalZipCode;
            public  string strFirstAddressLine;
            public  string strSecondAddressLine;
            public  string strBAIdAlias;
            public  string strBAShortName;
            public  string strMiddleInitial;
            public  string strBaServiceType;
            public  string strBACode;
            public  string strBAName;
            public  string strBAStateCode;
            public  string strBAUserId;
            public int Querytype;            
            public string BA_SERVICE_TYPE;
            public string TEMP_BAID;
            public DataSet cur_output;
            public string strStandardCode;
            public DateTime strDate_Entered ;
            public string strOwner ;
            public int intRequestOrder;
        }

      

        public struct StrucFRF
        {
            public string strFLD_KEY;
            public string strSTATUS;
            public string strFIELD_NAME;
            public string strDISTRICT;
            public string strPOOL_NAME;
            public string strPROVINCE_STATE_FIELD;
            public string intFIELD_ID;
            public string strREMARK_FIELD;
            public string strPI_TEMP_GRADIENT;
            public string intPOOL_ID;
            public string intFORM_ID;
            public string strFORM_AGE;
            public string strPERFTOP;
            public string strPERFBOTTOM;
            public string strFORM_ALIAS;
            public string strREMARKS_POOL;
            public string strPROVINCE_STATE_PCXS;
            public string strPOOL_ALIAS_PCXS;
            public string strSOURCE_MMS_FA;
            public string strFIELD_ALIAS_FA;
            public string strFIELD_ALIAS_FCXS;
            public string strPROVINCE_STATE_FCXS;
            public string strFORM_NAME;
            public string strREMARK;
            public string intAPI;
            public string strPI_USER_ID;
            public string strGEOLOGIC_PROVINCE;
            public string strPI_MD;
            public string strPROVINCE_STATE_FIC;
            public string strCOUNTY;
            public string strPROVINCE_STATE_POOL;
            public int strQuerytype;
            public string strRequest_Type;
            public DataSet o_remCursor;
            public int intRequestOrder;
            public string strOwner;
            public string intPOOL_ID_R;
            public string intFIELD_ID_P;
         
        }

        public struct StrucMISCInsert
        {
            public string strBAID;
            public string strRequestDescription;
            public string strUserName;
            public string strRemarks;
            public string strREQUEST_TYPE;
            public int intQuerytype;
            public string strTEXASSURVEY_NUMBER;
            public string strTEXASSURVEY_LONGNAME;
            public string strTEXASSURVEY_REMARKS;
            public string strMONUMENT_ID;
            public string strMONUMENT_LATITUDE;
            public string strMONUMENT_LONGITUDE;
            public string strMONUMENT_NAME;
            public string strMONUMENT_REMARKS;
            public DataSet cur_output;
            public int strMISC_KEY;
            public int intRequestOrder;
            public string strOwner;
        }
        public struct StrucQAData
        {
                public string strRequest_Type;
                public string strtable;
                public string o_remCursor;
                public DataSet cur_output;
                public string strBAID;
                public string strFirstName;
                public string strLastName;
                public string strPhoneNum;
                public string strFaxNum;
                public string strRemark;
                public string strCity;
                public string strProvinceState;
                public string strPostalZipCode;
                public string strFirstAddressLine;
                public string strSecondAddressLine;
                public string strBAIdAlias;
                public string strBAShortName;
                public string strMiddleInitial;
                public string strBaServiceType;
                public string strBACode;
                public string strBAName;
                public string strBAStateCode;
                public string strBAUserId;
                public int Querytype;
                public string BA_SERVICE_TYPE;
                public string TEMP_BAID;


               public string  var_DATE_ENTERED ;
               public string  VAR_FIELD_ID;
               public string  VAR_STANDARDCODE;
        }

        public struct StrucLogInData
        {
            public string strUserID;
            public string strUserName;
            public string strRole;
            public DataSet o_remCursor;
            public int QueryType;
            public string strUserPassword;
        }
    }
}
