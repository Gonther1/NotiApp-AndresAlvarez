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


public class MaestroVsSubmoduloController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public MaestroVsSubmoduloController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<MaestroVsSubmoduloDto>>> Get()
    {
        var maestrosvssubmodulos = await _unitOfWork.MaestroVsSubmodulos.GetAllAsync();
        return _mapper.Map<List<MaestroVsSubmoduloDto>>(maestrosvssubmodulos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<MaestroVsSubmodulos>> Post(MaestroVsSubmoduloDto maestrovssubmoduloDto)
    {
        var maestrovssubmodulo = _mapper.Map<MaestroVsSubmodulos>(maestrovssubmoduloDto);
        this._unitOfWork.MaestroVsSubmodulos.Add(maestrovssubmodulo);
        await _unitOfWork.SaveAsync();
        if (maestrovssubmodulo == null )
        {
            return BadRequest();
        }
        maestrovssubmoduloDto.Id = maestrovssubmoduloDto.Id;
        return CreatedAtAction(nameof(Post), new { id = maestrovssubmoduloDto.Id }, maestrovssubmoduloDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MaestroVsSubmoduloDto>> Get(int id)
    {
        var maestrovssubmodulo = await _unitOfWork.MaestroVsSubmodulos.GetByIdAsync(id);
        if (maestrovssubmodulo == null)
        {
            return NotFound();
        }
        return _mapper.Map<MaestroVsSubmoduloDto>(maestrovssubmodulo);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<MaestroVsSubmoduloDto>> Put(int id, [FromBody] MaestroVsSubmoduloDto maestrovssubmoduloDto)
    {
        if (maestrovssubmoduloDto == null)
            return NotFound();
        var maestrosvssubmodulos = _mapper.Map<MaestroVsSubmodulos>(maestrovssubmoduloDto);
        _unitOfWork.MaestroVsSubmodulos.Update(maestrosvssubmodulos);
        await _unitOfWork.SaveAsync();
        return maestrovssubmoduloDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var maestrovssubmodulo = await _unitOfWork.MaestroVsSubmodulos.GetByIdAsync(id);
        if (maestrovssubmodulo == null)
        {
            return NotFound();
        }
        _unitOfWork.MaestroVsSubmodulos.Remove(maestrovssubmodulo);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
