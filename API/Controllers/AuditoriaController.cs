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

public class AuditoriaController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public AuditoriaController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<AuditoriaDto>>> Get()
    {
        var auditorias = await _unitOfWork.Auditorias.GetAllAsync();
        return _mapper.Map<List<AuditoriaDto>>(auditorias);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Auditoria>> Post(AuditoriaDto auditoriaDto)
    {
        var auditorias = _mapper.Map<Auditoria>(auditoriaDto);

        if (auditorias.FechaCreacion == DateTime.MinValue)
        {
            auditorias.FechaCreacion = DateTime.Now;
            auditoriaDto.FechaCreacion = DateTime.Now;
        }
        if (auditorias.FechaModificacion == DateTime.MinValue)
        {
            auditorias.FechaModificacion = DateTime.Now;
            auditoriaDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.Auditorias.Add(auditorias);

        await _unitOfWork.SaveAsync();
        if (auditorias == null )
        {
            return BadRequest();
        }
        auditoriaDto.Id = auditorias.Id;
        return CreatedAtAction(nameof(Post), new { id = auditoriaDto.Id }, auditoriaDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<AuditoriaDto>> Get(int id)
    {
        var auditorias = await _unitOfWork.Auditorias.GetByIdAsync(id);
        if (auditorias == null)
        {
            return NotFound();
        }
        return _mapper.Map<AuditoriaDto>(auditorias);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<AuditoriaDto>> Put(int id, [FromBody] AuditoriaDto auditoriaDto)
    {
        var auditorias = _mapper.Map<Auditoria>(auditoriaDto);

        if (auditorias.Id == 0)
        {
            auditorias.Id = id;
        }
        if (auditorias.Id != id)
        {
            return BadRequest();
        }
        if (auditorias == null)
        {
            return NotFound();
        }

        if (auditorias.FechaCreacion == DateTime.MinValue)
        {
            auditorias.FechaCreacion = DateTime.Now;
            auditoriaDto.FechaCreacion = DateTime.Now;
        }
        if (auditorias.FechaModificacion == DateTime.MinValue)
        {
            auditorias.FechaModificacion = DateTime.Now;
            auditoriaDto.FechaModificacion = DateTime.Now;  
        }

        auditoriaDto.Id = auditorias.Id;
        _unitOfWork.Auditorias.Update(auditorias);
        await _unitOfWork.SaveAsync();
        return auditoriaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var auditorias = await _unitOfWork.Auditorias.GetByIdAsync(id);
        if (auditorias == null)
        {
            return NotFound();
        }
        _unitOfWork.Auditorias.Remove(auditorias);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
