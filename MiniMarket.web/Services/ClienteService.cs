using AutoMapper;
using MiniMarket.web.DTOs;
using Minimarket.Domain.Entities;

namespace MiniMarket.web.Services
{
    /// <summary>
    /// Servicio para la gestión de clientes
    /// </summary>
    public class ClienteService : IClienteService
    {
        private static List<Cliente> _clientes = new List<Cliente>();
        private static int _nextId = 1;
        private readonly IMapper _mapper;

        public ClienteService(IMapper mapper)
        {
            _mapper = mapper;

            // Agregar datos de ejemplo si no hay ninguno
            if (!_clientes.Any())
            {
                _clientes.AddRange(new List<Cliente>
                {
                    new Cliente
                    {
                        Id = _nextId++,
                        Documento = "001-1234567-8",
                        NombreCompleto = "Juan Pérez",
                        Telefono = "809-555-0101",
                        Email = "juan@email.com",
                        Direccion = "Calle Principal #123, Santo Domingo",
                        TipoCliente = "VIP",
                        FechaRegistro = DateTime.UtcNow.AddDays(-30),
                        DescuentoEspecial = 10,
                        LimiteCredito = 5000,
                        Saldo = 0,
                        Genero = "M",
                        FechaNacimiento = new DateTime(1985, 5, 15),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EstaActivo = true
                    },
                    new Cliente
                    {
                        Id = _nextId++,
                        Documento = "001-2345678-9",
                        NombreCompleto = "María Rodríguez",
                        Telefono = "809-555-0102",
                        Email = "maria@email.com",
                        Direccion = "Avenida Libertad #45, Santiago",
                        TipoCliente = "Regular",
                        FechaRegistro = DateTime.UtcNow.AddDays(-15),
                        DescuentoEspecial = null,
                        LimiteCredito = null,
                        Saldo = 150.50m,
                        Genero = "F",
                        FechaNacimiento = new DateTime(1990, 8, 22),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EstaActivo = true
                    },
                    new Cliente
                    {
                        Id = _nextId++,
                        Documento = "001-3456789-0",
                        NombreCompleto = "Pedro Sánchez",
                        Telefono = "809-555-0103",
                        Email = "pedro@email.com",
                        Direccion = "Calle Secundaria #78, La Vega",
                        TipoCliente = "Mayorista",
                        FechaRegistro = DateTime.UtcNow.AddDays(-7),
                        DescuentoEspecial = 15,
                        LimiteCredito = 10000,
                        Saldo = 2500.00m,
                        Genero = "M",
                        FechaNacimiento = new DateTime(1980, 11, 10),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EstaActivo = true
                    }
                });
            }
        }

        public async Task<IEnumerable<ClienteDTO>> GetAllAsync()
        {
            var clientes = _clientes.OrderByDescending(c => c.Id);
            return await Task.FromResult(_mapper.Map<IEnumerable<ClienteDTO>>(clientes));
        }

        public async Task<ClienteDTO> GetByIdAsync(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            return await Task.FromResult(_mapper.Map<ClienteDTO>(cliente));
        }

        public async Task<ClienteDTO> GetByDocumentoAsync(string documento)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Documento == documento);
            return await Task.FromResult(_mapper.Map<ClienteDTO>(cliente));
        }

        public async Task<ClienteDTO> GetByEmailAsync(string email)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Email.ToLower() == email.ToLower());
            return await Task.FromResult(_mapper.Map<ClienteDTO>(cliente));
        }

        public async Task<ClienteDTO> CreateAsync(ClienteCreateDTO clienteDto)
        {
            // Validar documento único
            if (_clientes.Any(c => c.Documento == clienteDto.Documento))
                throw new Exception($"Ya existe un cliente con el documento {clienteDto.Documento}");

            // Validar email único
            if (_clientes.Any(c => c.Email.ToLower() == clienteDto.Email.ToLower()))
                throw new Exception($"Ya existe un cliente con el email {clienteDto.Email}");

            var cliente = _mapper.Map<Cliente>(clienteDto);
            cliente.Id = _nextId++;
            cliente.FechaRegistro = DateTime.UtcNow;
            cliente.Saldo = 0;
            cliente.CreatedAt = DateTime.UtcNow;
            cliente.UpdatedAt = DateTime.UtcNow;
            cliente.EstaActivo = true;

            _clientes.Add(cliente);

            return await Task.FromResult(_mapper.Map<ClienteDTO>(cliente));
        }

        public async Task<ClienteDTO> UpdateAsync(ClienteUpdateDTO clienteDto)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == clienteDto.Id);
            if (cliente == null)
                throw new Exception($"Cliente con ID {clienteDto.Id} no encontrado");

            // Validar documento único (excepto el mismo cliente)
            if (_clientes.Any(c => c.Documento == clienteDto.Documento && c.Id != clienteDto.Id))
                throw new Exception($"Ya existe otro cliente con el documento {clienteDto.Documento}");

            // Validar email único (excepto el mismo cliente)
            if (_clientes.Any(c => c.Email.ToLower() == clienteDto.Email.ToLower() && c.Id != clienteDto.Id))
                throw new Exception($"Ya existe otro cliente con el email {clienteDto.Email}");

            // Actualizar propiedades
            cliente.Documento = clienteDto.Documento;
            cliente.NombreCompleto = clienteDto.NombreCompleto;
            cliente.Telefono = clienteDto.Telefono;
            cliente.Email = clienteDto.Email;
            cliente.Direccion = clienteDto.Direccion;
            cliente.TipoCliente = clienteDto.TipoCliente;
            cliente.DescuentoEspecial = clienteDto.DescuentoEspecial;
            cliente.LimiteCredito = clienteDto.LimiteCredito;
            cliente.Genero = clienteDto.Genero;
            cliente.FechaNacimiento = clienteDto.FechaNacimiento;
            cliente.EstaActivo = clienteDto.EstaActivo;
            cliente.UpdatedAt = DateTime.UtcNow;

            return await Task.FromResult(_mapper.Map<ClienteDTO>(cliente));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
                return await Task.FromResult(false);

            _clientes.Remove(cliente);
            return await Task.FromResult(true);
        }

        public async Task<bool> DesactivarAsync(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
                return await Task.FromResult(false);

            cliente.EstaActivo = false;
            cliente.UpdatedAt = DateTime.UtcNow;
            return await Task.FromResult(true);
        }

        public async Task<bool> ActivarAsync(int id)
        {
            var cliente = _clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
                return await Task.FromResult(false);

            cliente.EstaActivo = true;
            cliente.UpdatedAt = DateTime.UtcNow;
            return await Task.FromResult(true);
        }
    }
}