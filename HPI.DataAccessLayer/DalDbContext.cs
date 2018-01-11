using MySql.Data.Entity;
using System.Data.Common;
using System.Data.Entity;
using HPI.DataAccessLayer.DataModels;
namespace HPI.DataAccessLayer
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DalDbContext:DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DalDbContext()
            : base("name=DalDbContext")
        {

        }

        // Constructor to use on a DbConnection that is already opened
        public DalDbContext(DbConnection existingConnection, bool contextOwnsConnection)
              : base(existingConnection, contextOwnsConnection)
            {

            }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Configurations.Add(new ProductMap());
            base.OnModelCreating(modelBuilder);
           
        }
    }
}
