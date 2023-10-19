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

public class SubModuloController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SubModuloController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<SubModuloDto>>> Get()
    {
        var submodulos = await _unitOfWork.SubModulos.GetAllAsync();
        return _mapper.Map<List<SubModuloDto>>(submodulos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<SubModulo>> Post(SubModuloDto submoduloDto)
    {
        var submodulos = _mapper.Map<SubModulo>(submoduloDto);

        if (submodulos.FechaCreacion == DateTime.MinValue)
        {
            submodulos.FechaCreacion = DateTime.Now;
            submoduloDto.FechaCreacion = DateTime.Now;
        }
        if (submodulos.FechaModificacion == DateTime.MinValue)
        {
            submodulos.FechaModificacion = DateTime.Now;
            submoduloDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.SubModulos.Add(submodulos);

        await _unitOfWork.SaveAsync();
        if (submodulos == null )
        {
            return BadRequest();
        }
        submoduloDto.Id = submodulos.Id;
        return CreatedAtAction(nameof(Post), new { id = submoduloDto.Id }, submoduloDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<SubModuloDto>> Get(int id)
    {
        var submodulos = await _unitOfWork.SubModulos.GetByIdAsync(id);
        if (submodulos == null)
        {
            return NotFound();
        }
        return _mapper.Map<SubModuloDto>(submodulos);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<SubModuloDto>> Put(int id, [FromBody] SubModuloDto submoduloDto)
    {
        var submodulos = _mapper.Map<SubModulo>(submoduloDto);

        if (submodulos.Id == 0)
        {
            submodulos.Id = id;
        }
        if (submodulos.Id != id)
        {
            return BadRequest();
        }
        if (submodulos == null)
        {
            return NotFound();
        }

        if (submodulos.FechaCreacion == DateTime.MinValue)
        {
            submodulos.FechaCreacion = DateTime.Now;
            submoduloDto.FechaCreacion = DateTime.Now;
        }
        if (submodulos.FechaModificacion == DateTime.MinValue)
        {
            submodulos.FechaModificacion = DateTime.Now;
            submoduloDto.FechaModificacion = DateTime.Now;  
        }

        submoduloDto.Id = submodulos.Id;
        _unitOfWork.SubModulos.Update(submodulos);
        await _unitOfWork.SaveAsync();
        return submoduloDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var submodulos = await _unitOfWork.SubModulos.GetByIdAsync(id);
        if (submodulos == null)
        {
            return NotFound();
        }
        _unitOfWork.SubModulos.Remove(submodulos);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
