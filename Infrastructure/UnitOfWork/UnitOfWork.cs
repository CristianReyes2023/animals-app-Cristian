using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;

namespace Infrastructure.UnitOfWork
{//Está es una implementación de la unidad de trabajo (IUnitOfWork) o de las interfaces 
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private ICita _citas;
        private ICiudad _ciudades;
        private IClienteDireccion _clientedirecciones;
        private ICliente _clientes;
        private IClienteTelefono _clientetelefonos;
        private IDepartamento _departamentos;
        private IMascota _mascotas;
        private IPais _paises;
        private IRaza _razas;
        private IServicio _servicios;

        private readonly AnimalsContext _context;

        public UnitOfWork(AnimalsContext context)
        {
            _context = context;
        }

        //Construcción de metodos para implementar la clase
        public ICita Citas
        {
            get
            {
                if (_citas == null)
                {
                    _citas = new CitaRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _citas;
            }
        }
        public ICiudad Ciudades
        {
            get
            {
                if (_ciudades == null)
                {
                    _ciudades = new CiudadRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _ciudades;
            }
        }
        public IClienteDireccion ClienteDirecciones
        {
            get
            {
                if (_clientedirecciones == null)
                {
                    _clientedirecciones = new ClienteDireccionRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _clientedirecciones;
            }
        }
        public ICliente Clientes
        {
            get
            {
                if (_clientes == null)
                {
                    _clientes = new ClienteRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _clientes;
            }
        }
        public IClienteTelefono ClienteTelefonos
        {
            get
            {
                if (_clientetelefonos == null)
                {
                    _clientetelefonos = new ClienteTelefonoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _clientetelefonos;
            }
        }
        public IDepartamento Departamentos
        {
            get
            {
                if (_departamentos == null)
                {
                    _departamentos = new DepartamentoRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _departamentos;
            }
        }
        public IMascota Mascotas
        {
            get
            {
                if (_mascotas == null)
                {
                    _mascotas = new MascotaRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _mascotas;
            }
        }
        public IPais Paises
        {
            get
            {
                if (_paises == null)
                {
                    _paises = new PaisRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _paises;
            }
        }
        public IRaza Razas
        {
            get
            {
                if (_razas == null)
                {
                    _razas = new RazaRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _razas;
            }
        }
        public IServicio Servicios
        {
            get
            {
                if (_servicios == null)
                {
                    _servicios = new ServicioRepository(_context); // Remember putting the base in the repository of this entity
                }
                return _servicios;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        public Task<int> SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    } 
}