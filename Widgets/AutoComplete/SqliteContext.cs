using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AutoComplete
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
