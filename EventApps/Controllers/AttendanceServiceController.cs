using EventApps.Helpers;
using EventApps.Models;
using System.Web.Http;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;
using System.Web;

namespace EventApps.Controllers
{
    [RoutePrefix("api/AttendanceService")]
    public class AttendanceServiceController : ApiController
    {
        [Route("CheckInParticipant")]
        [HttpPost]
        public IHttpActionResult CheckInParticipant([FromBody]Attendance attendance)
        {
            AttendanceHelper helper = new AttendanceHelper();
            return Ok(helper.CheckInCandidate(attendance));
        }

        [Route("CheckOutParticipant")]
        [HttpPost]
        public IHttpActionResult CheckOutParticipant([FromBody]Attendance attendance)
        {
            AttendanceHelper helper = new AttendanceHelper();
            return Ok(helper.CheckOutCandidate(attendance));
        }

        [Route("CheckInOutStatus")]
        [HttpGet]
        public IHttpActionResult CheckInOutStatus(string idEvent, string idParticipant)
        {
            AttendanceHelper helper = new AttendanceHelper();
            return Ok(helper.CheckInOutStatus(idEvent, idParticipant));
        }

        [Route("CheckCertificateStatus")]
        [HttpGet]
        public IHttpActionResult CheckCertificateStatus(string idEvent, string idParticipant)
        {
            AttendanceHelper helper = new AttendanceHelper();
            return Ok(helper.CheckCertificateStatus(idEvent, idParticipant));
        }

        [Route("DownloadCertificate")]
        [HttpGet]
        public IHttpActionResult DownloadCertificate(string idEvent, string nameParticipant)
        {
            string dataDir = HttpContext.Current.Request.UserHostName;
            Document pdfDocument = new Document(dataDir + "/Images/test.pdf");
            TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber("Name");
            pdfDocument.Pages.Accept(textFragmentAbsorber);
            TextFragmentCollection textFragmentCollection = textFragmentAbsorber.TextFragments;
            foreach (TextFragment textFragment in textFragmentCollection)
            {
                // Update text and other properties
                textFragment.Text = nameParticipant;
            }

            dataDir = dataDir + "/Images/" + idEvent + "_" + nameParticipant + ".pdf";
            pdfDocument.Save(dataDir);

            return Ok();
        }
    }
}