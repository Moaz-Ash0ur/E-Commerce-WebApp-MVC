using ECommerceStore.BLL.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Model;
using System.Configuration;
using Task = System.Threading.Tasks.Task;


namespace ECommerceStore.BLL
{
    public class EmailService : IEmailService
    {

        private readonly string _apiKey;
        private readonly string _senderEmail;
        private readonly string _senderName;

        public EmailService(IConfiguration config)
        {
            _apiKey = config["Brevo:ApiKey"]!;
            _senderEmail = config["Brevo:SenderEmail"]!;
            _senderName = config["Brevo:SenderName"]!;
        }

        public async Task SendEmailAsync(string toName, string toEmail, string subject, string htmlContent)
        {
            var config = new sib_api_v3_sdk.Client.Configuration();
            config.AddApiKey("api-key", _apiKey);

            var apiInstance = new TransactionalEmailsApi(config);
            var sender = new SendSmtpEmailSender(_senderName, _senderEmail);
            var recipient = new SendSmtpEmailTo(toEmail, toName);

            try
            {
                var email = new SendSmtpEmail(
                    sender: sender,
                    to: new List<SendSmtpEmailTo> { recipient },
                    subject: subject,
                    htmlContent: htmlContent
                );

                var result = await apiInstance.SendTransacEmailAsync(email);
                Console.WriteLine("Email Sent OK: " + result.ToJson());
            }
            catch (Exception e)
            {
                Console.WriteLine("Email Send Failed: " + e.Message);
            }


            //SendSmtpEmailSender Email = new SendSmtpEmailSender(senderName, senderEmail);
            //SendSmtpEmailTo smtpEmailTo = new SendSmtpEmailTo(toEmail, toName);
            //List<SendSmtpEmailTo> To = new List<SendSmtpEmailTo>();
            //To.Add(smtpEmailTo);


            //try
            //{
            //    var sendSmtpEmail = new SendSmtpEmail(Email, To, null, null, null, htmlContent, subject);
            //    CreateSmtpEmail result = apiInstance.SendTransacEmail(sendSmtpEmail);
            //    Console.WriteLine("Email Sender OK: \n" + result.ToJson());
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("Email Sender Failure: \n" + e.Message);
            //}


        }


    }



}
