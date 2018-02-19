using System.Collections.Generic;

namespace HydroLogger.Code.DTO
{
    public class CollectionDTO
    {
        public string Name { get; set; }
        public List<string> Temperatures { get; set; }
        public List<string> Humiditys { get; set; }
        public List<string> Dates { get; set; }

        public CollectionDTO()
        {
            Temperatures = new List<string>();
            Humiditys = new List<string>();
            Dates = new List<string>();
        }
    }
}