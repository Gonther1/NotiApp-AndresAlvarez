using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;

public class BaseEntity
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
}
