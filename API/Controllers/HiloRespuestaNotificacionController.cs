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

public class HiloRespuestaNotificacionController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public HiloRespuestaNotificacionController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<HiloRespuestaNotificacionDto>>> Get()
    {
        var hrespuestasnot = await _unitOfWork.HilosRespuestasNotificaciones.GetAllAsync();
        return _mapper.Map<List<HiloRespuestaNotificacionDto>>(hrespuestasnot);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<HiloRespuestaNotificacion>> Post(HiloRespuestaNotificacionDto hrespuestanotDto)
    {
        var hrespuestasnot = _mapper.Map<HiloRespuestaNotificacion>(hrespuestanotDto);

        if (hrespuestasnot.FechaCreacion == DateTime.MinValue)
        {
            hrespuestasnot.FechaCreacion = DateTime.Now;
            hrespuestanotDto.FechaCreacion = DateTime.Now;
        }
        if (hrespuestasnot.FechaModificacion == DateTime.MinValue)
        {
            hrespuestasnot.FechaModificacion = DateTime.Now;
            hrespuestanotDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.HilosRespuestasNotificaciones.Add(hrespuestasnot);

        await _unitOfWork.SaveAsync();
        if (hrespuestasnot == null )
        {
            return BadRequest();
        }
        hrespuestanotDto.Id = hrespuestasnot.Id;
        return CreatedAtAction(nameof(Post), new { id = hrespuestanotDto.Id }, hrespuestanotDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<HiloRespuestaNotificacionDto>> Get(int id)
    {
        var hrespuestasnot = await _unitOfWork.HilosRespuestasNotificaciones.GetByIdAsync(id);
        if (hrespuestasnot == null)
        {
            return NotFound();
        }
        return _mapper.Map<HiloRespuestaNotificacionDto>(hrespuestasnot);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<HiloRespuestaNotificacionDto>> Put(int id, [FromBody] HiloRespuestaNotificacionDto hrespuestanotDto)
    {
        var hrespuestasnot = _mapper.Map<HiloRespuestaNotificacion>(hrespuestanotDto);

        if (hrespuestasnot.Id == 0)
        {
            hrespuestasnot.Id = id;
        }
        if (hrespuestasnot.Id != id)
        {
            return BadRequest();
        }
        if (hrespuestasnot == null)
        {
            return NotFound();
        }

        if (hrespuestasnot.FechaCreacion == DateTime.MinValue)
        {
            hrespuestasnot.FechaCreacion = DateTime.Now;
            hrespuestanotDto.FechaCreacion = DateTime.Now;
        }
        if (hrespuestasnot.FechaModificacion == DateTime.MinValue)
        {
            hrespuestasnot.FechaModificacion = DateTime.Now;
            hrespuestanotDto.FechaModificacion = DateTime.Now;  
        }

        hrespuestanotDto.Id = hrespuestasnot.Id;
        _unitOfWork.HilosRespuestasNotificaciones.Update(hrespuestasnot);
        await _unitOfWork.SaveAsync();
        return hrespuestanotDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var hrespuestasnot = await _unitOfWork.HilosRespuestasNotificaciones.GetByIdAsync(id);
        if (hrespuestasnot == null)
        {
            return NotFound();
        }
        _unitOfWork.HilosRespuestasNotificaciones.Remove(hrespuestasnot);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
