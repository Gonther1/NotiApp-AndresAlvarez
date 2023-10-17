using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IUnitOfWork
{
    IAuditoriaRepository Auditorias { get; }
    IBlockChainRepository BlockChain { get; }
    IEstadoNotificacionRepository EstadosNotificaciones { get; }
    IFormatoRepository Formatos { get; }
    IGenericoVsSubmoduloRepository GenericosVsSubmodulos { get; }
    IHiloRespuestaRepository HilosRespuestas { get; }
    IMaestroVsSubmodulosRepository Auditorias { get; }
    IModuloMaestroRepository ModulosMaestros { get; }
    IModuloNotificacionRepository ModulosNotificaciones { get; }
    IRadicadoRepository Radicados { get; }
    IRolRepository Roles { get; }
    IRolvSMaestro RolesvSMaestros { get; }
    ISubModuloRepository Submodulos { get; }
    ITipoNotificacionRepository TiposNotificaciones { get; }
    ITipoRequerimientoRepository TiposRequerimientos { get; }
    Task<int> SaveAsync();
}
