using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace EmployeesManagement.Domain.Common;

public class BaseEntity
{
    [Key]
    public Guid ID { get; set; } = default!;

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    private readonly List<BaseEvent> _domainEvents = new();

    [NotMapped]
    [JsonIgnore]
    [IgnoreDataMember]
    public IReadOnlyCollection<object> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(BaseEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}