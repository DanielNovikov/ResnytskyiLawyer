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

        [HttpPost]
        public IActionResult Leave([FromBody] LeaveApplicationRequestModel model)
        {
            var emailTo = model.Phone.StartsWith("+380") ? EmailToUkr : EmailToRus;

            using (var message = new MailMessage(EmailFrom, emailTo))
            {
                message.Subject = $"Новая заявка с сайта '{model.Phone}'";
                message.Body = $"Номер телефона: {model.Phone}\nОписание: {model.Description}";

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

            return Ok();
        }
    }
}