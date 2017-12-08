using Newtonsoft.Json;
using TK.CustomMap.Api.Google;

namespace CustomComponent.SearchNearBy.Models
{
    /// <summary>
    /// Result of the Google Maps Places details request
    /// </summary>
    public class GmsDetailsResult
    {
        [JsonProperty("status")]
         string StatusText { get; set; }
        /// <summary>
        /// Status of the API call
        /// </summary>
        public GmsDetailsResultStatus Status
        {
            get
            {
                switch (StatusText)
                {
                    case "OK":
                        return GmsDetailsResultStatus.Ok;
                    case "UNKNOWN_ERROR":
                        return GmsDetailsResultStatus.UnknownError;
                    case "ZERO_RESULTS":
                        return GmsDetailsResultStatus.ZeroResults;
                    case "OVER_QUERY_LIMIT":
                        return GmsDetailsResultStatus.OverQueryLimit;
                    case "REQUEST_DENIED":
                        return GmsDetailsResultStatus.RequestDenied;
                    case "INVALID_REQUEST":
                        return GmsDetailsResultStatus.InvalidRequest;
                    case "NOT_FOUND":
                        return GmsDetailsResultStatus.NotFound;
                    default:
                        return GmsDetailsResultStatus.UnknownStatus;
                }
            }
        }
        /// <summary>
        /// Result item
        /// </summary>
        [JsonProperty("result")]
        public GmsDetailsResultItem Item { get; set; }
    }



    /// <summary>
    /// Result item of the Google Places details api call
    /// </summary>
    public class GmsDetailsResultItem
    {
        /// <summary>
        /// All address components
        /// </summary>
        [JsonProperty("address_components")]
        public AddressComponents[] AddressComponents { get; set; }
        /// <summary>
        /// The Address as formatted text
        /// </summary>
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
        /// <summary>
        /// The phone number as formatted text
        /// </summary>
        [JsonProperty("formatted_phone_number")]
        public string FormattedPhoneNumer { get; set; }
        /// <summary>
        /// Geometry data of the place
        /// </summary>
        [JsonProperty("geometry")]
        public GmsGeometry Geometry { get; set; }
        /// <summary>
        /// Url to a place specific icon
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }
        /// <summary>
        /// Id 
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// The international phone number
        /// </summary>
        [JsonProperty("international_phone_number")]
        public string InternationalPhoneNumber { get; set; }
        /// <summary>
        /// Name of the place
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Place Id
        /// </summary>
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        // TODO: Add rest of the properties https://developers.google.com/places/web-service/details
    }
    /// <summary>
    /// Holds the data of an address component
    /// </summary>
    public struct AddressComponents
    {
        /// <summary>
        /// Long Name
        /// </summary>
        [JsonProperty("long_name")]
        public string LongName { get; set; }
        /// <summary>
        /// Short Name
        /// </summary>
        [JsonProperty("short_name")]
        public string ShortName { get; set; }
        /// <summary>
        /// Address Types e.g. "street_number" 
        /// </summary>
        public string[] Types { get; set; }
    }
   
}
