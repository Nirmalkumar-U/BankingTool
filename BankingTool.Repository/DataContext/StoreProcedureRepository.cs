using System.Reflection;
using BankingTool.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore; // For EF Core functionalities

namespace BankingTool.Repository
{
    public class StoreProcedureRepository(DataContext dataContext) : EntityRepository<Users>(dataContext), IStoreProcedureRepository
    {
        public async Task<List<T>> RunStoreProcedures<T>(string sqlQuery, Dictionary<string, string> para)
        {
            var sql = sqlQuery;
            List<SqlParameter> parameters = [];
            foreach (var par in para)
            {
                parameters.Add(new SqlParameter($"@{par.Key}", par.Value));
            }
            using var command = dataContext.Database.GetDbConnection().CreateCommand();
            command.CommandText = sql;
            foreach (var param in parameters)
            {
                command.Parameters.Add(param);
            }

            await dataContext.Database.OpenConnectionAsync();
            using var result = await command.ExecuteReaderAsync();
            var records = new List<T>();

            while (await result.ReadAsync())
            {
                var record = Activator.CreateInstance<T>();
                for (int i = 0; i < result.FieldCount; i++)
                {
                    var prop = typeof(T).GetProperty(result.GetName(i), BindingFlags.Instance | BindingFlags.Public | BindingFlags.IgnoreCase);
                    if (prop != null && !result.IsDBNull(i))
                    {
                        prop.SetValue(record, result.GetValue(i));
                    }
                }
                records.Add(record);
            }

            return records;
        }

        //public async Task<List<T>> RunStoreProcedure<T>(string sqlQuery, Dictionary<string, string> para)
        //{
        //    var parameters = new List<SqlParameter>();

        //    foreach (var item in para)
        //    {
        //        parameters.Add(new SqlParameter($"@{item.Key}", item.Value ?? (object)DBNull.Value));
        //    }

        //    //var actions = await dataContext.Set<T>()
        //    //    .FromSqlRaw(sqlQuery, parameters.ToArray())
        //    //    .ToListAsync();

        //    var dbSetMethod = dataContext.GetType().GetMethod("Set", new Type[] { })?.MakeGenericMethod(typeof(T));

        //    var dbSet = dbSetMethod.Invoke(dataContext, null);

        //    // Execute the query and map results to the entity type
        //    var actions = await ((IQueryable<T>)dbSet)
        //        .FromSqlRaw(sqlQuery, parameters)
        //        .ToListAsync();
        //    return actions;
        //}


    }
}




//var result1 = command.Query<T>(sql, parameters.ToArray()).ToList();
