using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace API.Controllers
{
    // [ApiController]
    // [Route("api/[controller]")]
    //Definir la ruta que quiero que tenga (se remplaza por pais ya que de PaisController se elimina el controller)
    public class PaisController : BaseControllerApi //
    {
        private readonly IUnitOfWork _unitOfWork;//Agregamos el guion bajo _ para diferenciar el nombre del atributo de la clase vs el nombre del parametro que le estoy ingresando al constructor
        private readonly IMapper _mapper;

        //Definimos el constructor

        public PaisController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            //En este punto ya se empieza a mappear todos los DTO y los voy a utilizar para poder obtener algunos resultados en algunas consusltas
            // Para poder mapear debo crear los campos,metodos, parametros etc para poder hacer este proceso necesitamos refactorizar y asignar un nuevo campo de unitOfWork (Create and assing field 'unitOfWork) y el mismo proceso para el mapper 
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<PaisDto>>> Get()
        {
            var paises = await _unitOfWork.Paises.GetAllAsync();
            // return Ok(entity);
            return _mapper.Map<List<PaisDto>>(paises); 
        }
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status200OK)]//Insertado correctamente
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//La solicitud fue errada  
        public async Task<ActionResult<Pais>> Post(PaisDto paisDto)//Recibe dentro de sus parametros de tipo Dto (Id y nombre) 
        {//Omitimos el Id cuando hagamos la petición ya que es un elemetno AI y no es de inserción propia 
            var pais = _mapper.Map<Pais>(paisDto);//usamos el mapper para mapear el Dto a la clase o a la entidad
            _unitOfWork.Paises.Add(pais);//Agregar la información que ya se encuentra en nuestro objeto de tipo pais
            await _unitOfWork.SaveAsync();//Usamos un metodo asyn para guardar la información en la base de datos 
            if (pais == null)
            {
                return BadRequest();
            }
            paisDto.Id = pais.Id;//Se usa para poder asignarle el Id que se genero de manera automatica por la base de datos a nuestro elemento ID
            return CreatedAtAction(nameof(Post), new { id = paisDto.Id }, paisDto);//Obtenemos el retorno de nuestra cadena de JSON donde va a retornar el ID y el contenido del paisDto
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]//Exitosamente
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaisDto>> Put(int id, [FromBody] PaisDto paisDto)//Argumentos ingresados es Id, segundo argumento vamos a tener la información modificada que le vamos a enviar a la base de datos
        {
            if(paisDto == null)
                return NotFound();
            var paises = _mapper.Map<Pais>(paisDto);//Conversión de Dto a la entidad paises
            _unitOfWork.Paises.Update(paises);//Ejecutamos de manera temporal la actualización
            await _unitOfWork.SaveAsync();//Por ultimo almacenamos la información en la base de datos 
            return paisDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)//OJO REVISAR SI NUESTRO ID A INGRESAR ES INT O STRING
        {
            var pais = await _unitOfWork.Paises.GetByIdAsync(id);//Ejecutamos el metodo Get para obtener el pais por el Id
            if (pais == null)
            {
                return NotFound();
            }
            _unitOfWork.Paises.Remove(pais);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaisDto>> Get(int Id)
        {
            var pais = await _unitOfWork.Paises.GetByIdAsync(Id);
            if (pais == null)
            {
                return NotFound();
            }
            return _mapper.Map<PaisDto>(pais);
        }
    }
}