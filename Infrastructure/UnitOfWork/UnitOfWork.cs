using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.UnitOfWork
{//Está es una implementación de la unidad de trabajo (IUnitOfWork)
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly AnimalsContext _context;

        public UnitOfWork(AnimalsContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    } 
}