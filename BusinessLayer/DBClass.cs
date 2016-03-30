// Created on        Created By
// July 2010      Gayathri (16727)

using System;
using System.Collections.Generic;
using System.Text;
using System.Data ;
using System.Data.OracleClient;
using StandardCodes.BusinessLayer;
using System.Web;

/// Interaction logic for DBClass

//****************************************************************************************************************************
//Module Name:  DBClass
//Created By:   Gayathri
//Created Date:
//Modified By: Raja Sekhar
//Date:        05/Oct/10

//****************************************************************************************************************************


namespace StandardCodes.BusinessLayer
{
   public class DBClass : StructureClass
    {
        #region "Data Members"
        //----Local Varibles 
        private OracleCommand objCommand;
        private OracleConnection objConnection;
        private OracleTransaction objTransaction;
        private string objConnectionstring;
      
        #endregion

        public string Connectionstring
        {
            get { return objConnectionstring; }
            set { objConnectionstring = value; }
        }

        public void Connect()
        {
            try
            {
                objConnection = new OracleConnection(objConnectionstring);
                objConnection.Open();
            }
            catch (Exception exp)
            {

            }
        }

        public void  close()
        {
            try
            {

                objConnection.Close();
                objConnection.Dispose();
            }
            catch (Exception exp)
            {

            }
        }

        //---To Execute NonQueryData 
        public void NonQueryData(string strQryText)
        {
            {
                objCommand.CommandType = CommandType.Text;
                objCommand.CommandText = strQryText;
                objCommand.ExecuteNonQuery();
            }
        }

        //---To Execute Command object using Trasaction 
        public void StratTransaction()
        {
            objTransaction = objConnection.BeginTransaction();
            objCommand.Transaction = objTransaction;
        }

        //---To Commit Transaction 
        public void CommitTransaction()
        {
            objTransaction.Commit();
        }

        //---To RollBack Transaction 
        public void RollBackTransaction()
        {
            objTransaction.Rollback();
        }

        //--- Query Data Excuted Dataset result returned 
        public void QueryData(string strQryText, ref DataSet o_dataset)
        {
            try
            {
                StrucData Resultset = default(StrucData);
                DataSet resultDS = default(DataSet);
             
                objCommand.Connection = objConnection;
                {
                    objCommand.CommandType = CommandType.Text;
                    objCommand.CommandText = strQryText;
                }

                //Create a new DataSet 
                Resultset.adapter = new OracleDataAdapter();
                resultDS = new DataSet();

                {
                    // Add a SelectCommand object 
                    Resultset.adapter.SelectCommand = objCommand;
                    // Populate the DataSet with the returned data 
                    Resultset.adapter.Fill(resultDS, "ResultDS");
                }
                // Resultset.adapter = oracleDA 
                Resultset.dataset = resultDS;

                o_dataset = resultDS;
            }
            catch (Exception ex)
            {
                SetErrodata(ex.ToString()); 
            
            }

        }

        public object SetErrodata(string argErr)
        {
            
            //---Setting Error description 
            
           throw new Exception(argErr);
          
        }

        //--------------This Function Generates Param object for given values--------------- 
        //------Param Name 
        //------Param value 
        //------Param Type 
        //------Param Direction 

        private OracleParameter GetParam(string argPName, OracleType argPType, ParameterDirection argParamDirection, object argPvalue)
        {
            //-------Declaring Parmeter Object 
            OracleParameter objParam = default(OracleParameter);
            //---New instance of Parmeter object 
            objParam = new OracleParameter();
            try
            {

               
                //---------Set Param values 
                {
                    objParam.ParameterName = argPName;
                    //--Set parmname 
                    objParam.OracleType = argPType;
                    //--Set Param Type 
                    objParam.Direction = argParamDirection;
                    //-- set param direction 
                    //--Checking Param value 
                    if (objParam.OracleType == OracleType.VarChar & argPvalue == null)
                    {
                        //--set param value 
                        objParam.Value = "";
                    }
                    else
                    {
                        //--set param value 
                        objParam.Value = argPvalue;
                    }

                    if (objParam.OracleType == OracleType.DateTime)
                    {
                        if (argPvalue == null | IsDate(argPvalue) == false)
                        {
                            //--set param value 
                            objParam.Value = DBNull.Value;
                        }
                        else
                        {
                            //--set param value 
                            objParam.Value = argPvalue;
                        }
                    }
                }
                //--Returning Param object 
                
            }
            catch (Exception ex)
            {
                SetErrodata(ex.ToString());
                }

            return objParam;
        }

