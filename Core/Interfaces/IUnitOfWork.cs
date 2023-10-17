using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces;

public interface IUnitOfWork
{
    IAuditoriaRepository Auditorias { get; }
    IBlockChainRepository BlockChains { get; }
    IEstadoNotificacionRepository EstadosNotificaciones { get; }
    IFormatoRepository Formatos { get; }
    IGenericoVsSubmoduloRepository GenericosVsSubmodulos { get; }
    IHiloRespuestaNotificacionRepository HilosRespuestasNotificaciones { get; }
    IMaestroVsSubmodulosRepository MaestroVsSubmodulos { get; }
    IModuloMaestroRepository ModulosMaestros { get; }
    IModuloNotificacionRepository ModulosNotificaciones { get; }
    IRadicadoRepository Radicados { get; }
    IRolRepository Roles { get; }
    IRolvSMaestroRepository RolesvSMaestros { get; }
    ISubModuloRepository SubModulos { get; }
    ITipoNotificacionRepository TiposNotificaciones { get; }
    ITipoRequerimientoRepository TiposRequerimientos { get; }
    IPermisoGenericoRepository PermisosGenericos { get; }
    Task<int> SaveAsync();
}
