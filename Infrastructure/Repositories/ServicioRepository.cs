
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;
public class ServicioRepository : GenericRepository<Servicio> , IServicio
{
    private readonly AnimalsContext _context;

    public ServicioRepository(AnimalsContext context) : base(context)
    {
        _context = context;
    }
}