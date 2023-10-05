using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;
public class DepartamentoRepository : GenericRepository<Departamento>
{
    private readonly AnimalsContext _context;

    public DepartamentoRepository(AnimalsContext context) : base(context)
    {
        _context = context;
    }
}   