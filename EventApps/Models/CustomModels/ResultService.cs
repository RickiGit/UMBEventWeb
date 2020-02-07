using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventApps.Models.CustomModels
{
    public class ResultService
    {
        public int result { get; set; }
        public string message { get; set; }

        public ResultService(int result, string message)
        {
            this.result = result;
            this.message = message;
        }
    }
}