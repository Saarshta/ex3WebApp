using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ex3WebApp.Models
{
    public class MazeName
    {
        public string Name { get; set; }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}