using EventApps.Helpers;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EventApps.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            var listOfEvent = EventHelper.GetAllListEvent();
            return View(listOfEvent);
        }

        public ActionResult RegistrationEvent(string id)
        {
            var listOfRegistration = RegistrationHelper.GetAllListRegistration(id);
            return View(listOfRegistration);
        }

        public static void Execute()
        {
            MailMessage mailMsg = new MailMessage();

            // To
            mailMsg.To.Add(new MailAddress("fajariyandi.mail@gmail.com", "Ricki"));
            // From
            mailMsg.From = new MailAddress("ricki@altrovis.com", "Fajar");

            // Subject and multipart/alternative Body
            mailMsg.Subject = "subject";
            string text = "text body";
            string html = @"<p>html body</p>";
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            mailMsg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));

            // Init SmtpClient and send
            SmtpClient smtpClient = new SmtpClient("smtp.sendgrid.net", Convert.ToInt32(587));
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential("fajariyandi.mail@gmail.com", "Password0!");
            smtpClient.Credentials = credentials;

            smtpClient.Send(mailMsg);
        }


        public async Task<ActionResult> Verification(string id, string idevent)
        {
            var result = RegistrationHelper.Verification(id);
            if (result)
            {
                Execute();
                TempData["msg"] = "<script>alert('Success verification data');</script>";
                return RedirectToAction("RegistrationEvent", new { id = idevent });
            }
            else
            {
                TempData["msg"] = "<script>alert('Data failed to update');</script>";
                return RedirectToAction("RegistrationEvent", new { id = idevent });
            }
        }
    }
}