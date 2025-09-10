using VeriShip.Domain.Entities.QCSpecifications;

namespace VeriShip.Infrastructure.Services.Model.WordProcessor;


public class MergeCoA
{
    public List<GeneralInformation> GeneralInformations { get; set; } = [];
    public List<Test> Tests { get; set; } = [];
    public string? CreatedBy { get; set; }
    public string? Comment { get; set; }
    public string? Salts { get; set; }

    public static MergeCoA From(IEnumerable<Result> results, IEnumerable<QcSpecification> checks)
    {
        var newInstance = new MergeCoA();
        foreach (var check in checks)
        {
            var result = results.FirstOrDefault(x => x.QcSpecificationId == check.Id);
            if (result == null)
            {
                continue;
            }

            switch (check.Table)
            {
                case Table.GeneralInformation:
                    newInstance.GeneralInformations.Add(new GeneralInformation()
                    {
                        Category = check.Category,
                        Label = check.Acceptance,
                        Result = result.Value
                    });
                    break;
                case Table.Tests:
                    newInstance.Tests.Add(new Test()
                    {
                        Name = check.Category,
                        Acceptance = check.Acceptance,
                        Technique = check.Technique,
                        Result = result.Value
                    });
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        return newInstance;
    }
}