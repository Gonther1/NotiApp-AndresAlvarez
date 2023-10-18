using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class RolvSMaestroDto
{
    public int Id { get; set; }
    public int IdRol { get; set; }
    public int IdMaestro { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime FechaModificacion { get; set; }
}
