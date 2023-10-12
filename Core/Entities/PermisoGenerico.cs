using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class PermisoGenerico : BaseEntity
{
    public string NombrePermiso { get; set; }
    public ICollection<GenericoVsSubmodulo> GenericosVsSubmodulos { get; set; }
}
