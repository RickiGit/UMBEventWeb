using EventApps.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EventApps.Controllers
{
    [RoutePrefix("api/ParticipantTypeService")]
    public class ParticipantTypeServiceController : ApiController
    {
        [Route("GetAllParticipantType")]
        [HttpGet]
        public IHttpActionResult GetAllParticipantType()
        {
            ParticipantTypeHelper helper = new ParticipantTypeHelper();
            return Ok(helper.GetAllListParticipantTypeMobile().ToList());
        }
    }
}