        public bool IsDate(object inValue)
        {
            bool result;
            try
            {
                DateTime myDT = DateTime.Parse(inValue.ToString());
                result = true;
                return result;
            }
            catch (FormatException e)
            {
                result = false;
                return result;
            }
           
        }


        //---- this Function Executes command in a tansaction 
        public object Executecommand(OracleCommand objCommand, int Exetype, string tbname)
        {
            StrucData Exedataset = default(StrucData);
            try
            {
                //---Starting Transaction 
                objTransaction = objConnection.BeginTransaction();
                objCommand.Transaction = objTransaction;
                if (Exetype == 1) return objCommand;
                //---Executing Command Object 
                Exedataset = (StrucData)ExecuteSelectcmd(ref objCommand, Exetype, tbname);
                //---commit transaction 
                CommitTransaction();
            }
            catch (Exception ex)
            {
                //--When Error occuured 
                RollBackTransaction();
                //----Setting Error message 
                SetErrodata(ex.Message);
            }
            return Exedataset;
        }
        public object ExecutQuery(string qstr)
        {
            DataSet ds = new DataSet();
              
            try
            {
                OracleDataAdapter Da = default(OracleDataAdapter);
                Da = new OracleDataAdapter(qstr, objConnection);
                Da.Fill(ds, "ordreport");
                
            }
            catch (Exception ex)
            {
                SetErrodata(ex.ToString());
            }
            return ds;
        }
        //-----This Function Executes command object and it fills The data set 
        public object ExecuteSelectcmd(ref OracleCommand objCommand, int ExeType, string tbname)
        {
            DataSet resultDS = default(DataSet);
          
                //--Declare varible here 
               
                OracleDataAdapter oracleDA = default(OracleDataAdapter);


                if (ExeType == 1) return objCommand;
                if (ExeType == 101)
                {
                    objCommand.ExecuteNonQuery();
                }
                //---Check connection Status 

                //'If Me.connectionStatus = RESULT.CONNECTION_SUCCESSFUL Then 
                //-- Create a new DataAdapter 
                oracleDA = new OracleDataAdapter();
                //-- Create a new DataSet 
                resultDS = new DataSet();
                //---- Add a SelectCommand object 
                oracleDA.SelectCommand = objCommand;

                // ----Populate the DataSet with the returned data 

                oracleDA.Fill(resultDS, tbname);


         
            return resultDS;

        }

        //----This Function Sets Command Object values 
        public object SetCommandobject(ref OracleCommand o_cmd, string Stprocedurenm)
        {
            //-------Prepare Command Object 
            //-------creat new Command Object 
            o_cmd = new OracleCommand();
            //----set values 
            {
                o_cmd.Connection = objConnection;
                //---set connection 
                o_cmd.CommandText = Stprocedurenm;
                //---set StoreProcedure name 
                //----set Command Type 
                o_cmd.CommandType = CommandType.StoredProcedure;
            }
            return o_cmd;
        }

        public object SpMISC(ref StrucMISCInsert argStrucData, int argExetype, string argTblName)
        {
            //----Declaring Command Object 
            OracleCommand objCommand = default(OracleCommand);
            //-------setting Command Object values 
            objCommand = (OracleCommand)SetCommandobject(ref objCommand, Constants.AdminSchemaName + "MISC_PACK.MISC_Crud");

