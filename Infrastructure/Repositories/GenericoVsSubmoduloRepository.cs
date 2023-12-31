using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class GenericoVsSubmoduloRepository : GenericRepository<GenericoVsSubmodulo>, IGenericoVsSubmoduloRepository
{
    private readonly NotiAppContext _context;

    public GenericoVsSubmoduloRepository(NotiAppContext context) : base(context)
    {
        _context = context;
    }
}
