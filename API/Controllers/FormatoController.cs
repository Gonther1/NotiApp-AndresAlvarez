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
        var formato = _mapper.Map<Formato>(formatoDto);
        this._unitOfWork.Formatos.Add(formato);
        await _unitOfWork.SaveAsync();
        if (formato == null )
        {
            return BadRequest();
        }
        formatoDto.Id = formatoDto.Id;
        return CreatedAtAction(nameof(Post), new { id = formatoDto.Id }, formatoDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<FormatoDto>> Get(int id)
    {
        var formato = await _unitOfWork.Formatos.GetByIdAsync(id);
        if (formato == null)
        {
            return NotFound();
        }
        return _mapper.Map<FormatoDto>(formato);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<FormatoDto>> Put(int id, [FromBody] FormatoDto formatoDto)
    {
        if (formatoDto == null)
            return NotFound();
        var formatos = _mapper.Map<Formato>(formatoDto);
        _unitOfWork.Formatos.Update(formatos);
        await _unitOfWork.SaveAsync();
        return formatoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var formato = await _unitOfWork.Formatos.GetByIdAsync(id);
        if (formato == null)
        {
            return NotFound();
        }
        _unitOfWork.Formatos.Remove(formato);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
