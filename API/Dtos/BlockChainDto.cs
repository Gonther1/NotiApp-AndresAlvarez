using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;

public class BlockChainDto
{
    public int Id { get; set; }
    public string HashGenerado { get; set; }
    public int IdNotificacion { get; set; }
    public int IdHiloRespuesta { get; set; }
    public int IdAuditoria { get; set; }
}
