
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CiudadController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CiudadController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CiudadDto>>> Get()
        {
            var ciudades = await _unitOfWork.Ciudades.GetAllAsync();
            // return Ok(entity);
            return _mapper.Map<List<CiudadDto>>(ciudades); 
        }
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  
        public async Task<ActionResult<Ciudad>> Post(CiudadDto ciudadDto)
        {
            var ciudad = _mapper.Map<Ciudad>(ciudadDto);
            _unitOfWork.Ciudades.Add(ciudad);
            await _unitOfWork.SaveAsync();
            if (ciudad == null)
            {
                return BadRequest();
            }
            ciudadDto.Id = ciudad.Id;
            return CreatedAtAction(nameof(Post), new { id = ciudadDto.Id }, ciudadDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CiudadDto>> Put(int id, [FromBody] CiudadDto ciudadDto)
        {
            if(ciudadDto == null)
                return NotFound();
            if(ciudadDto.Id == 0)
            {
                ciudadDto.Id = id;
            } 
            if(ciudadDto.Id != id)
            {
                return BadRequest();
            }
            var ciudad = _mapper.Map<Ciudad>(ciudadDto);
            _unitOfWork.Ciudades.Update(ciudad);
            await _unitOfWork.SaveAsync();
            return ciudadDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var ciudad = await _unitOfWork.Ciudades.GetByIdAsync(id);
            if (ciudad == null)
            {
                return NotFound();
            }
            _unitOfWork.Ciudades.Remove(ciudad);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CiudadDto>> Get(int Id)
        {
            var ciudad = await _unitOfWork.Ciudades.GetByIdAsync(Id);
            if (ciudad == null)
            {
                return NotFound();
            }
            return _mapper.Map<CiudadDto>(ciudad);
        }
    }
}