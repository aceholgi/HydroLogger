using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Net.Mail;


namespace HydroLogger.Code
{
    public static class MailManager
    {
        public static void SendMails()
        {
            if (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Emails] == null)
                return;

            string[] mails = (ConfigurationManager.ConnectionStrings[Constants.ConnectionStrings.Emails] + "").Split('|');

            foreach (string s in mails)
                SendMail(s);
        }

        private static void SendMail(string toAddress)
        {
            MailMessage mail = new MailMessage("wetAlert.com", toAddress)
            {
                Subject = "this is a test email.",
                Body = "this is my test email body"
            };

            SmtpClient client = new SmtpClient
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.gmail.com"
            };

            client.Send(mail);
        }
    }
}