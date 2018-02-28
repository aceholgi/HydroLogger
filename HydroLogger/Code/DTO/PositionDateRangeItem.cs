using System;

namespace HydroLogger.Code.DTO
{
    public class PositionDateRangeItem
    {
        public string Positions { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}