using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces
{//Es una estrategia que vamos a tener para poder agrupar todos los repositorios y interfaces en una sola clase.Esto con el objetivo de optimizar el uso de recursos de memoria especificamente.
//Normalmente cuando trabajamos con endpoints y de inicialización de clases todas las clases de instancian de forma automatica se usen o no se usen y eso puede producir en un momento determinado una perdida de rendimiento. Por medio de las unidades de trabajo (UnitOfWork) se puede ir instanciando aquellas clases que necesitamos y no usamos aquellas que no necesitamos en su debido momento.
    public interface IUnitOfWork  
    {//En la unidad de trabajo voy a implementar aquellas interfaces correspondientes a mis entidades 
    }
}