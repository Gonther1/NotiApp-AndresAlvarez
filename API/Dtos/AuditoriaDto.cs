using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Dtos;

public class AuditoriaDto : BaseEntity
{
    public string NombreUsuario { get; set; }
    public int DescAccion { get; set; }
}
