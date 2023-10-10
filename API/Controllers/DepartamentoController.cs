
using System.Reflection.Metadata.Ecma335;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DepartamentoController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartamentoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Get()
        {//Task: Los permite trabajar en c# o .net con tareas asincronas, te permite un uso más flexible (en que status está) y permitrabajar con expresiones landas de manera muy practica
            var departamentos = await _unitOfWork.Departamentos.GetAllAsync();
            // return Ok(entity);
            return _mapper.Map<List<DepartamentoDto>>(departamentos); 
        }
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  
        public async Task<ActionResult<Departamento>> Post(DepartamentoDto departamentoDto)
        {
            var departamento = _mapper.Map<Departamento>(departamentoDto);
            _unitOfWork.Departamentos.Add(departamento);
            await _unitOfWork.SaveAsync();
            if (departamento == null)
            {
                return BadRequest();
            }
            departamentoDto.Id = departamento.Id;
            return CreatedAtAction(nameof(Post), new { id = departamentoDto.Id }, departamentoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartamentoDto>> Put(int id, [FromBody] DepartamentoDto departamentoDto)
        {
            if(departamentoDto == null)
                return NotFound();
            if(departamentoDto.Id == 0)
            {
                departamentoDto.Id = id;
            } 
            if(departamentoDto.Id != id)
            {
                return BadRequest();
            }
            var departamento = _mapper.Map<Departamento>(departamentoDto);
            _unitOfWork.Departamentos.Update(departamento);
            await _unitOfWork.SaveAsync();
            return departamentoDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var departamento = await _unitOfWork.Departamentos.GetByIdAsync(id);
            if (departamento == null)
            {
                return NotFound();
            }
            _unitOfWork.Departamentos.Remove(departamento);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DepartamentoDto>> Get(int Id)
        {
            var departamento = await _unitOfWork.Departamentos.GetByIdAsync(Id);
            if (departamento == null)
            {
                return NotFound();
            }
            return _mapper.Map<DepartamentoDto>(departamento);
        }
    }
}