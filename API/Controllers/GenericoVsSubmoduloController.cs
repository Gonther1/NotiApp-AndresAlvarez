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

public class GenericoVsSubmoduloController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GenericoVsSubmoduloController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<GenericoVsSubmoduloDto>>> Get()
    {
        var genvssubmodulos = await _unitOfWork.GenericosVsSubmodulos.GetAllAsync();
        return _mapper.Map<List<GenericoVsSubmoduloDto>>(genvssubmodulos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<GenericoVsSubmodulo>> Post(GenericoVsSubmoduloDto genvssubmoduloDto)
    {
        var genvssubmodulos = _mapper.Map<GenericoVsSubmodulo>(genvssubmoduloDto);

        if (genvssubmodulos.FechaCreacion == DateTime.MinValue)
        {
            genvssubmodulos.FechaCreacion = DateTime.Now;
            genvssubmoduloDto.FechaCreacion = DateTime.Now;
        }
        if (genvssubmodulos.FechaModificacion == DateTime.MinValue)
        {
            genvssubmodulos.FechaModificacion = DateTime.Now;
            genvssubmoduloDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.GenericosVsSubmodulos.Add(genvssubmodulos);

        await _unitOfWork.SaveAsync();
        if (genvssubmodulos == null )
        {
            return BadRequest();
        }
        genvssubmoduloDto.Id = genvssubmodulos.Id;
        return CreatedAtAction(nameof(Post), new { id = genvssubmoduloDto.Id }, genvssubmoduloDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<GenericoVsSubmoduloDto>> Get(int id)
    {
        var genvssubmodulos = await _unitOfWork.GenericosVsSubmodulos.GetByIdAsync(id);
        if (genvssubmodulos == null)
        {
            return NotFound();
        }
        return _mapper.Map<GenericoVsSubmoduloDto>(genvssubmodulos);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<GenericoVsSubmoduloDto>> Put(int id, [FromBody] GenericoVsSubmoduloDto genvssubmoduloDto)
    {
        var genvssubmodulos = _mapper.Map<GenericoVsSubmodulo>(genvssubmoduloDto);

        if (genvssubmodulos.Id == 0)
        {
            genvssubmodulos.Id = id;
        }
        if (genvssubmodulos.Id != id)
        {
            return BadRequest();
        }
        if (genvssubmodulos == null)
        {
            return NotFound();
        }

        if (genvssubmodulos.FechaCreacion == DateTime.MinValue)
        {
            genvssubmodulos.FechaCreacion = DateTime.Now;
            genvssubmoduloDto.FechaCreacion = DateTime.Now;
        }
        if (genvssubmodulos.FechaModificacion == DateTime.MinValue)
        {
            genvssubmodulos.FechaModificacion = DateTime.Now;
            genvssubmoduloDto.FechaModificacion = DateTime.Now;  
        }

        genvssubmoduloDto.Id = genvssubmodulos.Id;
        _unitOfWork.GenericosVsSubmodulos.Update(genvssubmodulos);
        await _unitOfWork.SaveAsync();
        return genvssubmoduloDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var genvssubmodulos = await _unitOfWork.GenericosVsSubmodulos.GetByIdAsync(id);
        if (genvssubmodulos == null)
        {
            return NotFound();
        }
        _unitOfWork.GenericosVsSubmodulos.Remove(genvssubmodulos);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
