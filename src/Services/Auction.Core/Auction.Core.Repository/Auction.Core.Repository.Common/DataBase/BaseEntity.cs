using Auction.Core.Repository.Common.Interface.BaseEntity;
using MediatR;

namespace Auction.Core.Repository.Common.DataBase
{
    public class BaseEntity : IBaseEntity
    {
        private DateTime _createdAt;
        private DateTime _updatedAt;
        private string _createdBy = string.Empty;
        private string _updatedBy = string.Empty;
        private string _requestId = string.Empty;
        private bool _isDeleted;

        public DateTime CreatedAt { get => _createdAt; set => _createdAt = value; }
        public DateTime UpdatedAt { get => _updatedAt; set => _updatedAt = value; }
        public string CreatedBy { get => _createdBy; set => _createdBy = value; }
        public string UpdatedBy { get => _updatedBy; set => _updatedBy = value; }
        public bool IsDeleted { get => _isDeleted; set => _isDeleted = value; } 
        public string RequestId { get => _requestId; set => _requestId = value; }

        private IList<INotification>? domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => domainEvents?.ToList() ?? []; 

        public void AddDomain(INotification notification)
        {
            domainEvents = domainEvents ?? new List<INotification>();
            domainEvents.Add(notification);
        }

        public void RemoveDomain(INotification notification) => domainEvents?.Remove(notification); 

        public void ClearDomainEvents() =>  domainEvents?.Clear();
    }
}
