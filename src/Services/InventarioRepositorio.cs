using SistemaInventario.Interfaces;
using SistemaInventario.Models;

namespace SistemaInventario.Services
{
    // Esta clase IMPLEMENTA la interfaz IInventarioRepositorio
    // Guarda los datos EN MEMORIA (en colecciones)
    // Si mañana queremos guardar en base de datos, creamos otra clase
    // que implemente la misma interfaz — el resto del programa no cambia
    public class InventarioRepositorio : IInventarioRepositorio
    {
        // Dictionary para productos: clave = ID, valor = Producto
        // Elegimos Dictionary porque buscamos frecuentemente por ID
        // La búsqueda en Dictionary es O(1) — instantánea sin importar cuántos haya
        private Dictionary<int, Producto> productos = new Dictionary<int, Producto>();

        // Dictionary para categorías: clave = ID, valor = Categoria
        private Dictionary<int, Categoria> categorias = new Dictionary<int, Categoria>();

        // ─────────────────────────────────────────────
        // OPERACIONES CON CATEGORÍAS
        // ─────────────────────────────────────────────

        public void AgregarCategoria(Categoria categoria)
        {
            // ContainsKey verifica si ya existe una categoría con ese ID
            if (categorias.ContainsKey(categoria.Id))
                throw new InvalidOperationException($"Ya existe una categoría con ID {categoria.Id}.");

            categorias.Add(categoria.Id, categoria);
        }

        public IEnumerable<Categoria> ObtenerCategorias()
        {
            // Values devuelve solo los valores del Dictionary (sin las claves)
            return categorias.Values;
        }

        public Categoria? BuscarCategoriaPorId(int id)
        {
            // TryGetValue: forma segura de buscar en un Dictionary
            // Si encuentra el ID, devuelve true y guarda el valor en "categoria"
            // Si no lo encuentra, devuelve false y "categoria" queda en null
            categorias.TryGetValue(id, out Categoria? categoria);
            return categoria;
        }

        // ─────────────────────────────────────────────
        // OPERACIONES CON PRODUCTOS
        // ─────────────────────────────────────────────

        public void AgregarProducto(Producto producto)
        {
            if (productos.ContainsKey(producto.Id))
                throw new InvalidOperationException($"Ya existe un producto con ID {producto.Id}.");

            productos.Add(producto.Id, producto);
        }

        public bool EliminarProducto(int id)
        {
            // Remove devuelve true si lo eliminó, false si no existía
            return productos.Remove(id);
        }

        public Producto? BuscarPorId(int id)
        {
            productos.TryGetValue(id, out Producto? producto);
            return producto;
        }

        public Producto? BuscarPorNombre(string nombre)
        {
            // LINQ: FirstOrDefault recorre la colección y devuelve el PRIMERO
            // que cumpla la condición, o null si ninguno la cumple
            // OrdinalIgnoreCase = ignora mayúsculas/minúsculas en la comparación
            return productos.Values
                .FirstOrDefault(p => p.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Producto> ObtenerTodos()
        {
            // OrderBy ordena los productos por nombre antes de devolverlos
            return productos.Values.OrderBy(p => p.Nombre);
        }

        public IEnumerable<Producto> ObtenerPorCategoria(int categoriaId)
        {
            // Where filtra solo los productos de esa categoría
            return productos.Values
                .Where(p => p.Categoria.Id == categoriaId)
                .OrderBy(p => p.Nombre);
        }

        public IEnumerable<Producto> ObtenerConStockBajo(int minimo)
        {
            // Filtra productos cuyo stock sea MENOR O IGUAL al mínimo indicado
            return productos.Values
                .Where(p => p.Stock <= minimo)
                .OrderBy(p => p.Stock); // Ordena del más bajo al más alto
        }

        // ─────────────────────────────────────────────
        // ESTADÍSTICAS
        // ─────────────────────────────────────────────

        public double CalcularValorTotalInventario()
        {
            // Sum aplica ValorTotal() a cada producto y suma todos los resultados
            return productos.Values.Sum(p => p.ValorTotal());
        }

        public int ContarProductos()
        {
            return productos.Count;
        }
    }
}
