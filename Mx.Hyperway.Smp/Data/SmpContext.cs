using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mx.Hyperway.Smp.Data
{
    using Microsoft.EntityFrameworkCore;
    public class SmpContext : DbContext
    {
        public SmpContext(DbContextOptions<SmpContext> options): base(options)
        {
         
        }

        public DbSet<SmpHost> SmpHosts { get; set; }
        public DbSet<PeppolDocument> PeppolDocuments { get; set; }
        public DbSet<PeppolProcess> PeppolProcesses { get; set; }
        public DbSet<SmpService> SmpServices { get; set; }
        public DbSet<PeppolParticipant> PeppolParticipants { get; set; }
        public DbSet<SmpServiceEndpoint> SmpServiceEndpoints { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeppolDocument>().HasAlternateKey(x => x.Identifier);
            modelBuilder.Entity<PeppolProcess>().HasAlternateKey(x => x.Identifier);
            modelBuilder.Entity<PeppolParticipant>().HasAlternateKey(x => x.Identifier);
        }

        public override int SaveChanges()
        {
            // get entries that are being Added or Updated
            var modifiedEntries = this.ChangeTracker.Entries()
                .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            var now = DateTime.Now;

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as AuditableEntity;
                if (entity == null)
                {
                    continue;
                }
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                }

                entity.UpdatedAt = now;
            }

            return base.SaveChanges();
        }
    }
}
