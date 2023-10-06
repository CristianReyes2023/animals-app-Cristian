using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaisRepository : GenericRepository<Pais>, IPais
    {
        private readonly AnimalsContext _context;

        public PaisRepository(AnimalsContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IEnumerable<Pais>> GetAllAsync()//Este codigo sobreescribe este metodo presente en GenericRepository 
        //Este codigo indica que cuando liste los paises que incluya los departamentos y dentro de los departamentos me incluya las ciudades que esten asociadas a determinado departamento
        {
            return await _context.Paises
            .Include(p => p.Departamentos)
            .ThenInclude(c => c.Ciudades)
            .ToListAsync();
        }
        //Implementamos paginado de consulata 
        public override async Task<(int totalRegistros, IEnumerable<Pais> registros)> GetAllAsync(
            /*
            En este codigo vamos a encontrar una sobercarga del metodo override (anterior a este esto se puede hacer por que tenia la palabra reservada virtual) que va a recibir unos argumentos (totalRegistros) y regresa registros(que es una colleción))
            */
            int pageIndex,//Ingresa parametro, corresponde a cual pagina referenciar en determinado momento 
            int pageSize,//Ingresa parametro, cantidad de registros que queremos visualizar por pagina 
            string search//Ingresa parametro, se utiliza para pasar algun criterio de busqueda
        )
        {
            var query = _context.Paises as IQueryable<Pais>;//Consulta que nos permite obtener los paises 
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.NombrePais.ToLower().Contains(search));//Si esta variables es diferente a null, la consulta va a estar basada al elemento que estoy buscando
            }
            query = query.OrderBy(p => p.Id); //Se encarga de orderna el listado por el Id
            var totalRegistros = await query.CountAsync();//Total de registros 
            var registros = await query  //Tenmos una consulta donde podemos obtener elementos especificos y al tener skip o take podemos tener un rango dentro de la consulta
                .Include(u => u.Departamentos)
                .Skip((pageIndex - 1) * pageSize) 
                .Take(pageSize)
                .ToListAsync();
            return (totalRegistros, registros);
        }
    }
}