
using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.WebApp.Models;

public class QcResultEditModel : Result
{
    public bool IsChanged => (this.Value ?? "") != (this.OldValue ?? "");
    public bool IsNew { get; set; }
    public bool IsDeleted { get; set; }
    public string? OldValue { get; set; }

    public bool IsEditable => QcSpecification?.SpecialField == SpecialField.None;

    
    public static QcResultEditModel From(Result result, IEnumerable<QcSpecification> qcSpecifications)
    {
        return new QcResultEditModel()
        {
            Id = result.Id,
            Active = result.Active,
            Value = result.Value,
            OldValue = result.Value,
            QcSpecificationId = result.QcSpecificationId,
            QcSpecification = qcSpecifications.FirstOrDefault(c => c.Id == result.QcSpecificationId),

        };
    }

    public static Result ToResult(QcResultEditModel model)
    {
        return new Result()
        {
            Id = model.Id,
            Active = model.Active,
            Value = model.Value,
            QcSpecificationId = model.QcSpecificationId
        };
    }

}