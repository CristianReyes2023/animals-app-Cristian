using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ClienteRepository : GenericRepository<Cliente> , ICliente
    {
        private readonly AnimalsContext _context;

        public ClienteRepository(AnimalsContext context) : base(context)
        {
            _context = context;
        }
    }
}