using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace HR_Rerutement.Models
{
    public static class Utilities
    {
        public static void SendMail(String destination, String Subject, string Content)
        {


            var body = Content;
            var message = new MailMessage();
            message.To.Add(new MailAddress(destination));
            message.CC.Add(new MailAddress(ConfigurationManager.AppSettings.Get("CopieEmail")));
            message.From = new MailAddress(ConfigurationManager.AppSettings.Get("gmailAccountAddress"));
            message.Subject = Subject;
            message.Body = body;
            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = ConfigurationManager.AppSettings.Get("gmailAccountAddress"),
                    Password = ConfigurationManager.AppSettings.Get("gmailAccountPassword")
                };

                smtp.UseDefaultCredentials = false;
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;

                try
                {
                    smtp.Send(message);
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex);
                }
            }
        }
    }
}