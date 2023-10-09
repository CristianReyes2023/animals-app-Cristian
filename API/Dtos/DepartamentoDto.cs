using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Dtos
{
    public class DepartamentoDto
    {
        public int Id { get; set; }
        public string NombreDep {get;set;}
        public Pais Pais { get; set; }
    }
}