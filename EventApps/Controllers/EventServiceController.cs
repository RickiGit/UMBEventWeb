using Aspose.Pdf;
using Aspose.Pdf.Text;
using EventApps.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace EventApps.Controllers
{
    [RoutePrefix("api/EventService")]
    public class EventServiceController : ApiController
    {
        [Route("GetAllEvents")]
        [HttpGet]
        public IHttpActionResult GetAllEvents()
        {
            EventHelper helper = new EventHelper();
            return Ok(helper.GetAllListEventMobile().ToList());
        }

        [Route("GetAllMyEvents")]
        [HttpGet]
        public IHttpActionResult GetAllMyEvents(string idParticipant)
        {
            EventHelper helper = new EventHelper();
            return Ok(helper.GetAllListMyEventMobile(idParticipant).ToList());
        }

        [Route("GetQuotaAvailable")]
        [HttpGet]
        public IHttpActionResult GetQuotaAvailable(string idEvent)
        {
            EventHelper helper = new EventHelper();
            return Ok(helper.GetQuotaAvailable(idEvent));
        }

        [Route("DownloadCertificate")]
        [HttpGet]
        public IHttpActionResult DownloadCertificate(string idEvent, string nameParticipant)
        {
            string dataDir = "http://localhost:49661/Images/test.pdf";
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(dataDir);
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Stream receiveStream = response.GetResponseStream();

            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(dataDir);
            WebResponse myResp = myReq.GetResponse();

            StreamReader reader = new StreamReader(myResp.GetResponseStream());

            Document pdfDocument = new Document(reader.BaseStream);
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

            return Ok(dataDir);
        }
    }
}