using Microsoft.EntityFrameworkCore.ChangeTracking;
using VeriShip.Domain.Entities.Audits;

namespace VeriShip.Infrastructure.Persistence;

internal class AuditEntry : Audit
{
    public List<PropertyEntry> TemporaryProperties { get; } = [];
    public bool HasTemporaryProperties => TemporaryProperties.Any();

    public Audit ToAudit()
    {
        return new Audit()
        {
            Action = Action,
            User = User,
            DateTime = DateTime,
            NewValues = NewValues,
            RowId = RowId,
            TableName = TableName,

        };
    }
}