using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;
public class EstadoNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EstadoNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<EstadoNotificacionDto>>> Get()
    {
        var estadosnotificaciones = await _unitOfWork.EstadosNotificaciones.GetAllAsync();
        return _mapper.Map<List<EstadoNotificacionDto>>(estadosnotificaciones);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<EstadoNotificacion>> Post(EstadoNotificacionDto estadonotificacionDto)
    {
        var estadonotificacion = _mapper.Map<EstadoNotificacion>(estadonotificacionDto);
        this._unitOfWork.EstadosNotificaciones.Add(estadonotificacion);
        await _unitOfWork.SaveAsync();
        if (estadonotificacion == null )
        {
            return BadRequest();
        }
        estadonotificacionDto.Id = estadonotificacionDto.Id;
        return CreatedAtAction(nameof(Post), new { id = estadonotificacionDto.Id }, estadonotificacionDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EstadoNotificacionDto>> Get(int id)
    {
        var estadonotificacion = await _unitOfWork.EstadosNotificaciones.GetByIdAsync(id);
        if (estadonotificacion == null)
        {
            return NotFound();
        }
        return _mapper.Map<EstadoNotificacionDto>(estadonotificacion);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<EstadoNotificacionDto>> Put(int id, [FromBody] EstadoNotificacionDto estadonotificacionDto)
    {
        if (estadonotificacionDto == null)
            return NotFound();
        var estadosnotificaciones = _mapper.Map<EstadoNotificacion>(estadonotificacionDto);
        _unitOfWork.EstadosNotificaciones.Update(estadosnotificaciones);
        await _unitOfWork.SaveAsync();
        return estadonotificacionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var estadonotificacion = await _unitOfWork.EstadosNotificaciones.GetByIdAsync(id);
        if (estadonotificacion == null)
        {
            return NotFound();
        }
        _unitOfWork.EstadosNotificaciones.Remove(estadonotificacion);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
