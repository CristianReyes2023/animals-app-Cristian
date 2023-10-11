using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class MascotaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Especie { get; set; }
        public int IdRazaFk { get; set; }
        public int IdClienteFk { get; set; }
        public DateOnly FechaNacimiento { get; set; }
        // public RazaDto Raza { get; set; }
        // public ClienteDto Cliente { get; set; }
    }
}