using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class HiloRespuestaNotificacionRepository : GenericRepository<HiloRespuestaNotificacion>, IHiloRespuestaNotificacionRepository
{
    private readonly NotiAppContext _context;

    public HiloRespuestaNotificacionRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }
}
