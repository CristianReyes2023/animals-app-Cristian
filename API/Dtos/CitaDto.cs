using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class CitaDto
    {
        public int Id { get; set; }
        public DateTime Fecha {get;set;}
        public TimeSpan Hora {get;set;}
        public ClienteDto Cliente { get; set; }
        public MascotaDto Mascota { get; set; }
        public ServicioDto Servicio { get; set; }
    }
}