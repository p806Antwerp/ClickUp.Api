using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickUp.Api.DTOs
{
    public class ClickUpTagDTO
    {      
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("project_id")]
        public string ProjectId { get; set; }

        [JsonProperty("tag_fg")]
        public string TagForeground { get; set; }

        [JsonProperty("tag_bg")]
        public string TagBackground { get; set; }

        [JsonProperty("creator")]
        public long Creator { get; set; }
    }
}
