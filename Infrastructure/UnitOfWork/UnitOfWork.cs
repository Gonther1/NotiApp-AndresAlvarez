using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Configuration;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly NotiAppContext _context;
    private AuditoriaRepository _auditorias;
    private BlockChainRepository _blockchains;
    private EstadoNotificacionRepository _estadosnot;
    private GenericoVsSubmoduloRepository _genericosubmodulos;
    private HiloRespuestaNotificacionRepository _hilorespuesta;
    private MaestroVsSubmodulosRepository _maestrosvssubmodulos;
    private ModuloMaestroRepository _modulosmaestros;
    private ModuloNotificacionRepository _modulosnotificaciones;
    private PermisoGenericoRepository _permisosgenericos;
    private RadicadoRepository _radicados;
    private RolRepository _roles;
    private RolvSMaestroRepository _rolesvsmaestros;
    private SubModuloRepository _submodulos;
    private TipoNotificacionRepository _tiposnotificaciones;
    private TipoRequerimientoRepository _tiposrequerimientos;
    private FormatoRepository _formatos;
    
    
    public IAuditoriaRepository Auditorias
    {
        get 
        {
            if (_auditorias == null)
            {
                _auditorias = new AuditoriaRepository(_context);
            }
            return _auditorias;
        }
    }    
    public IBlockChainRepository BlockChains
    {
        get 
        {
            if (_blockchains == null)
            {
                _blockchains = new BlockChainRepository(_context);
            }
            return _blockchains;
        }
    }

    public IEstadoNotificacionRepository EstadosNotificaciones
    {
        get 
        {
            if (_estadosnot == null)
            {
                _estadosnot = new EstadoNotificacionRepository(_context);
            }
            return _estadosnot;
        }
    }
    public IGenericoVsSubmoduloRepository GenericosVsSubmodulos
    {
        get 
        {
            if (_genericosubmodulos == null)
            {
                _genericosubmodulos = new GenericoVsSubmoduloRepository(_context);
            }
            return _genericosubmodulos;
        }
    }
    public IHiloRespuestaNotificacionRepository HilosRespuestasNotificaciones
    {
        get 
        {
            if (_hilorespuesta == null)
            {
                _hilorespuesta = new HiloRespuestaNotificacionRepository(_context);
            }
            return _hilorespuesta;
        }
    }
    public IMaestroVsSubmodulosRepository MaestroVsSubmodulos
    {
        get 
        {
            if (_maestrosvssubmodulos == null)
            {
                _maestrosvssubmodulos = new MaestroVsSubmodulosRepository(_context);
            }
            return _maestrosvssubmodulos;
        }
    }

    public IModuloMaestroRepository ModulosMaestros
    {
        get 
        {
            if (_modulosmaestros == null)
            {
                _modulosmaestros = new ModuloMaestroRepository(_context);
            }
            return _modulosmaestros;
        }
    }


    public IModuloNotificacionRepository ModulosNotificaciones
    {
        get 
        {
            if (_modulosnotificaciones == null)
            {
                _modulosnotificaciones = new ModuloNotificacionRepository(_context);
            }
            return _modulosnotificaciones;
        }
    }
    public IPermisoGenericoRepository PermisosGenericos
    {
        get 
        {
            if (_permisosgenericos == null)
            {
                _permisosgenericos = new PermisoGenericoRepository(_context);
            }
            return _permisosgenericos;
        }
    }
    
    public IRolRepository Roles
    {
        get 
        {
            if (_roles == null)
            {
                _roles = new RolRepository(_context);
            }
            return _roles;
        }
    }
    public IRolvSMaestroRepository RolesvSMaestros
    {
        get 
        {
            if (_rolesvsmaestros == null)
            {
                _rolesvsmaestros = new RolvSMaestroRepository(_context);
            }
            return _rolesvsmaestros;
        }
    }
    public ISubModuloRepository SubModulos
    {
        get 
        {
            if (_submodulos == null)
            {
                _submodulos = new SubModuloRepository(_context);
            }
            return _submodulos;
        }
    }
    public ITipoNotificacionRepository TiposNotificaciones
    {
        get 
        {
            if (_tiposnotificaciones == null)
            {
                _tiposnotificaciones = new TipoNotificacionRepository(_context);
            }
            return _tiposnotificaciones;
        }
    }
    public ITipoRequerimientoRepository TiposRequerimientos
    {
        get 
        {
            if (_tiposrequerimientos == null)
            {
                _tiposrequerimientos = new TipoRequerimientoRepository(_context);
            }
            return _tiposrequerimientos;
        }
    }
    public IRadicadoRepository Radicados
    {
        get 
        {
            if (_radicados == null)
            {
                _radicados = new RadicadoRepository(_context);
            }
            return _radicados;
        }
    }
    
    public IFormatoRepository Formatos
    {
        get 
        {
            if (_formatos == null)
            {
                _formatos = new FormatoRepository(_context);
            }
            return _formatos;
        }
    } 
    public UnitOfWork(NotiAppContext context)
    {
        _context = context;
    }
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
    public void Dispose()
    {
        _context.Dispose();
    }
}