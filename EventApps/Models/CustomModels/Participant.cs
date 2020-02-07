using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Models
{
    public partial class Participant
    {
        public string ConfirmPassword { get; set; }
    }

    public partial class ParticipantMobile
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public System.Guid IDType { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public System.DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> Modified { get; set; }
        public string ModifiedBy { get; set; }
    }
}