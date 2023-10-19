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

public class PermisoGenericoController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PermisoGenericoController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<PermisoGenericoDto>>> Get()
    {
        var permisosgenericos = await _unitOfWork.PermisosGenericos.GetAllAsync();
        return _mapper.Map<List<PermisoGenericoDto>>(permisosgenericos);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<PermisoGenerico>> Post(PermisoGenericoDto permisogenricoDto)
    {
        var permisosgenericos = _mapper.Map<PermisoGenerico>(permisogenricoDto);

        if (permisosgenericos.FechaCreacion == DateTime.MinValue)
        {
            permisosgenericos.FechaCreacion = DateTime.Now;
            permisogenricoDto.FechaCreacion = DateTime.Now;
        }
        if (permisosgenericos.FechaModificacion == DateTime.MinValue)
        {
            permisosgenericos.FechaModificacion = DateTime.Now;
            permisogenricoDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.PermisosGenericos.Add(permisosgenericos);

        await _unitOfWork.SaveAsync();
        if (permisosgenericos == null )
        {
            return BadRequest();
        }
        permisogenricoDto.Id = permisosgenericos.Id;
        return CreatedAtAction(nameof(Post), new { id = permisogenricoDto.Id }, permisogenricoDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PermisoGenericoDto>> Get(int id)
    {
        var permisosgenericos = await _unitOfWork.PermisosGenericos.GetByIdAsync(id);
        if (permisosgenericos == null)
        {
            return NotFound();
        }
        return _mapper.Map<PermisoGenericoDto>(permisosgenericos);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<PermisoGenericoDto>> Put(int id, [FromBody] PermisoGenericoDto permisogenricoDto)
    {
        var permisosgenericos = _mapper.Map<PermisoGenerico>(permisogenricoDto);

        if (permisosgenericos.Id == 0)
        {
            permisosgenericos.Id = id;
        }
        if (permisosgenericos.Id != id)
        {
            return BadRequest();
        }
        if (permisosgenericos == null)
        {
            return NotFound();
        }

        if (permisosgenericos.FechaCreacion == DateTime.MinValue)
        {
            permisosgenericos.FechaCreacion = DateTime.Now;
            permisogenricoDto.FechaCreacion = DateTime.Now;
        }
        if (permisosgenericos.FechaModificacion == DateTime.MinValue)
        {
            permisosgenericos.FechaModificacion = DateTime.Now;
            permisogenricoDto.FechaModificacion = DateTime.Now;  
        }

        permisogenricoDto.Id = permisosgenericos.Id;
        _unitOfWork.PermisosGenericos.Update(permisosgenericos);
        await _unitOfWork.SaveAsync();
        return permisogenricoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var permisosgenericos = await _unitOfWork.PermisosGenericos.GetByIdAsync(id);
        if (permisosgenericos == null)
        {
            return NotFound();
        }
        _unitOfWork.PermisosGenericos.Remove(permisosgenericos);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
