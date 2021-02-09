using Soneta.Business.App;
using System.Collections.Generic;

namespace EnovaApiService.Extensions.Soneta
{
    public static class ConnectionExtensions 
    {
        public static IEnumerable<T> Query<T>(this Connection connection, string sql)
        {
            return SonetaSqlReader.Query<T>(connection, sql);
        }
    }
}
