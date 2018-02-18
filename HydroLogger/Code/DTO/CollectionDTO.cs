using System.Collections.Generic;

namespace HydroLogger.Code.DTO
{
    public class CollectionDTO
    {
        public string Name { get; set; }
        public string Temperatures { get; set; }
        public string Humiditys { get; set; }
        public string Dates { get; set; }
        public List<string> TemperaturesList { get; set; }
        public List<string> HumiditysList { get; set; }
        public List<string> DatesList { get; set; }

        public CollectionDTO() {
            TemperaturesList = new List<string>();
            HumiditysList = new List<string>();
            DatesList = new List<string>();
        }


        public void Concat()
        {
            Temperatures = _ConcatList(TemperaturesList);
            TemperaturesList.Clear();
            Humiditys = _ConcatList(HumiditysList);
            HumiditysList.Clear();
            Dates = _ConcatList(DatesList);
            DatesList.Clear();
        }

        private string _ConcatList(List<string> list)
        {
            string ret = "";
            foreach (string s in list)
                ret += s + ',';

            return ret.Substring(0, ret.Length - 1);
        }
    }
}