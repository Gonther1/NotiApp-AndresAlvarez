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
public class ModuloMaestroController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModuloMaestroController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<ModuloMaestroDto>>> Get()
    {
        var modulosmaestros = await _unitOfWork.ModulosMaestros.GetAllAsync();
        return _mapper.Map<List<ModuloMaestroDto>>(modulosmaestros);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ModuloMaestro>> Post(ModuloMaestroDto modulomaestroDto)
    {
        var modulomaestro = _mapper.Map<ModuloMaestro>(modulomaestroDto);
        this._unitOfWork.ModulosMaestros.Add(modulomaestro);
        await _unitOfWork.SaveAsync();
        if (modulomaestro == null )
        {
            return BadRequest();
        }
        modulomaestroDto.Id = modulomaestroDto.Id;
        return CreatedAtAction(nameof(Post), new { id = modulomaestroDto.Id }, modulomaestroDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ModuloMaestroDto>> Get(int id)
    {
        var modulomaestro = await _unitOfWork.ModulosMaestros.GetByIdAsync(id);
        if (modulomaestro == null)
        {
            return NotFound();
        }
        return _mapper.Map<ModuloMaestroDto>(modulomaestro);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<ModuloMaestroDto>> Put(int id, [FromBody] ModuloMaestroDto modulomaestroDto)
    {
        if (modulomaestroDto == null)
            return NotFound();
        var modulosmaestros = _mapper.Map<ModuloMaestro>(modulomaestroDto);
        _unitOfWork.ModulosMaestros.Update(modulosmaestros);
        await _unitOfWork.SaveAsync();
        return modulomaestroDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var modulomaestro = await _unitOfWork.ModulosMaestros.GetByIdAsync(id);
        if (modulomaestro == null)
        {
            return NotFound();
        }
        _unitOfWork.ModulosMaestros.Remove(modulomaestro);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
