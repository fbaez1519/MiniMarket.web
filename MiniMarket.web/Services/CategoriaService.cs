using AutoMapper;
using MiniMarket.web.DTOs;
using Minimarket.Domain.Entities;

namespace MiniMarket.web.Services
{
    /// <summary>
    /// Servicio para la gestión de categorías
    /// </summary>
    public class CategoriaService : ICategoriaService
    {
        private static List<Categoria> _categorias = new List<Categoria>();
        private static int _nextId = 1;
        private readonly IMapper _mapper;

        public CategoriaService(IMapper mapper)
        {
            _mapper = mapper;

            // Datos de ejemplo
            if (!_categorias.Any())
            {
                _categorias.AddRange(new List<Categoria>
                {
                    new Categoria
                    {
                        Id = _nextId++,
                        Nombre = "Lácteos",
                        Descripcion = "Productos lácteos como leche, queso, yogur",
                        Codigo = "CAT-001",
                        EsPrincipal = true,
                        Orden = 1,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EstaActivo = true
                    },
                    new Categoria
                    {
                        Id = _nextId++,
                        Nombre = "Panadería",
                        Descripcion = "Pan, pasteles y productos de panadería",
                        Codigo = "CAT-002",
                        EsPrincipal = true,
                        Orden = 2,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EstaActivo = true
                    },
                    new Categoria
                    {
                        Id = _nextId++,
                        Nombre = "Bebidas",
                        Descripcion = "Bebidas frías y calientes",
                        Codigo = "CAT-003",
                        EsPrincipal = true,
                        Orden = 3,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EstaActivo = true
                    },
                    new Categoria
                    {
                        Id = _nextId++,
                        Nombre = "Leche Entera",
                        Descripcion = "Leche entera pasteurizada",
                        Codigo = "CAT-001-01",
                        CategoriaPadreId = 1,
                        EsPrincipal = false,
                        Orden = 1,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        EstaActivo = true
                    }
                });
            }
        }

        public async Task<IEnumerable<CategoriaDTO>> GetAllAsync()
        {
            var categorias = _categorias.OrderBy(c => c.Orden).ThenBy(c => c.Nombre);
            return await Task.FromResult(_mapper.Map<IEnumerable<CategoriaDTO>>(categorias));
        }

        public async Task<CategoriaDTO?> GetByIdAsync(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return null;

            var dto = _mapper.Map<CategoriaDTO>(categoria);
            
            // Obtener nombre de la categoría padre si existe
            if (categoria.CategoriaPadreId.HasValue)
            {
                var padre = _categorias.FirstOrDefault(c => c.Id == categoria.CategoriaPadreId.Value);
                if (padre != null)
                    dto.CategoriaPadreNombre = padre.Nombre;
            }

            // Contar productos asociados
            dto.CantidadProductos = 0; // Se actualizará cuando exista el repositorio de productos

            return dto;
        }

        public async Task<CategoriaDTO?> GetByCodigoAsync(string codigo)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Codigo == codigo);
            if (categoria == null)
                return null;

