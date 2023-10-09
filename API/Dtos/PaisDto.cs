using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos;
public class PaisDto
{
    public int Id { get; set; }
    public string NombrePais { get; set; }
}

//DTO: Son objetos de transformación de datos donde yo puedo personalizar o tomar deciciones de como necesito que el usuario vea la información. Normalmente cuando tenemos peticiones como GET nosotros nos enfretamos a algo muy importante y es que cuando tenemos el resultado de una consulta o petición que cuando tenemos relaciones no sale Null (ese null equivale a los ICollection que tenemos) por medio de los DTO hacemos que esos null no aparezcan es decir mostrarle al usuario lo que querramos. Esto permite proteger nuestra información, incluso podemos hacer que nuestras clases tenga unos atributos con un nombre y mostrarle al usuario con otro nombre. 

//FALTA HACE LOS DTOSSS