using System;
using Newtonsoft.Json;
using TK.CustomMap.Api.Google;

namespace CustomComponent.SearchNearBy.Models
{
    /// <summary>
    /// Google Geometry data
    /// </summary>
    public class GmsGeometry
    {
        /// <summary>
        /// Location of the place
        /// </summary>
        [JsonProperty("location")]
        public GmsLocation Location { get; set; }
    }
}
