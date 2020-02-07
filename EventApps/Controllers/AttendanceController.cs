using EventApps.Helpers;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventApps.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            var listOfEvent = EventHelper.GetAllListTodayEvent();
            return View(listOfEvent);
        }

        public ActionResult Attendance(Guid id)
        {
            var listOfAttencdance = AttendanceHelper.GetListAttendance(id);
            return View(listOfAttencdance);
        }
        public ActionResult GenerateQRCode(string key)
        {
            var now = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(key + "_" + now,
            QRCodeGenerator.ECCLevel.Q);

            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            return View("QRCode", BitmapToBytes(qrCodeImage));
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}