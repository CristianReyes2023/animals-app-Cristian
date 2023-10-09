
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ClienteTelefonoController : BaseControllerApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ClienteTelefonoController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ClienteTelefonoDto>>> Get()
        {
            var clientetelefonos = await _unitOfWork.ClienteTelefonos.GetAllAsync();
            // return Ok(entity);
            return _mapper.Map<List<ClienteTelefonoDto>>(clientetelefonos); 
        }
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]  
        public async Task<ActionResult<ClienteTelefono>> Post(ClienteTelefonoDto clienteTelefonoDto)
        {
            var clientetelefono = _mapper.Map<ClienteTelefono>(clienteTelefonoDto);
            _unitOfWork.ClienteTelefonos.Add(clientetelefono);
            await _unitOfWork.SaveAsync();
            if (clientetelefono == null)
            {
                return BadRequest();
            }
            clienteTelefonoDto.Id = clientetelefono.Id;
            return CreatedAtAction(nameof(Post), new { id = clienteTelefonoDto.Id }, clienteTelefonoDto);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteTelefonoDto>> Put(int id, [FromBody] ClienteTelefonoDto clienteTelefonoDto)
        {
            if(clienteTelefonoDto == null)
                return NotFound();
            var clientetelefono = _mapper.Map<ClienteTelefono>(clienteTelefonoDto);
            _unitOfWork.ClienteTelefonos.Update(clientetelefono);
            await _unitOfWork.SaveAsync();
            return clienteTelefonoDto;
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var clientetelefono = await _unitOfWork.ClienteTelefonos.GetByIdAsync(id);
            if (clientetelefono == null)
            {
                return NotFound();
            }
            _unitOfWork.ClienteTelefonos.Remove(clientetelefono);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ClienteTelefonoDto>> Get(int Id)
        {
            var clientetelefono = await _unitOfWork.ClienteTelefonos.GetByIdAsync(Id);
            if (clientetelefono == null)
            {
                return NotFound();
            }
            return _mapper.Map<ClienteTelefonoDto>(clientetelefono);
        }
    }
}