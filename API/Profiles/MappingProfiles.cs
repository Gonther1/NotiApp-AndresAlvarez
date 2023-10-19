using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Auditoria, AuditoriaDto>()
        .ReverseMap();
        
        CreateMap<BlockChain, BlockChainDto>()
        .ReverseMap();

        CreateMap<EstadoNotificacion, EstadoNotificacionDto>()
        .ReverseMap();

        CreateMap<Formato, FormatoDto>()
        .ReverseMap();

        CreateMap<GenericoVsSubmodulo, GenericoVsSubmoduloDto>()
        .ReverseMap();

        CreateMap<HiloRespuestaNotificacion, HiloRespuestaNotificacionDto>()
        .ReverseMap();

        CreateMap<MaestroVsSubmodulos, MaestroVsSubmoduloDto>()
        .ReverseMap();

        CreateMap<ModuloMaestro, ModuloMaestroDto>()
        .ReverseMap();

        CreateMap<ModuloNotificacion, ModuloNotificacionDto>()
        .ReverseMap();

        CreateMap<PermisoGenerico, PermisoGenericoDto>()
        .ReverseMap();

        CreateMap<Radicado, RadicadoDto>()
        .ReverseMap();

        CreateMap<Rol, RolDto>()
        .ReverseMap();

        CreateMap<RolVsMaestro, RolvSMaestroDto>()
        .ReverseMap();

        CreateMap<SubModulo, SubModuloDto>()
        .ReverseMap();

        CreateMap<TipoNotificacion, TipoNotificacionDto>()
        .ReverseMap();

        CreateMap<TipoRequerimiento, TipoRequerimientoDto>()
        .ReverseMap();

    }
}
