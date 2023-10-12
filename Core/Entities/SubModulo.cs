using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class SubModulo : BaseEntity
{
    public string NombreSubModulo { get; set; }
    public ICollection<MaestroVsSubmodulos> MaestroVsSubmodulos { get; set; }
}
