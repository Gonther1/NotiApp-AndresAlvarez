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

public class RolvSMaestroController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RolvSMaestroController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<RolvSMaestroDto>>> Get()
    {
        var rolesvsmaestro = await _unitOfWork.RolesvSMaestros.GetAllAsync();
        return _mapper.Map<List<RolvSMaestroDto>>(rolesvsmaestro);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<RolVsMaestro>> Post(RolvSMaestroDto rolvsmaestroDto)
    {
        var rolesvsmaestro = _mapper.Map<RolVsMaestro>(rolvsmaestroDto);

        if (rolesvsmaestro.FechaCreacion == DateTime.MinValue)
        {
            rolesvsmaestro.FechaCreacion = DateTime.Now;
            rolvsmaestroDto.FechaCreacion = DateTime.Now;
        }
        if (rolesvsmaestro.FechaModificacion == DateTime.MinValue)
        {
            rolesvsmaestro.FechaModificacion = DateTime.Now;
            rolvsmaestroDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.RolesvSMaestros.Add(rolesvsmaestro);

        await _unitOfWork.SaveAsync();
        if (rolesvsmaestro == null )
        {
            return BadRequest();
        }
        rolvsmaestroDto.Id = rolesvsmaestro.Id;
        return CreatedAtAction(nameof(Post), new { id = rolvsmaestroDto.Id }, rolvsmaestroDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<RolvSMaestroDto>> Get(int id)
    {
        var rolesvsmaestro = await _unitOfWork.RolesvSMaestros.GetByIdAsync(id);
        if (rolesvsmaestro == null)
        {
            return NotFound();
        }
        return _mapper.Map<RolvSMaestroDto>(rolesvsmaestro);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<RolvSMaestroDto>> Put(int id, [FromBody] RolvSMaestroDto rolvsmaestroDto)
    {
        var rolesvsmaestro = _mapper.Map<RolVsMaestro>(rolvsmaestroDto);

        if (rolesvsmaestro.Id == 0)
        {
            rolesvsmaestro.Id = id;
        }
        if (rolesvsmaestro.Id != id)
        {
            return BadRequest();
        }
        if (rolesvsmaestro == null)
        {
            return NotFound();
        }

        if (rolesvsmaestro.FechaCreacion == DateTime.MinValue)
        {
            rolesvsmaestro.FechaCreacion = DateTime.Now;
            rolvsmaestroDto.FechaCreacion = DateTime.Now;
        }
        if (rolesvsmaestro.FechaModificacion == DateTime.MinValue)
        {
            rolesvsmaestro.FechaModificacion = DateTime.Now;
            rolvsmaestroDto.FechaModificacion = DateTime.Now;  
        }

        rolvsmaestroDto.Id = rolesvsmaestro.Id;
        _unitOfWork.RolesvSMaestros.Update(rolesvsmaestro);
        await _unitOfWork.SaveAsync();
        return rolvsmaestroDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var rolesvsmaestro = await _unitOfWork.RolesvSMaestros.GetByIdAsync(id);
        if (rolesvsmaestro == null)
        {
            return NotFound();
        }
        _unitOfWork.RolesvSMaestros.Remove(rolesvsmaestro);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
