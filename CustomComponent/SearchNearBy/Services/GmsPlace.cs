using System;
using System.Net.Http;
using System.Threading.Tasks;
using CustomComponent.Helpers;
using CustomComponent.SearchNearBy.Models;
using Newtonsoft.Json;

namespace CustomComponent.SearchNearBy.Services
{
    /// <summary>
    /// Calls the Google Place API
    /// </summary>
    public sealed class GmsPlace
    {
         static string _apiKey=Constant.APIKey;
         static GmsPlace _instance;
         readonly HttpClient _httpClient;

        /// <summary>
        /// Google Maps Place API instance
        /// </summary>
        public static GmsPlace Instance
        {
            get { return _instance ?? (_instance = new GmsPlace()); }
        }
        /// <summary>
        /// Creates a new instance of <see cref="GmsPlace"/> 
        /// </summary>
         GmsPlace() 
        {
            if (_apiKey == null) throw new InvalidOperationException("NO API KEY PROVIDED");

            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(Constant.BaseUrl)
            };
        }

        /// <summary>
        /// Performs the API call to the Google Places API to get place predictions 
        /// </summary>
        /// <param name="searchText">Search text</param>
        /// <returns>Result containing place predictions</returns>
        public async Task<GmsNearbyPlaceResult> GetNearByPredictions(string Url)
        {
            var result = await _httpClient.GetAsync(Url);

            if (result.IsSuccessStatusCode)
            {
                var placeResult = JsonConvert.DeserializeObject<GmsNearbyPlaceResult>(await result.Content.ReadAsStringAsync());
                //placeResult.SearchTerm = searchText;
                return placeResult;
            }

            return null;
        }



        /// <summary>
        /// Performs the API call to the Google Places API to get place predictions 
        /// </summary>
        /// <param name="searchText">Search text</param>
        /// <returns>Result containing place predictions</returns>
        public async Task<byte[]> GetPredictionImage(string Url)
        {
            var result = await _httpClient.GetAsync(Url);

            if (result.IsSuccessStatusCode)
            {
                var placeResult = (await result.Content.ReadAsByteArrayAsync());
                //placeResult.SearchTerm = searchText;
                return placeResult;
            }

            return null;
        }



        /// <summary>
        /// Performs the API call to the Google Places API to get place details 
        /// </summary>
        /// <param name="placeId">The google place Id</param>
        /// <returns>Result containing place details</returns>
        public async Task<GmsDetailsResult> GetDetails(string Url)
        {
            var result = await _httpClient.GetAsync(Url);

            if (result.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<GmsDetailsResult>(await result.Content.ReadAsStringAsync());
            }

            return null;
        }

    }
}
