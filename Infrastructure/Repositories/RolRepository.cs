using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class RolRepository : GenericRepository<Rol>, IRolRepository
{
    private readonly NotiAppContext _context;

    public RolRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }
}
