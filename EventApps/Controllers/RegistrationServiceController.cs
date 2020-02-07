using EventApps.Helpers;
using EventApps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EventApps.Controllers
{
    [RoutePrefix("api/RegistrationService")]
    public class RegistrationServiceController : ApiController
    {
        [Route("RegistrationEvent")]
        [HttpPost]
        public IHttpActionResult RegistrationEvent([FromBody]Registration registration)
        {
            RegistrationHelper helper = new RegistrationHelper();
            return Ok(helper.RegistrationEvent(registration));
        }

        [Route("StatusRegistration")]
        [HttpGet]
        public IHttpActionResult StatusRegistration(string idParticipant, string idEvent)
        {
            RegistrationHelper helper = new RegistrationHelper();
            return Ok(helper.CheckStatusRegistration(idParticipant, idEvent));
        }

        [Route("GetDetailRegistration")]
        [HttpGet]
        public IHttpActionResult GetDetailRegistration(string idParticipant, string idEvent)
        {
            RegistrationHelper helper = new RegistrationHelper();
            return Ok(helper.GetDetailRegistration(idParticipant, idEvent));
        }
    }
}