            return _mapper.Map<CategoriaDTO>(categoria);
        }

        public async Task<IEnumerable<CategoriaDTO>> GetPrincipalesAsync()
        {
            var principales = _categorias.Where(c => c.EsPrincipal && c.CategoriaPadreId == null)
                                         .OrderBy(c => c.Orden)
                                         .ThenBy(c => c.Nombre);
            return await Task.FromResult(_mapper.Map<IEnumerable<CategoriaDTO>>(principales));
        }

        public async Task<IEnumerable<CategoriaDTO>> GetSubCategoriasAsync(int categoriaPadreId)
        {
            var subCategorias = _categorias.Where(c => c.CategoriaPadreId == categoriaPadreId)
                                           .OrderBy(c => c.Orden)
                                           .ThenBy(c => c.Nombre);
            return await Task.FromResult(_mapper.Map<IEnumerable<CategoriaDTO>>(subCategorias));
        }

        public async Task<CategoriaDTO> CreateAsync(CategoriaCreateDTO categoriaDto)
        {
            // Validar nombre único
            if (_categorias.Any(c => c.Nombre.ToLower() == categoriaDto.Nombre.ToLower()))
                throw new Exception($"Ya existe una categoría con el nombre {categoriaDto.Nombre}");

            // Validar código único
            if (!string.IsNullOrEmpty(categoriaDto.Codigo) && _categorias.Any(c => c.Codigo == categoriaDto.Codigo))
                throw new Exception($"Ya existe una categoría con el código {categoriaDto.Codigo}");

            // Validar que la categoría padre exista
            if (categoriaDto.CategoriaPadreId.HasValue)
            {
                var padre = _categorias.FirstOrDefault(c => c.Id == categoriaDto.CategoriaPadreId.Value);
                if (padre == null)
                    throw new Exception($"La categoría padre con ID {categoriaDto.CategoriaPadreId.Value} no existe");
            }

            var categoria = _mapper.Map<Categoria>(categoriaDto);
            categoria.Id = _nextId++;
            categoria.CreatedAt = DateTime.UtcNow;
            categoria.UpdatedAt = DateTime.UtcNow;
            categoria.EstaActivo = true;

            _categorias.Add(categoria);

            return await Task.FromResult(_mapper.Map<CategoriaDTO>(categoria));
        }

        public async Task<CategoriaDTO> UpdateAsync(CategoriaUpdateDTO categoriaDto)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == categoriaDto.Id);
            if (categoria == null)
                throw new Exception($"Categoría con ID {categoriaDto.Id} no encontrada");

            // Validar nombre único (excepto la misma categoría)
            if (_categorias.Any(c => c.Nombre.ToLower() == categoriaDto.Nombre.ToLower() && c.Id != categoriaDto.Id))
                throw new Exception($"Ya existe otra categoría con el nombre {categoriaDto.Nombre}");

            // Validar código único (excepto la misma categoría)
            if (!string.IsNullOrEmpty(categoriaDto.Codigo) && _categorias.Any(c => c.Codigo == categoriaDto.Codigo && c.Id != categoriaDto.Id))
                throw new Exception($"Ya existe otra categoría con el código {categoriaDto.Codigo}");

            // Validar que la categoría padre exista
            if (categoriaDto.CategoriaPadreId.HasValue)
            {
                // No permitir que una categoría sea su propio padre
                if (categoriaDto.CategoriaPadreId.Value == categoriaDto.Id)
                    throw new Exception("Una categoría no puede ser su propio padre");

                var padre = _categorias.FirstOrDefault(c => c.Id == categoriaDto.CategoriaPadreId.Value);
                if (padre == null)
                    throw new Exception($"La categoría padre con ID {categoriaDto.CategoriaPadreId.Value} no existe");
            }

            // Actualizar propiedades
            categoria.Nombre = categoriaDto.Nombre;
            categoria.Descripcion = categoriaDto.Descripcion;
            categoria.Codigo = categoriaDto.Codigo;
            categoria.CategoriaPadreId = categoriaDto.CategoriaPadreId;
            categoria.EsPrincipal = categoriaDto.EsPrincipal;
            categoria.Orden = categoriaDto.Orden;
            categoria.EstaActivo = categoriaDto.EstaActivo;
            categoria.UpdatedAt = DateTime.UtcNow;

            return await Task.FromResult(_mapper.Map<CategoriaDTO>(categoria));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return await Task.FromResult(false);

            // Verificar si tiene subcategorías
            var tieneSubCategorias = _categorias.Any(c => c.CategoriaPadreId == id);
            if (tieneSubCategorias)
                throw new Exception("No se puede eliminar una categoría que tiene subcategorías");

            _categorias.Remove(categoria);
            return await Task.FromResult(true);
        }

        public async Task<bool> DesactivarAsync(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return await Task.FromResult(false);

            categoria.EstaActivo = false;
            categoria.UpdatedAt = DateTime.UtcNow;
            return await Task.FromResult(true);
        }

        public async Task<bool> ActivarAsync(int id)
        {
            var categoria = _categorias.FirstOrDefault(c => c.Id == id);
            if (categoria == null)
                return await Task.FromResult(false);

            categoria.EstaActivo = true;
            categoria.UpdatedAt = DateTime.UtcNow;
            return await Task.FromResult(true);
        }
    }
}