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
        var hilosrespuestasnot = await _unitOfWork.HilosRespuestasNotificaciones.GetAllAsync();
        return _mapper.Map<List<HiloRespuestaNotificacionDto>>(hilosrespuestasnot);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<HiloRespuestaNotificacion>> Post(HiloRespuestaNotificacionDto hilorespuestanotDto)
    {
        var hilorespuestanot = _mapper.Map<HiloRespuestaNotificacion>(hilorespuestanotDto);
        this._unitOfWork.HilosRespuestasNotificaciones.Add(hilorespuestanot);
        await _unitOfWork.SaveAsync();
        if (hilorespuestanot == null )
        {
            return BadRequest();
        }
        hilorespuestanotDto.Id = hilorespuestanotDto.Id;
        return CreatedAtAction(nameof(Post), new { id = hilorespuestanotDto.Id }, hilorespuestanotDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<HiloRespuestaNotificacionDto>> Get(int id)
    {
        var hilorespuestanot = await _unitOfWork.HilosRespuestasNotificaciones.GetByIdAsync(id);
        if (hilorespuestanot == null)
        {
            return NotFound();
        }
        return _mapper.Map<HiloRespuestaNotificacionDto>(hilorespuestanot);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<HiloRespuestaNotificacionDto>> Put(int id, [FromBody] HiloRespuestaNotificacionDto hilorespuestanotDto)
    {
        if (hilorespuestanotDto == null)
            return NotFound();
        var hilosrespuestasnot = _mapper.Map<HiloRespuestaNotificacion>(hilorespuestanotDto);
        _unitOfWork.HilosRespuestasNotificaciones.Update(hilosrespuestasnot);
        await _unitOfWork.SaveAsync();
        return hilorespuestanotDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var hilorespuestanot = await _unitOfWork.HilosRespuestasNotificaciones.GetByIdAsync(id);
        if (hilorespuestanot == null)
        {
            return NotFound();
        }
        _unitOfWork.HilosRespuestasNotificaciones.Remove(hilorespuestanot);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
