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

public class TipoNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoNotificacionDto>>> Get()
    {
        var tiposnotificaciones = await _unitOfWork.TiposNotificaciones.GetAllAsync();
        return _mapper.Map<List<TipoNotificacionDto>>(tiposnotificaciones);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoNotificacion>> Post(TipoNotificacionDto tiponotDto)
    {
        var tiposnotificaciones = _mapper.Map<TipoNotificacion>(tiponotDto);

        if (tiposnotificaciones.FechaCreacion == DateTime.MinValue)
        {
            tiposnotificaciones.FechaCreacion = DateTime.Now;
            tiponotDto.FechaCreacion = DateTime.Now;
        }
        if (tiposnotificaciones.FechaModificacion == DateTime.MinValue)
        {
            tiposnotificaciones.FechaModificacion = DateTime.Now;
            tiponotDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.TiposNotificaciones.Add(tiposnotificaciones);

        await _unitOfWork.SaveAsync();
        if (tiposnotificaciones == null )
        {
            return BadRequest();
        }
        tiponotDto.Id = tiposnotificaciones.Id;
        return CreatedAtAction(nameof(Post), new { id = tiponotDto.Id }, tiponotDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoNotificacionDto>> Get(int id)
    {
        var tiposnotificaciones = await _unitOfWork.TiposNotificaciones.GetByIdAsync(id);
        if (tiposnotificaciones == null)
        {
            return NotFound();
        }
        return _mapper.Map<TipoNotificacionDto>(tiposnotificaciones);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoNotificacionDto>> Put(int id, [FromBody] TipoNotificacionDto tiponotDto)
    {
        var tiposnotificaciones = _mapper.Map<TipoNotificacion>(tiponotDto);

        if (tiposnotificaciones.Id == 0)
        {
            tiposnotificaciones.Id = id;
        }
        if (tiposnotificaciones.Id != id)
        {
            return BadRequest();
        }
        if (tiposnotificaciones == null)
        {
            return NotFound();
        }

        if (tiposnotificaciones.FechaCreacion == DateTime.MinValue)
        {
            tiposnotificaciones.FechaCreacion = DateTime.Now;
            tiponotDto.FechaCreacion = DateTime.Now;
        }
        if (tiposnotificaciones.FechaModificacion == DateTime.MinValue)
        {
            tiposnotificaciones.FechaModificacion = DateTime.Now;
            tiponotDto.FechaModificacion = DateTime.Now;  
        }

        tiponotDto.Id = tiposnotificaciones.Id;
        _unitOfWork.TiposNotificaciones.Update(tiposnotificaciones);
        await _unitOfWork.SaveAsync();
        return tiponotDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var tiposnotificaciones = await _unitOfWork.TiposNotificaciones.GetByIdAsync(id);
        if (tiposnotificaciones == null)
        {
            return NotFound();
        }
        _unitOfWork.TiposNotificaciones.Remove(tiposnotificaciones);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
