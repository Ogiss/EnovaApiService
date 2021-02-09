using Soneta.Business.App;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace EnovaApiService.Extensions.Soneta
{
    internal class SonetaSqlReader : IDisposable
    {
        private readonly Connection _connection;
        private SqlDataReader _sqlDataReader;
        private Dictionary<string, PropertyInfo> _typeInfo;
        private Dictionary<int, PropertyInfo> _resultFields;


        private SonetaSqlReader(Connection connection) => _connection = connection;

        public static IEnumerable<T> Query<T>(Connection connection, string sql)
        {
            using (var reader = new SonetaSqlReader(connection).ExecuteCommand(sql))
            {
                while (reader.Read())
                {
                    yield return reader.GetRow<T>();
                }

            }
        }

        public void Dispose()
        {
            if (_sqlDataReader != null)
            {
                _sqlDataReader.Dispose();
                _sqlDataReader = null;
            }
        }

        private SonetaSqlReader ExecuteCommand(string sql)
        {
            _sqlDataReader = (SqlDataReader)_connection.ExecuteCommand(ExecuteMode.Reader, sql);
            return this;
        }

        private bool Read()
        {
            return _sqlDataReader.Read();
        }

        private T GetRow<T>()
        {
            T row = Activator.CreateInstance<T>();

            foreach (var kvp in GetResultFields<T>())
            {
                var val = _sqlDataReader.GetValue(kvp.Key);

                kvp.Value.SetValue(row, val);
            }

            return row;
        }

        private Dictionary<int, PropertyInfo> GetResultFields<T>()
        {
            if (_resultFields == null)
            {
                _resultFields = new Dictionary<int, PropertyInfo>();

                for (var i = 0; i < _sqlDataReader.FieldCount; i++)
                {
                    var name = _sqlDataReader.GetName(i);
                    var propertyInfo = GetPropertyInfo<T>(name);

                    if (propertyInfo != null)
                    {
                        _resultFields.Add(i, propertyInfo);
                    }
                }
            }

            return _resultFields;
        }

        private PropertyInfo GetPropertyInfo<T>(string propertyName)
        {
            if (_typeInfo == null)
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static
                    | BindingFlags.SetProperty | BindingFlags.SetField).ToList();

                _typeInfo = properties.ToDictionary((x) => x.Name);
            }

            if (_typeInfo.TryGetValue(propertyName, out PropertyInfo propertyInfo))
            {
                return propertyInfo;
            }

            return null;
        }
    }
}
