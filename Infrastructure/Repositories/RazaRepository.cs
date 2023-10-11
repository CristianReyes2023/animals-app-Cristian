
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;
public class RazaRepository : GenericRepository<Raza> , IRaza
{
    private readonly AnimalsContext _context;

    public RazaRepository(AnimalsContext context) : base(context)
    {
        _context = context;
    }
}