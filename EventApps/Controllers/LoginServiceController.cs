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
    [RoutePrefix("api/LoginService")]
    public class LoginServiceController : ApiController
    {
        [Route("GetUserAccount")]
        [HttpPost]
        public IHttpActionResult GetUserAccount([FromBody]LoginModel account)
        {
            ParticipantHelper helper = new ParticipantHelper();
            return Ok(helper.GetParticipant(account.Username, account.Password));
        }
    }
}