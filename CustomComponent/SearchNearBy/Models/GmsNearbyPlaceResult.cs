using System.Collections.Generic;
using Newtonsoft.Json;

namespace CustomComponent.SearchNearBy.Models
{
    /// <summary>
    /// Result class of the Google Place API call
    /// </summary>
    public class GmsNearbyPlaceResult
    {
        /// <summary>
        /// results received by the Google Place API call
        /// </summary>

        [JsonProperty("results")]
        public IEnumerable<GmsNearByPlacePrediction> PredictionsNearBy { get; set; }

        /// <summary>
        /// The search term send to the Google Place API
        /// </summary>
        [JsonIgnore]
        public string SearchTerm { get; internal set; }
    }

    /// <summary>
    /// Gms near by place prediction results fields.
    /// </summary>
    public class GmsNearByPlacePrediction
    {

        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
        [JsonProperty("reference")]
        public string Reference { get; set; }
        [JsonProperty("geometry")]
        public GmsGeometry Geometry { get; set; }
        [JsonProperty("vicinity")]
        public string Address { get; set; }
        [JsonProperty("rating")]
        public string Rating { get; set; }
        [JsonProperty("photos")]
        public IList<Photo> photos { get; set; }
        [JsonProperty("opening_hours")]
        public OpeningHours opening_hours { get; set; }
    }
    public class Photo
    {
        [JsonProperty("photo_reference")]
        public string PhotoReference { get; set; }

    }
    public class OpeningHours
    {
        [JsonProperty("open_now")]
        public bool open_now { get; set; }
    }
}
