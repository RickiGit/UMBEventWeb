using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Helpers
{
    public class CertificateHelper
    {
        public static DBEventEntities entities = new DBEventEntities();

        public static Certificate GetAllCertificatesByEvent(string idEvent)
        {
            var id = Guid.Parse(idEvent);
            return entities.Certificates.Where(x => x.IDEvent == id).SingleOrDefault();
        }
        public static bool SaveCertificate(Certificate certificate)
        {
            try
            {
                var items = new Certificate
                {
                    ID = Guid.NewGuid(),
                    IDEvent = certificate.IDEvent,
                    Images = certificate.Images,
                    Created = DateTime.Now,
                    CreatedBy = "System"
                };

                entities.Certificates.Add(items);
                entities.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}