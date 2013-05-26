using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace ChickenCoopBaseStation
{
    public static class AlertService
    {
        public static bool AlertMe(string message)
        {
            DateTime dateTime = DateTime.Now;
            string fileToWrite = "AutomaticChickenCoop_AlertMessageSent_" + dateTime.Month.ToString() + dateTime.Day.ToString() + dateTime.Year.ToString() + ".txt";
            FileStream dataWriter = null;

            if (File.Exists(fileToWrite))
                return false;

            try
            {
                string subjectOfMessage = "Automatic Chicken Coop Alert!";
                string to = "6125999506@vtext.com";
                //string to = "gregcummines@gmail.com";
                using (MailMessage mailMsg = new MailMessage("gregcummines@gmail.com", to))
                {
                    mailMsg.Subject = subjectOfMessage;
                    mailMsg.IsBodyHtml = false;
                    mailMsg.Body = message;

                    using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        //client.Host = "smtp.gmail.com";
                        client.Credentials = new NetworkCredential("gregcummines@gmail.com", "lr680nq");
                        client.EnableSsl = true;
                        client.Send(mailMsg);

                        if (!File.Exists(fileToWrite))
                        {
                            dataWriter = new FileStream(fileToWrite, FileMode.OpenOrCreate, FileAccess.Write);

                            byte[] data = Encoding.UTF8.GetBytes(subjectOfMessage);
                            dataWriter.Write(data, 0, data.Length);
                            dataWriter.Flush();
                        }
                    }
                }

                to = "6123821854@vtext.com";
                //string to = "gregcummines@gmail.com";
                using (MailMessage mailMsg = new MailMessage("gregcummines@gmail.com", to))
                {
                    mailMsg.Subject = subjectOfMessage;
                    mailMsg.IsBodyHtml = false;
                    mailMsg.Body = message;

                    using (SmtpClient client = new SmtpClient("smtp.gmail.com", 587))
                    {
                        //client.Host = "smtp.gmail.com";
                        client.Credentials = new NetworkCredential("gregcummines@gmail.com", "lr680nq");
                        client.EnableSsl = true;
                        client.Send(mailMsg);
                    }
                }
            }
            catch (SmtpException)
            {
                // handle exception here
            }
            return true;
        }
    }
}
