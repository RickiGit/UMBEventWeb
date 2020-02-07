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
    [RoutePrefix("api/ParticipantService")]

    public class ParticipantServiceController : ApiController
    {
        [Route("RegistrationParticipant")]
        [HttpPost]
        public IHttpActionResult RegistrationParticipant([FromBody]Participant participant)
        {
            ParticipantHelper helper = new ParticipantHelper();
            return Ok(helper.SaveParticipantMobile(participant));
        }
    }
}