using MongoDB.Bson;
using Newtonsoft.Json;

namespace HydroLogger.Code.DTO
{
    public class UploaderConfigItem
    {
        [JsonIgnore]
        public BsonObjectId _id { get; set; }

        public string Position { get; set; }
        public string UploaderId { get; set; }       
    }
}