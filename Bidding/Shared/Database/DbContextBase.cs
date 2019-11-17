using Bidding.Models.DatabaseModels.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Linq;

namespace Bidding.Shared.Database
{
    /// <summary>
    /// Provides an extension class to the Entity Framework DBContext class
    /// </summary>
    public class DbContextBase<TContext> : DbContext where TContext : DbContext
    {
        protected DbContextBase(DbContextOptions<TContext> options) : base(options)
        {
        }

        /// <summary>
        /// Executes the specified SQL (e.g. a stored procedure call) and returns a collection of records. To execute the SQL or stored procedure in
        /// a database transaction, set the transaction parameter to true. (Note: Entity Framework does not support T-SQL transactions inside of stored procedures.)
        /// </summary>
        /// <typeparam name="T">Result model class</typeparam>
        /// <param name="sql">Parametrized SQL query (SQL injection safe!)</param>
        /// <param name="transaction">True if the SQL should be executed in a transaction, or False (default) if not</param>
        public IQueryable<T> Execute<T>(FormattableString sql, bool transaction = false) where T : class
        {
            IQueryable<T> collection() => Set<T>().FromSql(sql).AsNoTracking();

            if (transaction)
            {
                return Transaction(collection);
            }
            return collection();
        }

        /// <summary>
        /// Execute sql query for specific value type, for example, Guid, string, int, bool
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IQueryable<T> ExecuteValueType<T>(FormattableString sql, bool transaction = false) where T : struct
        {
            return Execute<Scalar<T>>(sql, transaction).Select(row => (row == null) ? default(T) : row.Value);
        }

        /// <summary>
        /// Executes the specified SQL (e.g. a stored procedure call) and returns the first record from the result set or null. To execute the SQL or stored procedure
        /// in a database transaction, set the transaction parameter to true. (Note: Entity Framework does not support T-SQL transactions inside of stored procedures.)
        /// </summary>
        /// <typeparam name="T">Result model class</typeparam>
        /// <param name="sql">Parametrized SQL query (SQL injection safe!)</param>
        /// <param name="transaction">True if the SQL should be executed in a transaction, or False (default) if not</param>
        public T ExecuteScalar<T>(FormattableString sql, bool transaction = false) where T : class
        {
            return Execute<T>(sql, transaction).FirstOrDefault();
        }

        /// <summary>
        /// Executes the specified SQL (e.g. a stored procedure call). To execute the SQL or stored procedure
        /// in a database transaction, set the transaction parameter to true. (Note: Entity Framework does not support T-SQL transactions inside of stored procedures.)
        /// </summary>
        /// <param name="sql">Parametrized SQL query (SQL injection safe!)</param>
        /// <param name="transaction">True if the SQL should be executed in a transaction, or False (default) if not</param>
        public void ExecuteNonQuery(FormattableString sql, bool transaction = false)
        {
            Execute<Scalar<object>>(sql, transaction);
        }

        /// <summary>
        /// Executes the specified action in a database transaction and returns a collection of records.
        /// </summary>
        /// <typeparam name="T">Result model class</typeparam>
        /// <param name="collection">Action returning a collection of records</param>
        public IQueryable<T> Transaction<T>(Func<IQueryable<T>> collection) where T : class
        {
            IQueryable<T> result = null;
            IExecutionStrategy strategy = Database.CreateExecutionStrategy(); // retry on transient exceptions (we have to do it manually because of transactions)
            strategy.Execute(() =>                                            // note: all transient exceptions outside of the BeginTransaction block are handled globally
            {
                using (IDbContextTransaction transaction = Database.BeginTransaction())
                {
                    result = collection().ToList().AsQueryable(); ; // cannot iterate through the collection before committing changes
                    transaction.Commit();
                }
            });
            return result;
        }
    }
}
