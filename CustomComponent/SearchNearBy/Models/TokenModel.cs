using System;
using SQLite;

namespace CustomComponent.SearchNearBy.Models
{
    public class TokenModel
    {
        public TokenModel()
        {
        }
        [PrimaryKey]
        public int ID { get; set; }
        public string access_token { get; set; }
        public string error_description { get; set; }
        public DateTime expire_date { get; set; }
    }
}
