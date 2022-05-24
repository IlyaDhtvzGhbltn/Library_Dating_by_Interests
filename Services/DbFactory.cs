using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services
{
    public class DbFactory<DbContext> : IFactory<DbContext>
    {
        private readonly Func<DbContext> _factory;
        public DbFactory(Func<DbContext> factory)
        {
            _factory = factory;
        }
        public DbContext Create()
        {
            return _factory();
        }
    }
}
