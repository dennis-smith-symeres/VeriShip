using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace VeriShip.Domain.Entities.QCSpecifications;

public enum SpecialField
{
   
    None = 0,
   
    BatchCode = 1,
    ProjectCode = 2,
    MolecularFormula = 3,
    MolecularWeight = 4,
    Comment = 5,
    StructureAsMol= 6,
    StructureAsSvg = 7,
    Appearance=8,
    ParentWeight=9
}

