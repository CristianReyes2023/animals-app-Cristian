using System.Linq.Expressions;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
{
    private readonly AnimalsContext _context;
    public GenericRepository(AnimalsContext context)
    {
        _context = context;
    }
    //Estos metodos tiene la palabra reservada VIRTUAL
    //Este me permite hacer sobrecarga de los metodos en las otras clases  
    public virtual void Add(T entity)
    {
        _context.Set<T>().Add(entity);
        // Este método se utiliza para agregar una entidad de tipo T a la base de datos. Utiliza _context.Set<T>().Add(entity) para agregar la entidad al contexto de base de datos. 
    }

    public virtual void AddRange(IEnumerable<T> entities)
    {
        _context.Set<T>().AddRange(entities);
        //Este método agrega una colección de entidades de tipo T a la base de datos. Similar al método Add, utiliza _context.Set<T>().AddRange(entities) para agregar todas las entidades al contexto de base de datos.
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return _context.Set<T>().Where(expression);
        //Este método permite realizar búsquedas personalizadas utilizando expresiones LINQ
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    //Este método asincrónico recupera todas las entidades del tipo T desde la base de datos y las devuelve como una colección enumerable. Utiliza _context.Set<T>().ToListAsync() para obtener todas las entidades de la tabla correspondiente en la base de datos.
    {
        return await _context.Set<T>().ToListAsync();
        // return (IEnumerable<T>) await _context.Entities.FromSqlRaw("SELECT * FROM entity").ToListAsync();
        //Se utiliza para obtener una consulta a partir de una sentencia SQL, este tipo de consultas se hace atraves del _context
    }
    public virtual async Task<T> GetByIdAsync(int id)
    {
        return await _context.Set<T>().FindAsync(id);
        //Este método asincrónico se utiliza para recuperar una entidad por su identificador único (id).
    }

    public virtual Task<T> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
        //public virtual async Task<T> GetByIdAsync(string id)
        // return await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
        // Este método está definido pero lanza una excepción NotImplementedException. Es probable que esté destinado a recuperar una entidad por un identificador de tipo string-
    }

    public virtual void Remove(T entity)
    {
        _context.Set<T>().Remove(entity);
    }

    public virtual void RemoveRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public virtual void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }
    public virtual async Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(
        int pageIndex,
        int pageSize,
        string _search
    )
    // Este método asincrónico se utiliza para realizar una consulta paginada en la base de datos. Recibe los parámetros pageIndex y pageSize para especificar la página deseada y el tamaño de la página. También recibe _search, que parece ser un término de búsqueda que podría utilizarse en la consulta, aunque no está implementado en el código proporcionado.
    {
        var totalRegistros = await _context.Set<T>().CountAsync();
        var registros = await _context
            .Set<T>()
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (totalRegistros, registros);
    }
}

