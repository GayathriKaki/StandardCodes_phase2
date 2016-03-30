// Created on        Created By
// JULY 2010      Gayathri (16727)

using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using System.Data.Odbc;
using System.Net;


namespace StandardCodes.SendMail
{
    public class SendMail
    {


        public SendMail()
        {
        }

        // Mail Sending 

        public void MailSend(string strMessageTo, string strMessageFrom, string strMessageSubject, string strMessageBody)
        {
            try
            {
                MailMessage mailMessage = new MailMessage(strMessageFrom, strMessageTo);
                mailMessage.Subject = strMessageSubject;
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = strMessageBody;
                SmtpClient mailSender = new SmtpClient(System.Configuration.ConfigurationSettings.AppSettings["smtpserver"], int.Parse(System.Configuration.ConfigurationSettings.AppSettings["smtpport"])); //use this if you are in the development server
                mailSender.UseDefaultCredentials = false ;
                mailSender.Credentials = new NetworkCredential("gayathri.gollavilli@ihs.com", "Rocket99");              
                mailSender.Send(mailMessage);

            }
            catch (SmtpException ex)
            {
               
            }


        }


    }

}