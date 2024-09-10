using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Email
{
    public class EmailSender
    {

        public void SendEmailAsync(string userEmail, string emailSubject, string msg)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress("DUNPSI", "watchforum6@gmail.com"));
            email.To.Add(new MailboxAddress("", userEmail));

            email.Subject = emailSubject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = msg
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                smtp.Authenticate("watchforum6@gmail.com", "zqhp xjwx xoim hnxz");

                smtp.Send(email);

                smtp.Disconnect(true);
            }
        }
    }
}
