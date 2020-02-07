using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Packaging;
using EventApps.Helpers;
using EventApps.Models;
using System;
using System.Web.Mvc;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Aspose.Pdf;
using Aspose.Pdf.Text;

namespace EventApps.Controllers
{
    public class CertificateController : Controller
    {
        // GET: Certificate
        public ActionResult Index()
        {
            var listOfEvent = EventHelper.GetAllEventCertificate();

            for(var i = 0; i < listOfEvent.Count; i++)
            {
                var certificate = CertificateHelper.GetAllCertificatesByEvent(listOfEvent[i].ID.ToString());
                if(certificate != null)
                {
                    listOfEvent[i].CertificateTemplate = true;
                }
            }
            return View(listOfEvent);
        }

        public ActionResult Create(string ID)
        {
            ViewBag.IDEvent = ID;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Certificate certificate)
        {
            try
            {
                var FileName = "";

                if (certificate.FileTemplate != null)
                {
                    FileName = System.IO.Path.GetFileNameWithoutExtension(certificate.FileTemplate.FileName);
                    string FileExtension = System.IO.Path.GetExtension(certificate.FileTemplate.FileName);
                    FileName = FileName.Trim() + FileExtension;
                    certificate.Images = FileName;
                    string UploadPath = Server.MapPath("~/Images/");
                    certificate.FileTemplate.SaveAs(UploadPath + FileName);
                }

                var result = CertificateHelper.SaveCertificate(certificate);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.IDEvent = certificate.IDEvent;
                    TempData["msg"] = "<script>alert('Failed to insert data');</script>";
                    return View("Create");
                }
            }
            catch(Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult DownloadCertificate()
        {
            string dataDir = Server.MapPath("~/Images/test.pdf");

            // Open document
            Document pdfDocument = new Document(dataDir);

            // Create TextAbsorber object to find all instances of the input search phrase
            TextFragmentAbsorber textFragmentAbsorber = new TextFragmentAbsorber("Name");

            // Accept the absorber for all the pages
            pdfDocument.Pages.Accept(textFragmentAbsorber);

            // Get the extracted text fragments
            TextFragmentCollection textFragmentCollection = textFragmentAbsorber.TextFragments;

            // Loop through the fragments
            foreach (TextFragment textFragment in textFragmentCollection)
            {
                // Update text and other properties
                textFragment.Text = "HIYA HIYA HIYA";
            }

            dataDir = dataDir + "test3.pdf";
            // Save resulting PDF document.
            pdfDocument.Save(dataDir);

            return View();
        }
    }
}