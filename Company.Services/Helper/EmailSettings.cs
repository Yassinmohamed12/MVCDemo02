using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Company.Services.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            //SMTP => Simple Mail Transfer Protocol => Make You To SendEmail and Receive Email
            var client = new SmtpClient("smtp.gmail.com", 587);

            client.EnableSsl = true;

            //Credentials for need to give the mail that send Mails and his passwords
            client.Credentials = new NetworkCredential("ym9807770@gmail.com", "gyvmlvyjznwzhsai");

            client.Send("ym9807770@gmail.com", email.To, email.Subject,email.Body);

        }
    }
}