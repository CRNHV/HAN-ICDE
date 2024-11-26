using ICDE.Data.Entities.OnderwijsOnderdeel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ICDE.Data.Interceptors;
public class OnderwijsOnderdeelInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context == null)
            return await base.SavingChangesAsync(eventData, result);

        var entries = context.ChangeTracker.Entries()
            .Where(e => e.Entity is IOnderwijsOnderdeel && e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            var entity = (IOnderwijsOnderdeel)entry.Entity;
            if (entity.RelationshipChanged)
                continue;
            var newEntity = context.Entry(entity).CurrentValues.ToObject();
            ((IOnderwijsOnderdeel)newEntity).VersieNummer = entity.VersieNummer + 1;
            ((IOnderwijsOnderdeel)newEntity).Id = 0;
            entry.State = EntityState.Detached;
            context.Add(newEntity);
        }

        return await base.SavingChangesAsync(eventData, result);
    }
}