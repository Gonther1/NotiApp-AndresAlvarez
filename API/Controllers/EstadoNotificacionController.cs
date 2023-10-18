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
        var estadosnot = await _unitOfWork.EstadosNotificaciones.GetAllAsync();
        return _mapper.Map<List<EstadoNotificacionDto>>(estadosnot);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<EstadoNotificacion>> Post(EstadoNotificacionDto estadonotificacionDto)
    {
        var estadosnot = _mapper.Map<EstadoNotificacion>(estadonotificacionDto);

        if (estadosnot.FechaCreacion == DateTime.MinValue)
        {
            estadosnot.FechaCreacion = DateTime.Now;
            estadonotificacionDto.FechaCreacion = DateTime.Now;
        }
        if (estadosnot.FechaModificacion == DateTime.MinValue)
        {
            estadosnot.FechaModificacion = DateTime.Now;
            estadonotificacionDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.EstadosNotificaciones.Add(estadosnot);

        await _unitOfWork.SaveAsync();
        if (estadosnot == null )
        {
            return BadRequest();
        }
        estadonotificacionDto.Id = estadosnot.Id;
        return CreatedAtAction(nameof(Post), new { id = estadonotificacionDto.Id }, estadonotificacionDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EstadoNotificacionDto>> Get(int id)
    {
        var estadosnot = await _unitOfWork.EstadosNotificaciones.GetByIdAsync(id);
        if (estadosnot == null)
        {
            return NotFound();
        }
        return _mapper.Map<EstadoNotificacionDto>(estadosnot);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<EstadoNotificacionDto>> Put(int id, [FromBody] EstadoNotificacionDto estadonotificacionDto)
    {
        var estadosnot = _mapper.Map<EstadoNotificacion>(estadonotificacionDto);

        if (estadosnot.Id == 0)
        {
            estadosnot.Id = id;
        }
        if (estadosnot.Id != id)
        {
            return BadRequest();
        }
        if (estadosnot == null)
        {
            return NotFound();
        }

        if (estadosnot.FechaCreacion == DateTime.MinValue)
        {
            estadosnot.FechaCreacion = DateTime.Now;
            estadonotificacionDto.FechaCreacion = DateTime.Now;
        }
        if (estadosnot.FechaModificacion == DateTime.MinValue)
        {
            estadosnot.FechaModificacion = DateTime.Now;
            estadonotificacionDto.FechaModificacion = DateTime.Now;  
        }

        estadonotificacionDto.Id = estadosnot.Id;
        _unitOfWork.EstadosNotificaciones.Update(estadosnot);
        await _unitOfWork.SaveAsync();
        return estadonotificacionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var estadosnot = await _unitOfWork.EstadosNotificaciones.GetByIdAsync(id);
        if (estadosnot == null)
        {
            return NotFound();
        }
        _unitOfWork.EstadosNotificaciones.Remove(estadosnot);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
