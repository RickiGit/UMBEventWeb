using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Models.CustomModels
{
    public class RegistrationModel
    {
        public System.Guid ID { get; set; }
        public string IDParticipant { get; set; }
        public string Name { get; set; }
        public string NameEvent { get; set; }
        public System.DateTime RegisterDate { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
    }
}