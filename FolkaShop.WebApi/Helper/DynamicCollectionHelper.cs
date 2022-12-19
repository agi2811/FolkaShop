using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace FolkaShop.WebApi
{
    public static class DynamicCollectionHelper
    {
        public static IEnumerable<dynamic> CollectionFromSql(this DbContext dbContext, string sql, Dictionary<string, object> Parameters)
        {
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                foreach (KeyValuePair<string, object> param in Parameters)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = param.Key;
                    dbParameter.Value = param.Value;
                    cmd.Parameters.Add(dbParameter);
                }

                var retObject = new List<dynamic>();
                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            var type = dataReader[fieldCount].GetType().Name;
                            var val = (type == "DBNull" ? null : dataReader[fieldCount]);
                            dataRow.Add(dataReader.GetName(fieldCount), val);
                        }

                        retObject.Add((ExpandoObject)dataRow);
                    }
                }

                return retObject;
            }
        }

        public static List<ExpandoObject> CollectionFromSqlGeneric(this DbContext dbContext, string sql, Dictionary<string, object> Parameters)
        {
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                foreach (KeyValuePair<string, object> param in Parameters)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = param.Key;
                    dbParameter.Value = param.Value;
                    cmd.Parameters.Add(dbParameter);
                }

                var retObject = new List<ExpandoObject>();
                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            var type = dataReader[fieldCount].GetType().Name;
                            var val = (type == "DBNull" ? null : dataReader[fieldCount]);
                            dataRow.Add(dataReader.GetName(fieldCount), val);
                        }

                        retObject.Add((ExpandoObject)dataRow);
                    }
                }

                return retObject;
            }
        }

        public static List<T> GetDataFromSqlToList<T>(this DbContext dbContext, string sql) where T : class
        {
            SqlConnection sqlCon = new SqlConnection();
            var retObject = new ExpandoObject();
            List<ExpandoObject> allRow = new List<ExpandoObject>();

            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            var type = dataReader[fieldCount].GetType().Name;
                            var val = (type == "DBNull" ? null : dataReader[fieldCount]);
                            dataRow.Add(dataReader.GetName(fieldCount), val);
                        }

                        retObject = (ExpandoObject)dataRow;
                        allRow.Add(retObject);
                    }
                }

                var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<List<T>, List<ExpandoObject>>());
                var mapper = new Mapper(mapperConfig);
                List<T> result = mapper.Map<List<T>>(allRow);
                return result;
            }
        }

        public static T GetDataFromSqlToSingle<T>(this DbContext dbContext, string sql) where T : class
        {
            SqlConnection sqlCon = new SqlConnection();
            var retObject = new ExpandoObject();
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = new ExpandoObject() as IDictionary<string, object>;
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            var type = dataReader[fieldCount].GetType().Name;
                            var val = (type == "DBNull" ? null : dataReader[fieldCount]);
                            dataRow.Add(dataReader.GetName(fieldCount), val);
                        }

                        retObject = (ExpandoObject)dataRow;
                    }
                }

                var mapperConfig = new MapperConfiguration(cfg => cfg.CreateMap<T, ExpandoObject>());
                var mapper = new Mapper(mapperConfig);
                T result = mapper.Map<T>(retObject);
                return result;
            }
        }
    }
}