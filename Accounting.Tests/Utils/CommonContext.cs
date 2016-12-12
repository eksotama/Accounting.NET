using Accounting.DAL;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Tests
{
    public class CommonContext
    {
        public Dictionary<string, object> ObjectBag { get; set; }

        public EffortProviderFactory effortFactory { get; set; }

        private AccountingDbContext Context { get; set; }

        public CommonContext()
        {
            this.ObjectBag = new Dictionary<string, object>();
            effortFactory = new EffortProviderFactory();
        }

        public void ResetContext()
        {
            Context = null;
        }

        public AccountingDbContext GetContext()
        {
            if (Context == null)
            {
                Context = new AccountingDbContext(effortFactory.CreateConnection("name=AccountingDbContext"));
            }
            return Context;
        }

    }

    public class EffortProviderFactory : IDbConnectionFactory
    {
        private static DbConnection _connection;
        private readonly static object _lock = new object();

        public static void ResetDb()
        {
            lock (_lock)
            {
                _connection = null;
            }
        }

        public DbConnection CreateConnection(string nameOrConnectionString)
        {
            lock (_lock)
            {
                if (_connection == null)
                {
                    _connection = Effort.DbConnectionFactory.CreateTransient();
                }

                return _connection;
            }
        }
    }
}
