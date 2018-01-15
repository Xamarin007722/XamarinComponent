using System;
using SQLite;

namespace CustomComponent.SearchNearBy.Models
{
    /// <summary>
    /// Token model class .
    /// </summary>
    public class TokenModel
    {
        public TokenModel()
        {
        }
        /// <summary>
        /// Token model class properties
        /// </summary>
        /// <value>The identifier.</value>
        [PrimaryKey]
        public int ID { get; set; }
        public string access_token { get; set; }
        public string error_description { get; set; }
        public DateTime expire_date { get; set; }
    }
}
