using SistemaInventario.Models;

namespace SistemaInventario.Interfaces
{
    // Interfaz que define el CONTRATO del repositorio de inventario
    // Define QUÉ operaciones existen, sin importar CÓMO se implementan
    // Hoy guardamos en memoria — mañana podría ser base de datos, archivo, API
    // El resto del programa no necesita saber cómo se guardan los datos
    public interface IInventarioRepositorio
    {
        // Operaciones con Productos
        void AgregarProducto(Producto producto);
        bool EliminarProducto(int id);
        Producto? BuscarPorId(int id);                        // ? = puede devolver null
        Producto? BuscarPorNombre(string nombre);
        IEnumerable<Producto> ObtenerTodos();
        IEnumerable<Producto> ObtenerPorCategoria(int categoriaId);
        IEnumerable<Producto> ObtenerConStockBajo(int minimo);

        // Operaciones con Categorías
        void AgregarCategoria(Categoria categoria);
        IEnumerable<Categoria> ObtenerCategorias();
        Categoria? BuscarCategoriaPorId(int id);

        // Estadísticas
        double CalcularValorTotalInventario();
        int ContarProductos();
    }
}
