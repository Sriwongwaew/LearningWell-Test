using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningWell.Models
{
    public class CountyData
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 

        public string Title { get; set; }
        public List<Variable> Variables { get; set; }

    }
    public class Variable
    {
        public string Code { get; set; }
        public string Text { get; set; }
        [JsonProperty("Values")]
        public List<string> Values { get; set; }
        [JsonProperty("ValueTexts")]
        public List<string> ValueTexts { get; set; }
        public bool Elimination { get; set; }
        public bool? Time { get; set; }
    }
}
