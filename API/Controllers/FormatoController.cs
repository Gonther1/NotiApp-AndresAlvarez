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

public class FormatoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public FormatoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<FormatoDto>>> Get()
    {
        var formatos = await _unitOfWork.Formatos.GetAllAsync();
        return _mapper.Map<List<FormatoDto>>(formatos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<Formato>> Post(FormatoDto formatoDto)
    {
        var formatos = _mapper.Map<Formato>(formatoDto);

        if (formatos.FechaCreacion == DateTime.MinValue)
        {
            formatos.FechaCreacion = DateTime.Now;
            formatoDto.FechaCreacion = DateTime.Now;
        }
        if (formatos.FechaModificacion == DateTime.MinValue)
        {
            formatos.FechaModificacion = DateTime.Now;
            formatoDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.Formatos.Add(formatos);

        await _unitOfWork.SaveAsync();
        if (formatos == null )
        {
            return BadRequest();
        }
        formatoDto.Id = formatos.Id;
        return CreatedAtAction(nameof(Post), new { id = formatoDto.Id }, formatoDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<FormatoDto>> Get(int id)
    {
        var formatos = await _unitOfWork.Formatos.GetByIdAsync(id);
        if (formatos == null)
        {
            return NotFound();
        }
        return _mapper.Map<FormatoDto>(formatos);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<FormatoDto>> Put(int id, [FromBody] FormatoDto formatoDto)
    {
        var formatos = _mapper.Map<Formato>(formatoDto);

        if (formatos.Id == 0)
        {
            formatos.Id = id;
        }
        if (formatos.Id != id)
        {
            return BadRequest();
        }
        if (formatos == null)
        {
            return NotFound();
        }

        if (formatos.FechaCreacion == DateTime.MinValue)
        {
            formatos.FechaCreacion = DateTime.Now;
            formatoDto.FechaCreacion = DateTime.Now;
        }
        if (formatos.FechaModificacion == DateTime.MinValue)
        {
            formatos.FechaModificacion = DateTime.Now;
            formatoDto.FechaModificacion = DateTime.Now;  
        }

        formatoDto.Id = formatos.Id;
        _unitOfWork.Formatos.Update(formatos);
        await _unitOfWork.SaveAsync();
        return formatoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var formatos = await _unitOfWork.Formatos.GetByIdAsync(id);
        if (formatos == null)
        {
            return NotFound();
        }
        _unitOfWork.Formatos.Remove(formatos);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
