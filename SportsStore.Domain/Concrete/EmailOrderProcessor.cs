using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace SportsStore.Domain.Concrete
{
    /// <summary>
    /// Settings for sending email.
    /// </summary>
    public class EmailSettings
    {
        public string MailToAddress = "orders@example.com";
        public string MailFromAddress = "sportsstore@example.com";
        public bool UseSsl = true;
        public string Username = "aaa";
        public string Password = "aaa";
        public string ServerName = "aaa@aaa.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true; // don't use the smpt server but write to file instead
        public string FileLocation = @"c:\sports_store_emails";
    }

    /// <summary>
    /// Concrete implementation of an order processor using email.
    /// </summary>
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings EmailSettings
        {
            get;
            set;
        }

        public EmailOrderProcessor(EmailSettings emailSettings)
        {
            EmailSettings = emailSettings;
        }


        public void ProcessOrder(Cart cart, ShippingDetails shippingDetails)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = true;
                smtpClient.Host = EmailSettings.ServerName;
                smtpClient.Port = EmailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(EmailSettings.Username, EmailSettings.Password);

                if (EmailSettings.WriteAsFile == true)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = EmailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been placed")
                    .AppendLine("----")
                    .AppendLine("Items:");

                foreach (var line in cart.Lines)
                {
                    body.AppendLine(
                        String.Format("Product: {0} Quantity: {1} Subtotal: {2}",
                        line.Product.Name,
                        line.Quantity,
                        (line.Product.Price * line.Quantity)));
                }

                body.AppendFormat("Total order value: {0:c}", cart.CountTotalValue())
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingDetails.Name)
                    .AppendLine(shippingDetails.Line1)
                    .AppendLine(shippingDetails.Line2 ?? "")
                    .AppendLine(shippingDetails.Line3 ?? "")
                    .AppendLine(shippingDetails.City)
                    .AppendLine(shippingDetails.State ?? "")
                    .AppendLine(shippingDetails.Country)
                    .AppendLine(shippingDetails.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}",
                    shippingDetails.GiftWrap ? "Yes" : "No");

                MailMessage mailMessage = new MailMessage(
                    EmailSettings.MailFromAddress, // From 
                    EmailSettings.MailToAddress, // To 
                    "New order submitted!", // Subject 
                    body.ToString()); // Body 

                if (EmailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.ASCII;
                }

                smtpClient.Send(mailMessage);
            }
        }
    }
}
