using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUberEats
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //const string accountSid = "AC53e1eb902344a22504489db9f99f5c18";
            //const string authToken = "6170b6f5caf58910fa75b2e231e53f06";

            //TwilioClient.Init(accountSid, authToken);

            //GenerarCodigoUberEatsAleatorio();

            CreateHostBuilder(args).Build().Run();
            //Envio().Wait();
        }

        //[JSInvokable]
        //public static void GenerarCodigoUberEatsAleatorio()
        //{
        //    for (int i = 0; i <= 1; i++)
        //    {
        //        var guid = Guid.NewGuid();
        //        var justNumbers = new String(guid.ToString().Where(Char.IsDigit).ToArray());
        //        var seed = int.Parse(justNumbers.Substring(0, 4));

        //        var random = new Random(seed);


        //        var message = MessageResource.Create(
        //           body: ($"{seed}"),
        //           from: new Twilio.Types.PhoneNumber("+12513579706"),
        //           to: new Twilio.Types.PhoneNumber("+51920162111") //nro a remplazar
        //           );
        //         Console.WriteLine(message.Sid);               
        //    }       
        //}

        //static async Task Envio()
        //{
        //    var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("devantryx@gmail.com", "Cliente1");
        //    var subject = "Envio de correo a clientes registrados";
        //    var to = new EmailAddress("devantryx@gmail.com", "Cliente1");
        //    var plainTextContent = "Mensaje";
        //    var htmlContent = "<strong>Mensaje de prueba</strong>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
