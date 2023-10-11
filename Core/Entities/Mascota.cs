using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities;
public class Mascota : BaseEntity
{
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Especie { get; set; }
    [Required]
    public int IdRazaFk { get; set; }
    public Raza Razas { get; set; } //Revisar si va raza o razas
    public DateOnly FechaNacimiento { get; set; }
    [Required]
    public int IdClienteFk { get; set; }
    public Cliente Clientes { get; set; }
    public ICollection<Cita> Citas { get; set; }

}
