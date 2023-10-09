
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class MascotaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MascotaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<MascotaDto>>> Get()
        {
            var mascotas = await _unitOfWork.Mascotas.GetAllAsync();
            // return Ok(entity);
            return _mapper.Map<List<MascotaDto>>(mascotas); 
        }
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  
        public async Task<ActionResult<Mascota>> Post(MascotaDto mascotaDto)
        {
            var mascota = _mapper.Map<Mascota>(mascotaDto);
            _unitOfWork.Mascotas.Add(mascota);
            await _unitOfWork.SaveAsync();
            if (mascota == null)
            {
                return BadRequest();
            }
            mascotaDto.Id = mascota.Id;
            return CreatedAtAction(nameof(Post), new { id = mascotaDto.Id }, mascotaDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MascotaDto>> Put(int id, [FromBody] MascotaDto mascotaDto)
        {
            if(mascotaDto == null)
                return NotFound();
            var mascota = _mapper.Map<Mascota>(mascotaDto);
            _unitOfWork.Mascotas.Update(mascota);
            await _unitOfWork.SaveAsync();
            return mascotaDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var mascota = await _unitOfWork.Mascotas.GetByIdAsync(id);
            if (mascota == null)
            {
                return NotFound();
            }
            _unitOfWork.Mascotas.Remove(mascota);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MascotaDto>> Get(int Id)
        {
            var mascota = await _unitOfWork.Mascotas.GetByIdAsync(Id);
            if (mascota == null)
            {
                return NotFound();
            }
            return _mapper.Map<MascotaDto>(mascota);
        }
    }
}