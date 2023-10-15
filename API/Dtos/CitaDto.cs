using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CitaDto
    {
        public int Id { get; set; }
        public DateOnly Fecha {get;set;}
        public TimeOnly Hora {get;set;}
        public int IdClienteFk { get; set; }
        public int IdMascotaFk { get; set; }
        public int IdServicioFk { get; set; }
        
        // public ClienteDto Cliente { get; set; }
        // public MascotaDto Mascota { get; set; }
        // public ServicioDto Servicio { get; set; }
    }
}