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

public class RadicadoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RadicadoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<RadicadoDto>>> Get()
    {
        var radicados = await _unitOfWork.Radicados.GetAllAsync();
        return _mapper.Map<List<RadicadoDto>>(radicados);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Radicado>> Post(RadicadoDto radicadoDto)
    {
        var radicados = _mapper.Map<Radicado>(radicadoDto);

        if (radicados.FechaCreacion == DateTime.MinValue)
        {
            radicados.FechaCreacion = DateTime.Now;
            radicadoDto.FechaCreacion = DateTime.Now;
        }
        if (radicados.FechaModificacion == DateTime.MinValue)
        {
            radicados.FechaModificacion = DateTime.Now;
            radicadoDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.Radicados.Add(radicados);

        await _unitOfWork.SaveAsync();
        if (radicados == null )
        {
            return BadRequest();
        }
        radicadoDto.Id = radicados.Id;
        return CreatedAtAction(nameof(Post), new { id = radicadoDto.Id }, radicadoDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RadicadoDto>> Get(int id)
    {
        var radicados = await _unitOfWork.Radicados.GetByIdAsync(id);
        if (radicados == null)
        {
            return NotFound();
        }
        return _mapper.Map<RadicadoDto>(radicados);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<RadicadoDto>> Put(int id, [FromBody] RadicadoDto radicadoDto)
    {
        var radicados = _mapper.Map<Radicado>(radicadoDto);

        if (radicados.Id == 0)
        {
            radicados.Id = id;
        }
        if (radicados.Id != id)
        {
            return BadRequest();
        }
        if (radicados == null)
        {
            return NotFound();
        }

        if (radicados.FechaCreacion == DateTime.MinValue)
        {
            radicados.FechaCreacion = DateTime.Now;
            radicadoDto.FechaCreacion = DateTime.Now;
        }
        if (radicados.FechaModificacion == DateTime.MinValue)
        {
            radicados.FechaModificacion = DateTime.Now;
            radicadoDto.FechaModificacion = DateTime.Now;  
        }

        radicadoDto.Id = radicados.Id;
        _unitOfWork.Radicados.Update(radicados);
        await _unitOfWork.SaveAsync();
        return radicadoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var radicados = await _unitOfWork.Radicados.GetByIdAsync(id);
        if (radicados == null)
        {
            return NotFound();
        }
        _unitOfWork.Radicados.Remove(radicados);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
