using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Backend.src.Data
{
    public class DbInterceptor : SaveChangesInterceptor
    {
        private void UpdateTimestamps(DbContextEventData eventData)
        {
            var entries = eventData.Context!.ChangeTracker
                .Entries()
                .Where(
                    e =>
                        e.Entity is BaseModel
                        && (e.State == EntityState.Added || e.State == EntityState.Modified)
                );

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    ((BaseModel)entry.Entity).CreatedAt = DateTime.Now;
                }
                else
                {
                    ((BaseModel)entry.Entity).UpdatedAt = DateTime.Now;
                }
            }
        }

        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result
        )
        {
            UpdateTimestamps(eventData);
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
            DbContextEventData eventData,
            InterceptionResult<int> result,
            CancellationToken cancellationToken = default
        )
        {
            UpdateTimestamps(eventData);
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
