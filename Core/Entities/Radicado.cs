using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class Radicado : BaseEntity
{
    public ICollection<ModuloNotificacion> ModuloNotificaciones { get; set; }
}
