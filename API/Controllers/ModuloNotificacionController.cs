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
        var modulonotificacion = _mapper.Map<ModuloNotificacion>(modulonotificacionDto);
        this._unitOfWork.ModulosNotificaciones.Add(modulonotificacion);
        await _unitOfWork.SaveAsync();
        if (modulonotificacion == null )
        {
            return BadRequest();
        }
        modulonotificacionDto.Id = modulonotificacionDto.Id;
        return CreatedAtAction(nameof(Post), new { id = modulonotificacionDto.Id }, modulonotificacionDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ModuloNotificacionDto>> Get(int id)
    {
        var modulonotificacion = await _unitOfWork.ModulosNotificaciones.GetByIdAsync(id);
        if (modulonotificacion == null)
        {
            return NotFound();
        }
        return _mapper.Map<ModuloNotificacionDto>(modulonotificacion);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ModuloNotificacionDto>> Put(int id, [FromBody] ModuloNotificacionDto modulonotificacionDto)
    {
        if (modulonotificacionDto == null)
            return NotFound();
        var modulosnotificaciones = _mapper.Map<ModuloNotificacion>(modulonotificacionDto);
        _unitOfWork.ModulosNotificaciones.Update(modulosnotificaciones);
        await _unitOfWork.SaveAsync();
        return modulonotificacionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var modulonotificacion = await _unitOfWork.ModulosNotificaciones.GetByIdAsync(id);
        if (modulonotificacion == null)
        {
            return NotFound();
        }
        _unitOfWork.ModulosNotificaciones.Remove(modulonotificacion);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
