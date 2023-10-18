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

public class BlockChainController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public BlockChainController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<IEnumerable<BlockChainDto>>> Get()
    {
        var blockchains = await _unitOfWork.BlockChains.GetAllAsync();
        return _mapper.Map<List<BlockChainDto>>(blockchains);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<BlockChain>> Post(BlockChainDto blockchainDto)
    {
        var blockchains = _mapper.Map<BlockChain>(blockchainDto);

        if (blockchains.FechaCreacion == DateTime.MinValue)
        {
            blockchains.FechaCreacion = DateTime.Now;
            blockchainDto.FechaCreacion = DateTime.Now;
        }
        if (blockchains.FechaModificacion == DateTime.MinValue)
        {
            blockchains.FechaModificacion = DateTime.Now;
            blockchainDto.FechaModificacion = DateTime.Now;  
        }

        _unitOfWork.BlockChains.Add(blockchains);

        await _unitOfWork.SaveAsync();
        if (blockchains == null )
        {
            return BadRequest();
        }
        blockchainDto.Id = blockchains.Id;
        return CreatedAtAction(nameof(Post), new { id = blockchainDto.Id }, blockchainDto);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<BlockChainDto>> Get(int id)
    {
        var blockchains = await _unitOfWork.BlockChains.GetByIdAsync(id);
        if (blockchains == null)
        {
            return NotFound();
        }
        return _mapper.Map<BlockChainDto>(blockchains);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<ActionResult<BlockChainDto>> Put(int id, [FromBody] BlockChainDto blockchainDto)
    {
        var blockchains = _mapper.Map<BlockChain>(blockchainDto);

        if (blockchains.Id == 0)
        {
            blockchains.Id = id;
        }
        if (blockchains.Id != id)
        {
            return BadRequest();
        }
        if (blockchains == null)
        {
            return NotFound();
        }

        if (blockchains.FechaCreacion == DateTime.MinValue)
        {
            blockchains.FechaCreacion = DateTime.Now;
            blockchainDto.FechaCreacion = DateTime.Now;
        }
        if (blockchains.FechaModificacion == DateTime.MinValue)
        {
            blockchains.FechaModificacion = DateTime.Now;
            blockchainDto.FechaModificacion = DateTime.Now;  
        }

        blockchainDto.Id = blockchains.Id;
        _unitOfWork.BlockChains.Update(blockchains);
        await _unitOfWork.SaveAsync();
        return blockchainDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var blockchains = await _unitOfWork.BlockChains.GetByIdAsync(id);
        if (blockchains == null)
        {
            return NotFound();
        }
        _unitOfWork.BlockChains.Remove(blockchains);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }
}
