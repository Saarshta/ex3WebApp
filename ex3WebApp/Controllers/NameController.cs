using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace ex3.Controllers
{

    public class NameController : ApiController
    {
        private static List<string> names = new List<string>
        {
            "avi", "beni", "dani"
        };
        public JObject GetAllNames() {
            JObject obj = JObject.Parse(JsonConvert.SerializeObject(names));
            return obj;
        }
        [HttpGet]
        public IHttpActionResult GetName(string name)
        {
            
            
            foreach (string itemName in names)
            {
                if (itemName.Equals(name))
                {
                    return Ok(name);
                }
            }
           
                return NotFound();
          
           
        }

        [HttpPost]
        public void AddName(string name)
        {
            names.Add(name);
        }
    }
}
