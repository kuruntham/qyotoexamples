using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using TableWidget.Model;

namespace TableWidget
{
    class SqliteContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
