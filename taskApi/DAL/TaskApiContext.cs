using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;

namespace taskApi.DAL
{
    public class TaskApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<ApplicationHoliday> ApplicationHolidays { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            this.Configuration.LazyLoadingEnabled = false;


            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.Entity is Entity
                    && (x.State == System.Data.Entity.EntityState.Added || x.State == System.Data.Entity.EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                Entity entity = entry.Entity as Entity;
                if (entity != null)
                {
                    DateTime now = DateTime.UtcNow;
                    if (entry.State == System.Data.Entity.EntityState.Added)
                    {
                        entity.DateCreate = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.DateUpdate).IsModified = false;
                    }

                    entity.DateUpdate = now;
                }
            }

            return base.SaveChanges();
        }
    }
}