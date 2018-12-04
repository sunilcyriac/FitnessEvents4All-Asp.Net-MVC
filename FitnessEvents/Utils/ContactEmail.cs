using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FitnessEvents.Utils
{
    public class ContactEmail
    
    {
        //code sourced from FIT5032 tutorials
        private const String API_KEY = "APIKEYHERE";

        public void Send(String subject, String contents)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("noreply@localhost.com", "Fitness Events4All");
            var to = new EmailAddress("csunil93@gmail.com");
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = client.SendEmailAsync(msg);
        }

    }
}