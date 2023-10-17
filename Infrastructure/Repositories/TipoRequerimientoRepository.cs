using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class TipoRequerimientoRepository : GenericRepository<TipoRequerimiento>, ITipoRequerimientoRepository
{
    private readonly NotiAppContext _context;

    public TipoRequerimientoRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }
}
