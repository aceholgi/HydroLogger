using System.Collections.Generic;

namespace HydroLogger.Code.DTO
{
    public class ResultDTO
    {
        public List<HumitureItem> HumitureItems { get; set; }
        public string Name { get; set; }

        public ResultDTO()
        {
        }

        public ResultDTO(string name, List<HumitureItem> items)
        {
            HumitureItems = items;
            Name = name;
        }
    }
}