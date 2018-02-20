using HydroLogger.Code.DTO;
using MongoDB.Driver;
using System;

namespace HydroLogger.Code
{
    public static class FilterBuilder
    {
        public static FilterDefinition<HumitureItem> BuildFilter(DateTime from, DateTime to)
        {
            var filterBuilder = Builders<HumitureItem>.Filter;

            var filter = filterBuilder.Gte(x => x.Date, from) &
            filterBuilder.Lte(x => x.Date, to);

            return filter;
        }
    }
}