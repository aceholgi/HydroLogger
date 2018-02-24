using System.Collections.Generic;

namespace HydroLogger.Code.DTO
{
    public class QueryResultItem
    {
        public List<HumitureItem> HumitureItems { get; set; }
        public string Name { get; set; }

        public QueryResultItem()
        {
        }
    }
}