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
        var gvssubmodulos = await _unitOfWork.GenericosVsSubmodulos.GetAllAsync();
        return _mapper.Map<List<GenericoVsSubmoduloDto>>(gvssubmodulos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<GenericoVsSubmodulo>> Post(GenericoVsSubmoduloDto gvssubmoduloDto)
    {
        var gvssubmodulo = _mapper.Map<GenericoVsSubmodulo>(gvssubmoduloDto);
        this._unitOfWork.GenericosVsSubmodulos.Add(gvssubmodulo);
        await _unitOfWork.SaveAsync();
        if (gvssubmodulo == null )
        {
            return BadRequest();
        }
        gvssubmoduloDto.Id = gvssubmoduloDto.Id;
        return CreatedAtAction(nameof(Post), new { id = gvssubmoduloDto.Id }, gvssubmoduloDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<GenericoVsSubmoduloDto>> Get(int id)
    {
        var gvssubmodulo = await _unitOfWork.GenericosVsSubmodulos.GetByIdAsync(id);
        if (gvssubmodulo == null)
        {
            return NotFound();
        }
        return _mapper.Map<GenericoVsSubmoduloDto>(gvssubmodulo);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<GenericoVsSubmoduloDto>> Put(int id, [FromBody] GenericoVsSubmoduloDto gvssubmoduloDto)
    {
        if (gvssubmoduloDto == null)
            return NotFound();
        var gvssubmodulos = _mapper.Map<GenericoVsSubmodulo>(gvssubmoduloDto);
        _unitOfWork.GenericosVsSubmodulos.Update(gvssubmodulos);
        await _unitOfWork.SaveAsync();
        return gvssubmoduloDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var gvssubmodulo = await _unitOfWork.GenericosVsSubmodulos.GetByIdAsync(id);
        if (gvssubmodulo == null)
        {
            return NotFound();
        }
        _unitOfWork.GenericosVsSubmodulos.Remove(gvssubmodulo);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
