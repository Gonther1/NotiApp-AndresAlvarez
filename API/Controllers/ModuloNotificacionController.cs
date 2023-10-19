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

public class ModuloNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModuloNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ModuloNotificacionDto>>> Get()
    {
        var modulosnotificaciones = await _unitOfWork.ModulosNotificaciones.GetAllAsync();
        return _mapper.Map<List<ModuloNotificacionDto>>(modulosnotificaciones);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ModuloNotificacion>> Post(ModuloNotificacionDto modulonotificacionDto)
    {
        var modulosnotificaciones = _mapper.Map<ModuloNotificacion>(modulonotificacionDto);

        if (modulosnotificaciones.FechaCreacion == DateTime.MinValue)
        {
            modulosnotificaciones.FechaCreacion = DateTime.Now;
            modulonotificacionDto.FechaCreacion = DateTime.Now;
        }
        if (modulosnotificaciones.FechaModificacion == DateTime.MinValue)
        {
            modulosnotificaciones.FechaModificacion = DateTime.Now;
            modulonotificacionDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.ModulosNotificaciones.Add(modulosnotificaciones);

        await _unitOfWork.SaveAsync();
        if (modulosnotificaciones == null )
        {
            return BadRequest();
        }
        modulonotificacionDto.Id = modulosnotificaciones.Id;
        return CreatedAtAction(nameof(Post), new { id = modulonotificacionDto.Id }, modulonotificacionDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ModuloNotificacionDto>> Get(int id)
    {
        var modulosnotificaciones = await _unitOfWork.ModulosNotificaciones.GetByIdAsync(id);
        if (modulosnotificaciones == null)
        {
            return NotFound();
        }
        return _mapper.Map<ModuloNotificacionDto>(modulosnotificaciones);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ModuloNotificacionDto>> Put(int id, [FromBody] ModuloNotificacionDto modulonotificacionDto)
    {
        var modulosnotificaciones = _mapper.Map<ModuloNotificacion>(modulonotificacionDto);

        if (modulosnotificaciones.Id == 0)
        {
            modulosnotificaciones.Id = id;
        }
        if (modulosnotificaciones.Id != id)
        {
            return BadRequest();
        }
        if (modulosnotificaciones == null)
        {
            return NotFound();
        }

        if (modulosnotificaciones.FechaCreacion == DateTime.MinValue)
        {
            modulosnotificaciones.FechaCreacion = DateTime.Now;
            modulonotificacionDto.FechaCreacion = DateTime.Now;
        }
        if (modulosnotificaciones.FechaModificacion == DateTime.MinValue)
        {
            modulosnotificaciones.FechaModificacion = DateTime.Now;
            modulonotificacionDto.FechaModificacion = DateTime.Now;  
        }

        modulonotificacionDto.Id = modulosnotificaciones.Id;
        _unitOfWork.ModulosNotificaciones.Update(modulosnotificaciones);
        await _unitOfWork.SaveAsync();
        return modulonotificacionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var modulosnotificaciones = await _unitOfWork.ModulosNotificaciones.GetByIdAsync(id);
        if (modulosnotificaciones == null)
        {
            return NotFound();
        }
        _unitOfWork.ModulosNotificaciones.Remove(modulosnotificaciones);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
