using Microsoft.AspNetCore.Mvc;
using ResnytskyiLawyer.Models;
using System.Net;
using System.Net.Mail;

namespace ResnytskyiLawyer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApplicationController : Controller
    {
        private const string EmailFrom = "viacheslav.resnytskyi@gmail.com";
        private const string Password = "z2xeEYq49o";

        private const string EmailToUkr = "snitm@ukr.net";
        private const string EmailToRus = "snit@inbox.ru";

        private const string CopyEmailTo = "serbanwot@gmail.com";

        [HttpPost]
        public IActionResult Leave([FromBody] LeaveApplicationRequestModel model)
        {
            var emailTo = model.Phone.StartsWith("+380") ? EmailToUkr : EmailToRus;

            SendEmail(emailTo, model.Phone, model.Description);
            SendEmail(CopyEmailTo, model.Phone, model.Description);

            return Ok();
        }

        private void SendEmail(string emailTo, string phone, string description)
        {
            using (var message = new MailMessage(EmailFrom, emailTo))
            {
                message.Subject = $"Новая заявка с сайта '{phone}'";
                message.Body = $"Номер телефона: {phone}\nОписание: {description}";

                using (var smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;

                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential(EmailFrom, Password);

                    smtp.Port = 587;

                    smtp.Send(message);
                }
            }
        }
    }
}