using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class Pais : BaseEntity
{
    [Required]
    public string NombrePais { get; set; }
    public ICollection<Departamento> Departamentos { get; set; }
}