            {
                //----Add Required Parameters 
                objCommand.Parameters.Add(GetParam("var_BA_ID", OracleType.VarChar, ParameterDirection.Input, argStrucData.strBAID));
                objCommand.Parameters.Add(GetParam("var_REQUEST_DESCRIPTION", OracleType.VarChar, ParameterDirection.Input, argStrucData.strRequestDescription));
                objCommand.Parameters.Add(GetParam("var_USER_NAME", OracleType.VarChar, ParameterDirection.Input, argStrucData.strUserName));
                objCommand.Parameters.Add(GetParam("var_REQUEST_TYPE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strREQUEST_TYPE));
                objCommand.Parameters.Add(GetParam("var_REMARKS", OracleType.VarChar, ParameterDirection.Input, argStrucData.strRemarks));
                objCommand.Parameters.Add(GetParam("var_Querytype", OracleType.Int16, ParameterDirection.Input, argStrucData.intQuerytype));
                objCommand.Parameters.Add(GetParam("var_TEXASSURVEYNUMBER", OracleType.VarChar, ParameterDirection.Input, argStrucData.strTEXASSURVEY_NUMBER));
                objCommand.Parameters.Add(GetParam("var_TEXASSURVEYLONGNAME", OracleType.VarChar, ParameterDirection.Input, argStrucData.strTEXASSURVEY_LONGNAME));
                objCommand.Parameters.Add(GetParam("var_TEXASSURVEYREMARKS", OracleType.VarChar, ParameterDirection.Input, argStrucData.strTEXASSURVEY_REMARKS));
                objCommand.Parameters.Add(GetParam("var_MONUMENTID", OracleType.VarChar, ParameterDirection.Input, argStrucData.strMONUMENT_ID));
                objCommand.Parameters.Add(GetParam("var_MONUMENTLATITUDE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strMONUMENT_LATITUDE));
                objCommand.Parameters.Add(GetParam("var_MONUMENTLONGITUDE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strMONUMENT_LONGITUDE));
                objCommand.Parameters.Add(GetParam("var_MONUMENTNAME", OracleType.VarChar, ParameterDirection.Input, argStrucData.strMONUMENT_NAME));
                objCommand.Parameters.Add(GetParam("var_MONUMENTREMARKS", OracleType.VarChar, ParameterDirection.Input, argStrucData.strMONUMENT_REMARKS));
                objCommand.Parameters.Add(GetParam("o_remCursor", OracleType.Cursor, ParameterDirection.Output, argStrucData.cur_output));
                objCommand.Parameters.Add(GetParam("var_MISC_KEY", OracleType.VarChar, ParameterDirection.Input, argStrucData.strMISC_KEY));
                objCommand.Parameters.Add(GetParam("var_Request_Order", OracleType.Int32, ParameterDirection.Input, argStrucData.intRequestOrder));
                objCommand.Parameters.Add(GetParam("var_Owner", OracleType.VarChar, ParameterDirection.Input, argStrucData.strOwner));


            }
            //----Executing Command Object---- 
            return ExecuteSelectcmd(ref objCommand, argExetype, argTblName);
        }

     


        public object SpQA(ref StrucQAData argStrucData, int argExetype, string argTblName)
        {
            //----Declaring Command Object 
            OracleCommand objCommand = default(OracleCommand);
            //-------setting Command Object values 
            objCommand = (OracleCommand)SetCommandobject(ref objCommand, Constants.AdminSchemaName + "QA_GetRequest_PACK.QA_GetRequest");

            {
                //----Add Required Parameters 
                objCommand.Parameters.Add(GetParam("var_Request_Type", OracleType.VarChar, ParameterDirection.Input, argStrucData.strRequest_Type));
                objCommand.Parameters.Add(GetParam("var_table", OracleType.VarChar, ParameterDirection.Input, argStrucData.strtable));
                objCommand.Parameters.Add(GetParam("o_remCursor", OracleType.Cursor, ParameterDirection.Output, argStrucData.cur_output));


                //BA Fields
                objCommand.Parameters.Add(GetParam("var_BA_ID", OracleType.VarChar, ParameterDirection.Input, argStrucData.strBAID));
                objCommand.Parameters.Add(GetParam("var_FIRST_NAME", OracleType.VarChar, ParameterDirection.Input, argStrucData.strFirstName));
                objCommand.Parameters.Add(GetParam("var_LAST_NAME", OracleType.VarChar, ParameterDirection.Input, argStrucData.strLastName));
                objCommand.Parameters.Add(GetParam("var_PHONE_NUM", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPhoneNum));
                objCommand.Parameters.Add(GetParam("var_FAX_NUM", OracleType.VarChar, ParameterDirection.Input, argStrucData.strFaxNum));
                objCommand.Parameters.Add(GetParam("var_REMARK", OracleType.VarChar, ParameterDirection.Input, argStrucData.strRemark));
                objCommand.Parameters.Add(GetParam("var_CITY", OracleType.VarChar, ParameterDirection.Input, argStrucData.strCity));
                objCommand.Parameters.Add(GetParam("var_PROVINCE_STATE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strProvinceState));
                objCommand.Parameters.Add(GetParam("var_POSTAL_ZIP_CODE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPostalZipCode));
                objCommand.Parameters.Add(GetParam("var_FIRST_ADDRESS_LINE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strFirstName));
                objCommand.Parameters.Add(GetParam("var_SECND_ADDRESS_LINE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strSecondAddressLine));
                objCommand.Parameters.Add(GetParam("var_BA_ID_ALIAS", OracleType.VarChar, ParameterDirection.Input, argStrucData.strBAIdAlias));
                objCommand.Parameters.Add(GetParam("var_BA_SHORTNAME", OracleType.VarChar, ParameterDirection.Input, argStrucData.strBAShortName));
                objCommand.Parameters.Add(GetParam("var_MIDDLE_INITIAL", OracleType.VarChar, ParameterDirection.Input, argStrucData.strMiddleInitial));
                objCommand.Parameters.Add(GetParam("var_BA_SERVICE_TYPE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strBaServiceType));
                objCommand.Parameters.Add(GetParam("var_BA_CODE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strBACode));
                objCommand.Parameters.Add(GetParam("var_BA_NAME", OracleType.VarChar, ParameterDirection.Input, argStrucData.strBAName));
                objCommand.Parameters.Add(GetParam("var_BA_STATECODE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strBAStateCode));
                objCommand.Parameters.Add(GetParam("var_BA_USERID", OracleType.VarChar, ParameterDirection.Input, argStrucData.strBAUserId));
                objCommand.Parameters.Add(GetParam("var_Querytype", OracleType.Int32, ParameterDirection.Input, argStrucData.Querytype));
                objCommand.Parameters.Add(GetParam("var_BA_TYPE", OracleType.VarChar, ParameterDirection.Input, argStrucData.BA_SERVICE_TYPE));
                objCommand.Parameters.Add(GetParam("var_TEMP_BAID", OracleType.VarChar, ParameterDirection.Input, argStrucData.TEMP_BAID));
 
                objCommand.Parameters.Add(GetParam("var_DATE_ENTERED", OracleType.VarChar, ParameterDirection.Input, argStrucData.var_DATE_ENTERED ));
                objCommand.Parameters.Add(GetParam("VAR_FIELD_ID", OracleType.VarChar, ParameterDirection.Input, argStrucData.VAR_FIELD_ID));
                objCommand.Parameters.Add(GetParam("VAR_STANDARDCODE", OracleType.VarChar, ParameterDirection.Input, argStrucData.VAR_STANDARDCODE));
               

            }
            //----Executing Command Object---- 
            return ExecuteSelectcmd(ref objCommand, argExetype, argTblName);
        }

        public object SpBusinessAssociate(ref StrucBAInsert argStrucData, int argExetype, string argTblName)
        {
            //----Declaring Command Object 
            OracleCommand objCommand = default(OracleCommand);
            //-------setting Command Object values 
            objCommand = (OracleCommand)SetCommandobject(ref objCommand, Constants.AdminSchemaName + "BASSOC_PACK.BusinessAssoc_Crud");

            {
                //----Add Required Parameters 
                objCommand.Parameters.Add(GetParam("var_BA_ID", OracleType.VarChar , ParameterDirection.Input, argStrucData.strBAID));
                objCommand.Parameters.Add(GetParam("var_FIRST_NAME", OracleType.VarChar , ParameterDirection.Input, argStrucData.strFirstName));
                objCommand.Parameters.Add(GetParam("var_LAST_NAME", OracleType.VarChar , ParameterDirection.Input, argStrucData.strLastName));
                objCommand.Parameters.Add(GetParam("var_PHONE_NUM", OracleType.VarChar , ParameterDirection.Input, argStrucData.strPhoneNum));
                objCommand.Parameters.Add(GetParam("var_FAX_NUM", OracleType.VarChar , ParameterDirection.Input, argStrucData.strFaxNum));
                objCommand.Parameters.Add(GetParam("var_REMARK", OracleType.VarChar , ParameterDirection.Input, argStrucData.strRemark));
                objCommand.Parameters.Add(GetParam("var_CITY", OracleType.VarChar , ParameterDirection.Input, argStrucData.strCity));
                objCommand.Parameters.Add(GetParam("var_PROVINCE_STATE", OracleType.VarChar , ParameterDirection.Input, argStrucData.strProvinceState));
                objCommand.Parameters.Add(GetParam("var_POSTAL_ZIP_CODE", OracleType.VarChar , ParameterDirection.Input, argStrucData.strPostalZipCode));
                objCommand.Parameters.Add(GetParam("var_FIRST_ADDRESS_LINE", OracleType.VarChar , ParameterDirection.Input, argStrucData.strFirstAddressLine));
                objCommand.Parameters.Add(GetParam("var_SECND_ADDRESS_LINE", OracleType.VarChar , ParameterDirection.Input, argStrucData.strSecondAddressLine));
                objCommand.Parameters.Add(GetParam("var_BA_ID_ALIAS", OracleType.VarChar , ParameterDirection.Input, argStrucData.strBAIdAlias));
                objCommand.Parameters.Add(GetParam("var_BA_SHORTNAME", OracleType.VarChar , ParameterDirection.Input, argStrucData.strBAShortName));
                objCommand.Parameters.Add(GetParam("var_MIDDLE_INITIAL", OracleType.VarChar , ParameterDirection.Input, argStrucData.strMiddleInitial));
                objCommand.Parameters.Add(GetParam("var_BA_SERVICE_TYPE", OracleType.VarChar , ParameterDirection.Input, argStrucData.strBaServiceType));
                objCommand.Parameters.Add(GetParam("var_BA_CODE", OracleType.VarChar , ParameterDirection.Input, argStrucData.strBACode));
                objCommand.Parameters.Add(GetParam("var_BA_NAME", OracleType.VarChar , ParameterDirection.Input, argStrucData.strBAName));
                objCommand.Parameters.Add(GetParam("var_BA_STATECODE", OracleType.VarChar , ParameterDirection.Input, argStrucData.strBAStateCode));
                objCommand.Parameters.Add(GetParam("var_BA_USERID", OracleType.VarChar , ParameterDirection.Input, argStrucData.strBAUserId));
                objCommand.Parameters.Add(GetParam("var_Querytype",OracleType.Int32 , ParameterDirection.Input, argStrucData.Querytype));
                objCommand.Parameters.Add(GetParam("var_BA_TYPE", OracleType.VarChar , ParameterDirection.Input, argStrucData.BA_SERVICE_TYPE));
                objCommand.Parameters.Add(GetParam("var_TEMP_BAID", OracleType.VarChar , ParameterDirection.Input, argStrucData.TEMP_BAID));
                objCommand.Parameters.Add(GetParam("var_StandardCode",OracleType.VarChar  , ParameterDirection.Input, argStrucData.strStandardCode));
                objCommand.Parameters.Add(GetParam("var_Date_Entered", OracleType.DateTime  , ParameterDirection.Input, argStrucData.strDate_Entered));
                objCommand.Parameters.Add(GetParam("var_Owner", OracleType.VarChar , ParameterDirection.Input, argStrucData.strOwner));
                objCommand.Parameters.Add(GetParam("o_remCursor", OracleType.Cursor, ParameterDirection.Output, argStrucData.cur_output));
                objCommand.Parameters.Add(GetParam("var_Request_Order", OracleType.VarChar, ParameterDirection.Input, argStrucData.intRequestOrder.ToString()));


            }
            //----Executing Command Object---- 
            return ExecuteSelectcmd(ref objCommand, argExetype, argTblName);
        }



        public object SpFRF_NEW(ref StrucFRF argStrucData, int argExetype, string argTblName)
        {
           
            //----Declaring Command Object 
            OracleCommand objCommand = default(OracleCommand);
            //-------setting Command Object values 
            objCommand = (OracleCommand)SetCommandobject(ref objCommand, Constants.AdminSchemaName + "FRF_PACKAGE.FieldRF_Crud");

            {
                //----Add Required Parameters 
                objCommand.Parameters.Add(GetParam("var_FLD_KEY", OracleType.VarChar, ParameterDirection.Input, argStrucData.strFLD_KEY));
                objCommand.Parameters.Add(GetParam("var_STATUS", OracleType.VarChar, ParameterDirection.Input, argStrucData.strSTATUS));
                objCommand.Parameters.Add(GetParam("var_FIELD_NAME", OracleType.VarChar, ParameterDirection.Input, argStrucData.strFIELD_NAME));
                objCommand.Parameters.Add(GetParam("var_DISTRICT", OracleType.VarChar, ParameterDirection.Input, argStrucData.strDISTRICT));
                objCommand.Parameters.Add(GetParam("var_POOL_NAME", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPOOL_NAME));
                objCommand.Parameters.Add(GetParam("var_PROVINCE_STATE_FIELD", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPROVINCE_STATE_FIELD));
                objCommand.Parameters.Add(GetParam("var_FIELD_ID", OracleType.VarChar, ParameterDirection.Input, argStrucData.intFIELD_ID));
                objCommand.Parameters.Add(GetParam("var_REMARK_FIELD", OracleType.VarChar, ParameterDirection.Input, argStrucData.strREMARK_FIELD));
                objCommand.Parameters.Add(GetParam("var_PI_TEMP_GRADIENT", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPI_TEMP_GRADIENT));
                objCommand.Parameters.Add(GetParam("var_COUNTRY", OracleType.VarChar, ParameterDirection.Input, argStrucData.strCOUNTY));
                objCommand.Parameters.Add(GetParam("var_POOL_ID", OracleType.VarChar, ParameterDirection.Input, argStrucData.intPOOL_ID));
                objCommand.Parameters.Add(GetParam("var_FORM_ID", OracleType.VarChar, ParameterDirection.Input, argStrucData.intFORM_ID));
                objCommand.Parameters.Add(GetParam("var_FORM_AGE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strFORM_AGE));
                objCommand.Parameters.Add(GetParam("var_PERFTOP", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPERFTOP));
                objCommand.Parameters.Add(GetParam("var_PERFBOTTOM", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPERFBOTTOM));
                objCommand.Parameters.Add(GetParam("var_FORM_ALIAS", OracleType.VarChar, ParameterDirection.Input, argStrucData.strFORM_ALIAS));
                objCommand.Parameters.Add(GetParam("var_REMARKS_POOL", OracleType.VarChar, ParameterDirection.Input, argStrucData.strREMARKS_POOL));
                objCommand.Parameters.Add(GetParam("var_PROVINCE_STATE_PCXS", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPROVINCE_STATE_PCXS));
                objCommand.Parameters.Add(GetParam("var_POOL_ALIAS_PCXS", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPOOL_ALIAS_PCXS));
                objCommand.Parameters.Add(GetParam("var_SOURCE_MMS_FA", OracleType.VarChar, ParameterDirection.Input, argStrucData.strSOURCE_MMS_FA));
                objCommand.Parameters.Add(GetParam("var_FIELD_ALIAS_FA", OracleType.VarChar, ParameterDirection.Input, argStrucData.strFIELD_ALIAS_FA));
                objCommand.Parameters.Add(GetParam("var_FIELD_ALIAS_FCXS", OracleType.VarChar, ParameterDirection.Input, argStrucData.strFIELD_ALIAS_FCXS));
                objCommand.Parameters.Add(GetParam("var_PROVINCE_STATE_FCXS", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPROVINCE_STATE_FCXS));
                objCommand.Parameters.Add(GetParam("var_FORM_NAME", OracleType.VarChar, ParameterDirection.Input, argStrucData.strFORM_NAME));
                objCommand.Parameters.Add(GetParam("var_REMARK", OracleType.VarChar, ParameterDirection.Input, argStrucData.strREMARK));
                objCommand.Parameters.Add(GetParam("var_API", OracleType.VarChar, ParameterDirection.Input, argStrucData.intAPI));
                objCommand.Parameters.Add(GetParam("var_PI_USER_ID", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPI_USER_ID));
                objCommand.Parameters.Add(GetParam("var_GEOLOGIC_PROVINCE", OracleType.VarChar, ParameterDirection.Input, argStrucData.strGEOLOGIC_PROVINCE));
                objCommand.Parameters.Add(GetParam("var_PI_MD", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPI_MD));
                objCommand.Parameters.Add(GetParam("var_PROVINCE_STATE_FIC", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPROVINCE_STATE_FIC));
                objCommand.Parameters.Add(GetParam("var_COUNTY", OracleType.VarChar, ParameterDirection.Input, argStrucData.strCOUNTY));
                objCommand.Parameters.Add(GetParam("var_PROVINCE_STATE_POOL", OracleType.VarChar, ParameterDirection.Input, argStrucData.strPROVINCE_STATE_POOL));
                objCommand.Parameters.Add(GetParam("var_Querytype", OracleType.VarChar, ParameterDirection.Input, argStrucData.strQuerytype));
                objCommand.Parameters.Add(GetParam("var_Request_Type", OracleType.VarChar, ParameterDirection.Input, argStrucData.strRequest_Type));
                objCommand.Parameters.Add(GetParam("o_remCursor", OracleType.Cursor, ParameterDirection.Output, argStrucData.o_remCursor));
                objCommand.Parameters.Add(GetParam("var_Request_Order", OracleType.Int32, ParameterDirection.Input, argStrucData.intRequestOrder));
                objCommand.Parameters.Add(GetParam("var_Owner", OracleType.VarChar, ParameterDirection.Input, argStrucData.strOwner));
                objCommand.Parameters.Add(GetParam("var_FIELD_ID_P", OracleType.VarChar, ParameterDirection.Input, argStrucData.intFIELD_ID_P));
                objCommand.Parameters.Add(GetParam("var_POOL_ID_R", OracleType.VarChar, ParameterDirection.Input, argStrucData.intPOOL_ID_R));


            }
            //----Executing Command Object---- 
            return ExecuteSelectcmd(ref objCommand, argExetype, argTblName);
        }
        public object SpFRF_DataRetreive(ref StrucFRF argStrucData, string argTblName)
        {
            //----Declaring Command Object 
            OracleCommand objCommand = default(OracleCommand);
            //-------setting Command Object values 
            objCommand = (OracleCommand)SetCommandobject(ref objCommand, Constants.AdminSchemaName + "FRF_DataRetreiv_Pack.spFRF_DataRetreiv");

            {
                //----Add Required Parameters 

                objCommand.Parameters.Add(GetParam("o_CountryCursor", OracleType.Cursor, ParameterDirection.Output, argStrucData.o_remCursor));
                objCommand.Parameters.Add(GetParam("o_StateyCursor", OracleType.Cursor, ParameterDirection.Output, argStrucData.o_remCursor));
                objCommand.Parameters.Add(GetParam("o_RegionCursor", OracleType.Cursor, ParameterDirection.Output, argStrucData.o_remCursor));
                objCommand.Parameters.Add(GetParam("o_DistrictCursor", OracleType.Cursor, ParameterDirection.Output, argStrucData.o_remCursor));
                objCommand.Parameters.Add(GetParam("o_BasinCursor", OracleType.Cursor, ParameterDirection.Output, argStrucData.o_remCursor));


            }
            //----Executing Command Object---- 
            return ExecuteSelectcmd(ref objCommand, 4, argTblName);
        }

        public object SpUser_DataRetreive(ref StrucLogInData argStrucData, string argTblName)
        {
            //----Declaring Command Object 
            OracleCommand objCommand = default(OracleCommand);
            //-------setting Command Object values 
            objCommand = (OracleCommand)SetCommandobject(ref objCommand, Constants.AdminSchemaName + "USER_PACK.User_Retrieve");

            {
                //----Add Required Parameters 
                objCommand.Parameters.Add(GetParam("var_UserID", OracleType.VarChar, ParameterDirection.Input, argStrucData.strUserID));
                objCommand.Parameters.Add(GetParam("o_remCursor", OracleType.Cursor, ParameterDirection.Output, argStrucData.o_remCursor));
                objCommand.Parameters.Add(GetParam("var_QueryType", OracleType.VarChar, ParameterDirection.Input, argStrucData.QueryType));
            }

            //----Executing Command Object---- 
            return ExecuteSelectcmd(ref objCommand, 2, argTblName);
        }

        public string GetDefaultOwner()
        {

            string ownerName = "";

            DataSet DsBA = new DataSet();

            ConnectionServices conService = new ConnectionServices();
            clsCon conobj = new clsCon();
            DBClass Dbobject = new DBClass();
            StructureClass.StrucLogInData strData = new StructureClass.StrucLogInData();

            conobj = conService.GetconnectionObj();
            Dbobject.Connectionstring = conobj.Connectionstring;           
            strData.QueryType = 2;

            Dbobject.Connect();
            DsBA = (DataSet)Dbobject.SpUser_DataRetreive(ref strData, "users");
            Dbobject.close();

            if (DsBA.Tables[0].Rows.Count > 0)
            {
                ownerName = DsBA.Tables[0].Rows[0]["OWNER"].ToString();
            }

            return ownerName;

        }

        public static DataTable LoadNamedAges()
        {
            DataSet dsState = new DataSet();
            DBClass Dbobject = new DBClass();
            try
            {
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                StructureClass.StrucBAInsert strData = new StructureClass.StrucBAInsert();

                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strData.Querytype = 11;

                Dbobject.Connect();
                dsState = (DataSet)Dbobject.SpBusinessAssociate(ref strData, 2, "Business_Associate");
                Dbobject.close();
            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    //MessageBox.Show("Please check your network connectivity");
                }
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Problem populating the BA Service Type data.");

            }
            finally
            {
                Dbobject.close();
            }

            return dsState.Tables[0];

        }


        public static DataTable LoadState()
        {
            DataSet dsState = new DataSet();
            DBClass Dbobject = new DBClass();

            try
            {

                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                StructureClass.StrucBAInsert strData = new StructureClass.StrucBAInsert();

                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strData.Querytype = 12;

                Dbobject.Connect();
                dsState = (DataSet)Dbobject.SpBusinessAssociate(ref strData, 2, "Business_Associate");
                Dbobject.close();


            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                   // MessageBox.Show("Please check your network connectivity");
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Problem populating the State.");

            }
            finally
            {
                Dbobject.close();
            }
            return dsState.Tables[0];
        }
        public static DataSet LoadFRFDataSchema()
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
                   // MessageBox.Show("Please check your network connectivity");
                }
                return dsState;
            }
            catch (Exception ex)
            {
                return dsState;
            }
            finally
            {
                Dbobject.close();
            }
        }

        public static DataSet BindDataBASchema()
        {
            DataSet dsState = new DataSet();

            DBClass Dbobject = new DBClass();
            try
            {
                ConnectionServices conService = new ConnectionServices();
                clsCon conobj = new clsCon();
                StructureClass.StrucBAInsert strData = new StructureClass.StrucBAInsert();
              
                conobj = conService.GetconnectionObj();
                Dbobject.Connectionstring = conobj.Connectionstring;
                strData.Querytype = 10;

                Dbobject.Connect();
                dsState = (DataSet)Dbobject.SpBusinessAssociate(ref strData, 2, "Business_Associate");
                Dbobject.close();

               // DataGrid_Standard.ItemsSource = dsState.Tables[0].DefaultView;
            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                   // MessageBox.Show("Please check your network connectivity");
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Problem occured while binding the data.");
            }
            finally
            {
                Dbobject.close();
            }
            return dsState;
        }

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
                   // MessageBox.Show("Please check your network connectivity");
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

        public static DataTable SaveBasin(string geologic_province)
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
                strData.strQuerytype  = 16;
                strData.strGEOLOGIC_PROVINCE = geologic_province;

                Dbobject.Connect();
                dsState = (DataSet)Dbobject.SpFRF_NEW(ref strData, 2, "Business_Associate");
                Dbobject.close();


            }
            catch (OracleException ex)
            {
                if (ex.Code == 12170)
                {
                    // MessageBox.Show("Please check your network connectivity");
                }
            }
            catch (Exception ex)
            {
                //  MessageBox.Show("Problem populating the State.");

            }
            finally
            {
                Dbobject.close();
            }
            return dsState.Tables[0];
        }
    }
}
