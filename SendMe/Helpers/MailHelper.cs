using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
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
                HtmlContent = PopulateBody(body)
            };
            msg.AddTo(new EmailAddress(to, adminName));
            var response = await client.SendEmailAsync(msg);
        }

        //Create body of html email from html template file
        //https://www.aspsnippets.com/Articles/Send-HTML-Page-File-as-Email-Body-in-ASPNet-using-C-and-VBNet.aspx
        public static string PopulateBody(string message)
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/EmailTemplates/GeneralTemplate.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Message}", message);
            return body;
        }
    }
}