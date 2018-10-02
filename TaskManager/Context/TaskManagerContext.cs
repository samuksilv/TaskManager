using Microsoft.EntityFrameworkCore;
using TaskManager.Entities.EntitiesDB;

namespace TaskManager.Context
{
    public class TaskManagerContext : DbContext {
        public TaskManagerContext (DbContextOptions<TaskManagerContext> options) : base (options) { }
        public virtual DbSet<USER> USER { get; set; }
        public virtual DbSet<TASK> TASK { get; set; }
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            #region Sequences

            modelBuilder.HasSequence<long> ("S_SEQUSER").StartsAt (1).HasMin (1);
            modelBuilder.HasSequence<long> ("S_SEQTASK").StartsAt (1).HasMin (1);

            #endregion

            #region propeties Mapping
            modelBuilder.Entity<USER> ()
                .Property (u => u.SEQUSER)
                .HasDefaultValueSql ("nextval('\"S_SEQUSER\"')");

            modelBuilder.Entity<TASK> ()
                .Property (u => u.SEQTASK)
                .HasDefaultValueSql ("nextval('\"S_SEQTASK\"')");

            modelBuilder.Entity<USER> ()
                .HasMany (u => u.TASK)
                .WithOne (u => u.USER);
            #endregion        

            base.OnModelCreating (modelBuilder);
        }

    }
}