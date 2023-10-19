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

public class TipoRequerimientoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TipoRequerimientoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<TipoRequerimientoDto>>> Get()
    {
        var tiposreq = await _unitOfWork.TiposRequerimientos.GetAllAsync();
        return _mapper.Map<List<TipoRequerimientoDto>>(tiposreq);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoRequerimiento>> Post(TipoRequerimientoDto tiporeqDto)
    {
        var tiposreq = _mapper.Map<TipoRequerimiento>(tiporeqDto);

        if (tiposreq.FechaCreacion == DateTime.MinValue)
        {
            tiposreq.FechaCreacion = DateTime.Now;
            tiporeqDto.FechaCreacion = DateTime.Now;
        }
        if (tiposreq.FechaModificacion == DateTime.MinValue)
        {
            tiposreq.FechaModificacion = DateTime.Now;
            tiporeqDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.TiposRequerimientos.Add(tiposreq);

        await _unitOfWork.SaveAsync();
        if (tiposreq == null )
        {
            return BadRequest();
        }
        tiporeqDto.Id = tiposreq.Id;
        return CreatedAtAction(nameof(Post), new { id = tiporeqDto.Id }, tiporeqDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoRequerimientoDto>> Get(int id)
    {
        var tiposreq = await _unitOfWork.TiposRequerimientos.GetByIdAsync(id);
        if (tiposreq == null)
        {
            return NotFound();
        }
        return _mapper.Map<TipoRequerimientoDto>(tiposreq);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<TipoRequerimientoDto>> Put(int id, [FromBody] TipoRequerimientoDto tiporeqDto)
    {
        var tiposreq = _mapper.Map<TipoRequerimiento>(tiporeqDto);

        if (tiposreq.Id == 0)
        {
            tiposreq.Id = id;
        }
        if (tiposreq.Id != id)
        {
            return BadRequest();
        }
        if (tiposreq == null)
        {
            return NotFound();
        }

        if (tiposreq.FechaCreacion == DateTime.MinValue)
        {
            tiposreq.FechaCreacion = DateTime.Now;
            tiporeqDto.FechaCreacion = DateTime.Now;
        }
        if (tiposreq.FechaModificacion == DateTime.MinValue)
        {
            tiposreq.FechaModificacion = DateTime.Now;
            tiporeqDto.FechaModificacion = DateTime.Now;  
        }

        tiporeqDto.Id = tiposreq.Id;
        _unitOfWork.TiposRequerimientos.Update(tiposreq);
        await _unitOfWork.SaveAsync();
        return tiporeqDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var tiposreq = await _unitOfWork.TiposRequerimientos.GetByIdAsync(id);
        if (tiposreq == null)
        {
            return NotFound();
        }
        _unitOfWork.TiposRequerimientos.Remove(tiposreq);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
