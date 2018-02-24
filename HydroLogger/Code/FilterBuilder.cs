using HydroLogger.Code.DTO;
using MongoDB.Bson;
using MongoDB.Driver;
using System;

namespace HydroLogger.Code
{
    public static class FilterBuilder
    {

        #region Humiture Items
        /// <summary>
        /// Returns Items where HumitureItem.Date >= from && HumitureItem.Date <= to
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public static FilterDefinition<HumitureItem> BuildHumitureFilter(DateTime from, DateTime to)
        {
            var filterBuilder = Builders<HumitureItem>.Filter;

            var filter = filterBuilder.Gte(x => x.Date, from) &
            filterBuilder.Lte(x => x.Date, to);

            return filter;
        }
        #endregion

        #region Uploader Config Items
        /// <summary>
        /// Returns everything
        /// </summary>
        /// <returns></returns>
        public static FilterDefinition<UploaderConfigItem> BuildUploaderConfigFilter()
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
        public static FilterDefinition<UploaderConfigItem> BuildUploaderConfigFilter(string id)
        {
            var filterBuilder = Builders<UploaderConfigItem>.Filter;

            var filter = filterBuilder.Eq(x => x.UploaderId, id);

            return filter;
        }
        #endregion
    }
}