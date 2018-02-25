using HydroLogger.Code.DTO;
using MongoDB.Driver;
using System;

namespace HydroLogger.Code
{
    public static class FilterBuilder
    {
        public static class Humiture
        {
            /// <summary>
            /// Returns everything
            /// </summary>
            /// <returns></returns>
            public static FilterDefinition<HumitureItem> BuildFilter()
            {
                var filterBuilder = Builders<HumitureItem>.Filter;

                var filter = filterBuilder.Empty;

                return filter;
            }

            /// <summary>
            /// Returns Items where HumitureItem.Date >= from && HumitureItem.Date <= to
            /// </summary>
            /// <param name="from"></param>
            /// <param name="to"></param>
            /// <returns></returns>
            public static FilterDefinition<HumitureItem> BuildFilter(DateTime from, DateTime to)
            {
                var filterBuilder = Builders<HumitureItem>.Filter;

                var filter = filterBuilder.Gte(x => x.Date, from) &
                filterBuilder.Lte(x => x.Date, to);

                return filter;
            }
        }

        public static class UploaderConfig
        {
            /// <summary>
            /// Returns everything
            /// </summary>
            /// <returns></returns>
            public static FilterDefinition<UploaderConfigItem> BuildFilter()
            {
                var filterBuilder = Builders<UploaderConfigItem>.Filter;

                var filter = filterBuilder.Empty;

                return filter;
            }

            /// <summary>
            /// Returns Items where UploaderConfigItem.Id == id
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public static FilterDefinition<UploaderConfigItem> BuildFilter(string id)
            {
                var filterBuilder = Builders<UploaderConfigItem>.Filter;

                var filter = filterBuilder.Eq(x => x.UploaderId, id);

                return filter;
            }
        }
    }
}