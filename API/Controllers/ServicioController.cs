using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ServicioController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ServicioController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<IEnumerable<ServicioDto>>> Get()
        {
            var servicio = await _unitOfWork.Servicios.GetAllAsync();
            return _mapper.Map<List<ServicioDto>>(servicio);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<ActionResult<Servicio>> Post(MascotaDto servicioDto)
        {
            var servicio = _mapper.Map<Servicio>(servicioDto);
                _unitOfWork.Servicios.Add(servicio);
                await _unitOfWork.SaveAsync();
            if(servicioDto == null)
                return BadRequest();
            servicioDto.Id = servicio.Id;

            return CreatedAtAction(nameof(Post),new {id = servicioDto.Id},servicioDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<ServicioDto>> Put(int id, [FromBody] ServicioDto servicioDto)
        {
            if(servicioDto == null)
            {
                return BadRequest();
            }
            if(servicioDto.Id == 0)
            {
                servicioDto.Id = id;
            }
            if(servicioDto.Id != 0)
            {
                return NotFound();
            }
            var servicio = _mapper.Map<Servicio>(servicioDto);
            _unitOfWork.Servicios.Update(servicio);
            await _unitOfWork.SaveAsync();
            return servicioDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var servicio = _unitOfWork.Servicios.GetByIdAsync(id);
            if(servicio == null)
            {
                return NotFound();
            }
            _unitOfWork.Servicios.Remove(servicio);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ServicioDto>> Get(int id)
        {
            var servicio = await  _unitOfWork.Servicios.GetByIdAsync(id);
            if(servicio == null)
                return NotFound();
            return _mapper.Map<ServicioDto>(servicio);
            
        }
    }
}