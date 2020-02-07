using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Models
{
    public partial class Event
    {
        public HttpPostedFileBase FileImages { get; set; }
        public bool CertificateTemplate { get; set; }
    }
    public partial class EventMobile
    {
        public System.Guid ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public double NormalPrice { get; set; }
        public double HighPrice { get; set; }
        public double OtherPrice { get; set; }
        public string Type { get; set; }
        public int Quota { get; set; }
        public string Images { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    }
}