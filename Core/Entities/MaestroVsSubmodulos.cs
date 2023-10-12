using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class MaestroVsSubmodulos : BaseEntity
{
    public int IdMaestro { get; set; }
    public ModuloMaestro ModulosMaestros { get; set; }
    public int IdSubmodulo { get; set; }
    public SubModulo SubModulos { get; set; }
    public ICollection<GenericoVsSubmodulo> GenericosVsSubmodulos { get; set; }
}
