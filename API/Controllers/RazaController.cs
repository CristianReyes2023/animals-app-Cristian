
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class RazaController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RazaController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<RazaDto>>> Get()
        {
            var razas = await _unitOfWork.Razas.GetAllAsync();
            // return Ok(entity);
            return _mapper.Map<List<RazaDto>>(razas); 
        }
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  
        public async Task<ActionResult<Raza>> Post(RazaDto razaDto)
        {
            var raza = _mapper.Map<Raza>(razaDto);
            _unitOfWork.Razas.Add(raza);
            await _unitOfWork.SaveAsync();
            if (raza == null)
            {
                return BadRequest();
            }
            razaDto.Id = raza.Id;
            return CreatedAtAction(nameof(Post), new { id = razaDto.Id }, razaDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RazaDto>> Put(int id, [FromBody] RazaDto razaDto)
        {
            if(razaDto == null)
                return NotFound();
            var raza = _mapper.Map<Raza>(razaDto);
            _unitOfWork.Razas.Update(raza);
            await _unitOfWork.SaveAsync();
            return razaDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var raza = await _unitOfWork.Razas.GetByIdAsync(id);
            if (raza == null)
            {
                return NotFound();
            }
            _unitOfWork.Razas.Remove(raza);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RazaDto>> Get(int Id)
        {
            var raza = await _unitOfWork.Razas.GetByIdAsync(Id);
            if (raza == null)
            {
                return NotFound();
            }
            return _mapper.Map<RazaDto>(raza);
        }
    }
}