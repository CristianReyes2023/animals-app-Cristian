using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace Core.Entities;
public class Cita : BaseEntity
{
    [Required]
    public DateOnly Fecha { get; set; }
    [Required]
    public TimeOnly Hora { get; set; }
    [Required]
    public int IdClienteFk { get; set; }
    public Cliente Clientes { get; set; }
    [Required]
    public int IdMascotaFk { get; set; }
    public Mascota Mascotas { get; set; }
    [Required]
    public int IdServicioFk { get; set; }
    public Servicio Servicios { get; set; }
}
