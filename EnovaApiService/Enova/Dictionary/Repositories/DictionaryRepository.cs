using EnovaApiService.Extensions.Soneta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovaApiService.Enova.Dictionary.Repositories
{
    public class DictionaryRepository : IDictionaryRepository
    {
        public IEnumerable<Guid> GetDeletedItemsGuids(DateTime stampFrom, DateTime stampTo)
        {
            var sql =
                $"SELECT SourceGuid Guid FROM ChangeInfos " +
                $"WHERE SourceTable='Dictionary' AND Type=4 AND [Time] > '{stampFrom.ToString("yyyy-MM-dd HH:mm:ss.fff")}' AND [Time] <= '{stampTo.ToString("yyyy-MM-dd HH:mm:ss.fff")}'";

            using (var connection = EnovaClient.Database.OpenConnection(Soneta.Business.App.DatabaseType.Operational))
            {
                return connection.Query<GuidEntry>(sql).Select(x => x.Guid).ToArray();
            }
        }

        private class GuidEntry
        {
            public Guid Guid { get; set; }
        }
    }
}
