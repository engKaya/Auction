using Auction.Core.Repository.Common.Context;
using Auction.Core.Repository.Common.DataBase;
using MediatR;

namespace Auction.Core.Middleware.Service.DomainEvents
{
    public static class DomainEventExtensions
    {
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, BaseDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());


            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);

        }
    }
}
