using System.Linq.Expressions;
using Core.Entities;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(int Id); 
    //Este método asincrónico se utiliza para recuperar una entidad por su identificador único (Id).
    //Tener en cuenta que tipo de datos es el Id para hacer la busqueda si String o Int
    Task<IEnumerable<T>> GetAllAsync();
    //Este método asincrónico recupera todas las entidades del tipo T y devuelve una colección enumerable de las mismas.
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);    
    // Este método permite realizar búsquedas personalizadas utilizando expresiones LINQ.

    Task<(int totalRegistros, IEnumerable<T> registros)> GetAllAsync(int pageIndex, int pageSize, string search); 
    //Este método asincrónico se utiliza para paginar y buscar registros en una colección.  
    void Add(T entity);
    //Este método agrega una entidad de tipo T a la base de datos.
    void AddRange(IEnumerable<T> entities);
    //Este método agrega una colección de entidades de tipo T a la base de datos.
    void Remove(T entity);
    // Este método elimina una entidad de tipo T de la base de datos.
    void RemoveRange(IEnumerable<T> entities);
    // Este método elimina una colección de entidades de tipo T de la base de datos.
    void Update(T entity);
    //Este método actualiza una entidad de tipo T en la base de datos.
} 
