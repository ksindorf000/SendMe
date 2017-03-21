using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace SendMe.Helpers
{
    public class MailHelper
    {
        public static async Task Execute(string body, string adminName, string to, string stuName, string from, string subj)
        {
            var apiKey = WebConfigurationManager.AppSettings["SendGridApiKey"];
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(from, stuName),
                Subject = subj,
                PlainTextContent = body,
                HtmlContent = "<table><tr>"
                + "<td style=\"max-height: 200px\">"
                + "<a href=\"www.google.com\"><img src=\"https://cmeblogspot.files.wordpress.com/2013/11/welcome-email-header-2.png?w=600\"/></a>"
                + "</td></tr>"
                + "<tr><td>"
                + "<p style=\"text-align: center\"> " + body + "</p>"
                + "</td></tr></table>"
            };
            msg.AddTo(new EmailAddress(to, adminName));
            var response = await client.SendEmailAsync(msg);
        }
    }
}