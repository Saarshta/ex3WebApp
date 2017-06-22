using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ex3.Models
{
    public class MazeParam
    {

        
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Cols { get; set; }
        public string InitialPos { get; set; }
        public int InitialPosRow { get; set; }
        public int InitialPosCol { get; set; }
        public string GoalPos { get; set; }
        public int GoalPosRow { get; set; }
        public int GoalPosCol { get; set; }
        public string AsString { get; set; }
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }



}