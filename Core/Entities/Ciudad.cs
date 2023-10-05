using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class Ciudad : BaseEntity
{
    [Required]
    public string NombreCiudad { get; set; }
    public int IdDepFk { get; set; }
    public Departamento Departamentos { get; set; }
    // public ICollection<Cliente> Clientes { get; set; } No debe existir ya que existe otra conección que hace dicha función
    public ClienteDireccion ClienteDireccion { get; set; }
}