using SistemaInventario.Interfaces;
using SistemaInventario.Models;

namespace SistemaInventario.UI
{
    // Esta clase maneja TODA la interacción con el usuario
    // No sabe cómo se guardan los datos — solo sabe mostrar y pedir información
    // Recibe un IInventarioRepositorio (la interfaz, no la implementación)
    // Esto es Dependency Injection en su forma más básica
    public class Menu
    {
        // Guardamos la referencia al repositorio — lo inyectamos en el constructor
        private readonly IInventarioRepositorio repositorio;

        public Menu(IInventarioRepositorio repositorio)
        {
            this.repositorio = repositorio;
        }

        // ─────────────────────────────────────────────
        // PUNTO DE ENTRADA DEL MENÚ
        // ─────────────────────────────────────────────

        public void Iniciar()
        {
            CargarDatosIniciales(); // Precargamos categorías y productos de ejemplo

            bool salir = false;

            while (!salir)
            {
                MostrarMenuPrincipal();
                string opcion = Console.ReadLine()?.Trim() ?? "";

                // Switch expression — forma moderna del switch en C#
                switch (opcion)
                {
                    case "1": MostrarTodosLosProductos(); break;
                    case "2": AgregarProducto(); break;
                    case "3": BuscarProducto(); break;
                    case "4": ActualizarStock(); break;
                    case "5": EliminarProducto(); break;
                    case "6": MostrarProductosPorCategoria(); break;
                    case "7": MostrarStockBajo(); break;
                    case "8": MostrarResumen(); break;
                    case "9": salir = true; break;
                    default:
                        MostrarError("Opción no válida. Ingresa un número del 1 al 9.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresiona Enter para continuar...");
                    Console.ReadLine();
                }
            }

            Console.Clear();
            Console.WriteLine("👋 ¡Hasta luego! Sistema cerrado correctamente.");
        }

        // ─────────────────────────────────────────────
        // MENÚ PRINCIPAL
        // ─────────────────────────────────────────────

        private void MostrarMenuPrincipal()
        {
            Console.Clear();
            MostrarEncabezado("SISTEMA DE GESTIÓN DE INVENTARIO");
            Console.WriteLine("  1. 📋 Ver todos los productos");
            Console.WriteLine("  2. ➕ Agregar producto");
            Console.WriteLine("  3. 🔍 Buscar producto");
            Console.WriteLine("  4. 📦 Actualizar stock");
            Console.WriteLine("  5. 🗑️  Eliminar producto");
            Console.WriteLine("  6. 🏷️  Ver por categoría");
            Console.WriteLine("  7. ⚠️  Stock bajo");
            Console.WriteLine("  8. 📊 Resumen del inventario");
            Console.WriteLine("  9. 🚪 Salir");
            Console.WriteLine(new string('─', 50));
            Console.Write("  Selecciona una opción: ");
        }

        // ─────────────────────────────────────────────
        // OPCIÓN 1: VER TODOS LOS PRODUCTOS
        // ─────────────────────────────────────────────

        private void MostrarTodosLosProductos()
        {
            Console.Clear();
            MostrarEncabezado("LISTADO COMPLETO DE PRODUCTOS");

            var productos = repositorio.ObtenerTodos();

            if (!productos.Any()) // Any() devuelve false si la colección está vacía
            {
                MostrarAviso("No hay productos registrados.");
                return;
            }

            // Encabezado de columnas
            Console.WriteLine($"  {"ID",-6} {"Nombre",-25} {"Categoría",-15} {"Precio",10} {"Stock",6}");
            Console.WriteLine(new string('─', 70));

            foreach (var producto in productos)
            {
                // Coloreamos en amarillo si el stock es bajo (≤ 5)
                if (producto.Stock <= 5)
                    Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine($"  {producto}");
                Console.ResetColor();
            }

            Console.WriteLine(new string('─', 70));
            Console.WriteLine($"  Total: {repositorio.ContarProductos()} productos");
        }

        // ─────────────────────────────────────────────
        // OPCIÓN 2: AGREGAR PRODUCTO
        // ─────────────────────────────────────────────

        private void AgregarProducto()
        {
            Console.Clear();
            MostrarEncabezado("AGREGAR NUEVO PRODUCTO");

            // Mostramos las categorías disponibles para que el usuario elija
            var categorias = repositorio.ObtenerCategorias().ToList();
            Console.WriteLine("  Categorías disponibles:");
            foreach (var cat in categorias)
                Console.WriteLine($"    {cat}");
            Console.WriteLine();

            try
            {
                // Leemos cada dato con su validación correspondiente
                string nombre = LeerTexto("Nombre del producto");
                double precio = LeerDouble("Precio unitario");
                int stock = LeerEntero("Stock inicial");
                int categoriaId = LeerEntero("ID de categoría");

                // Verificamos que la categoría exista
                Categoria? categoria = repositorio.BuscarCategoriaPorId(categoriaId);
                if (categoria == null)
                {
                    MostrarError("Categoría no encontrada.");
                    return;
                }

                // Creamos el producto — el constructor valida los datos internamente
                Producto nuevo = new Producto(nombre, precio, stock, categoria);
                repositorio.AgregarProducto(nuevo);

                MostrarExito($"Producto '{nombre}' agregado correctamente con ID {nuevo.Id:D3}.");
            }
            catch (ArgumentException ex)
            {
                // Capturamos los errores de validación que lanza el modelo
                MostrarError($"Dato inválido: {ex.Message}");
            }
        }

        // ─────────────────────────────────────────────
        // OPCIÓN 3: BUSCAR PRODUCTO
        // ─────────────────────────────────────────────

        private void BuscarProducto()
        {
            Console.Clear();
            MostrarEncabezado("BUSCAR PRODUCTO");

            Console.WriteLine("  1. Buscar por ID");
            Console.WriteLine("  2. Buscar por nombre");
            Console.Write("  Opción: ");
            string opcion = Console.ReadLine()?.Trim() ?? "";

            Producto? encontrado = null;

            if (opcion == "1")
            {
                int id = LeerEntero("ID del producto");
                encontrado = repositorio.BuscarPorId(id);
            }
            else if (opcion == "2")
            {
                string nombre = LeerTexto("Nombre (o parte del nombre)");
                encontrado = repositorio.BuscarPorNombre(nombre);
            }
            else
            {
                MostrarError("Opción no válida.");
                return;
            }

            if (encontrado == null)
            {
                MostrarAviso("No se encontró ningún producto.");
                return;
            }

            // Mostramos el detalle completo del producto encontrado
            Console.WriteLine();
            Console.WriteLine("  ✅ Producto encontrado:");
            Console.WriteLine(new string('─', 50));
            Console.WriteLine($"  ID:          {encontrado.Id:D3}");
            Console.WriteLine($"  Nombre:      {encontrado.Nombre}");
            Console.WriteLine($"  Categoría:   {encontrado.Categoria.Nombre}");
            Console.WriteLine($"  Precio:      {encontrado.Precio:C2}");
            Console.WriteLine($"  Stock:       {encontrado.Stock} unidades");
            Console.WriteLine($"  Valor total: {encontrado.ValorTotal():C2}");
            Console.WriteLine($"  Ingreso:     {encontrado.FechaIngreso:dd/MM/yyyy HH:mm}");
        }

        // ─────────────────────────────────────────────
        // OPCIÓN 4: ACTUALIZAR STOCK
        // ─────────────────────────────────────────────

        private void ActualizarStock()
        {
            Console.Clear();
            MostrarEncabezado("ACTUALIZAR STOCK");

            int id = LeerEntero("ID del producto");
            Producto? producto = repositorio.BuscarPorId(id);

            if (producto == null)
            {
                MostrarError("Producto no encontrado.");
                return;
            }

            Console.WriteLine($"\n  Producto: {producto.Nombre} | Stock actual: {producto.Stock}");
            Console.WriteLine("  1. Agregar stock (entrada de mercadería)");
            Console.WriteLine("  2. Reducir stock (salida / venta)");
            Console.Write("  Opción: ");
            string opcion = Console.ReadLine()?.Trim() ?? "";

            try
            {
                int cantidad = LeerEntero("Cantidad");

                if (opcion == "1")
                {
                    producto.AgregarStock(cantidad);
                    MostrarExito($"Stock actualizado. Nuevo stock: {producto.Stock}");
                }
                else if (opcion == "2")
                {
                    bool exito = producto.ReducirStock(cantidad);
                    if (exito)
                        MostrarExito($"Stock reducido. Nuevo stock: {producto.Stock}");
                    else
                        MostrarError($"Stock insuficiente. Stock actual: {producto.Stock}");
                }
                else
                {
                    MostrarError("Opción no válida.");
                }
            }
            catch (ArgumentException ex)
            {
                MostrarError(ex.Message);
            }
        }

        // ─────────────────────────────────────────────
        // OPCIÓN 5: ELIMINAR PRODUCTO
        // ─────────────────────────────────────────────

        private void EliminarProducto()
        {
            Console.Clear();
            MostrarEncabezado("ELIMINAR PRODUCTO");

            int id = LeerEntero("ID del producto a eliminar");
            Producto? producto = repositorio.BuscarPorId(id);

            if (producto == null)
            {
                MostrarError("Producto no encontrado.");
                return;
            }

            // Pedimos confirmación antes de eliminar — buena práctica UX
            Console.WriteLine($"\n  ⚠️  ¿Seguro que quieres eliminar '{producto.Nombre}'?");
            Console.Write("  Escribe SI para confirmar: ");
            string confirmacion = Console.ReadLine()?.Trim().ToUpper() ?? "";

            if (confirmacion != "SI")
            {
                MostrarAviso("Operación cancelada.");
                return;
            }

            bool eliminado = repositorio.EliminarProducto(id);
            if (eliminado)
                MostrarExito($"Producto '{producto.Nombre}' eliminado correctamente.");
            else
                MostrarError("No se pudo eliminar el producto.");
        }

        // ─────────────────────────────────────────────
        // OPCIÓN 6: VER POR CATEGORÍA
        // ─────────────────────────────────────────────

        private void MostrarProductosPorCategoria()
        {
            Console.Clear();
            MostrarEncabezado("PRODUCTOS POR CATEGORÍA");

            var categorias = repositorio.ObtenerCategorias().ToList();
            foreach (var cat in categorias)
                Console.WriteLine($"  {cat}");

            Console.WriteLine();
            int categoriaId = LeerEntero("ID de categoría");

            Categoria? categoria = repositorio.BuscarCategoriaPorId(categoriaId);
            if (categoria == null)
            {
                MostrarError("Categoría no encontrada.");
                return;
            }

            var productos = repositorio.ObtenerPorCategoria(categoriaId).ToList();
            Console.WriteLine($"\n  Productos en '{categoria.Nombre}' ({productos.Count}):");
            Console.WriteLine(new string('─', 70));

            if (!productos.Any())
            {
                MostrarAviso("No hay productos en esta categoría.");
                return;
            }

            foreach (var p in productos)
                Console.WriteLine($"  {p}");
        }

        // ─────────────────────────────────────────────
        // OPCIÓN 7: STOCK BAJO
        // ─────────────────────────────────────────────

        private void MostrarStockBajo()
        {
            Console.Clear();
            MostrarEncabezado("⚠️  PRODUCTOS CON STOCK BAJO");

            int minimo = LeerEntero("Mínimo de stock (se mostrarán los que tengan igual o menos)");
            var productos = repositorio.ObtenerConStockBajo(minimo).ToList();

            if (!productos.Any())
            {
                MostrarExito($"Todos los productos tienen más de {minimo} unidades en stock.");
                return;
            }

            Console.WriteLine($"\n  {productos.Count} producto(s) con stock ≤ {minimo}:");
            Console.WriteLine(new string('─', 70));

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (var p in productos)
                Console.WriteLine($"  {p}");
            Console.ResetColor();
        }

        // ─────────────────────────────────────────────
        // OPCIÓN 8: RESUMEN
        // ─────────────────────────────────────────────

        private void MostrarResumen()
        {
            Console.Clear();
            MostrarEncabezado("📊 RESUMEN DEL INVENTARIO");

            var productos = repositorio.ObtenerTodos().ToList();
            var categorias = repositorio.ObtenerCategorias().ToList();

            Console.WriteLine($"  Total de productos:     {repositorio.ContarProductos()}");
            Console.WriteLine($"  Total de categorías:    {categorias.Count}");
            Console.WriteLine($"  Valor total inventario: {repositorio.CalcularValorTotalInventario():C2}");

            // Mostramos cuántos productos tiene cada categoría
            Console.WriteLine("\n  Productos por categoría:");
            Console.WriteLine(new string('─', 40));

            foreach (var cat in categorias)
            {
                int cantidad = repositorio.ObtenerPorCategoria(cat.Id).Count();
                Console.WriteLine($"  {cat.Nombre,-20} {cantidad,3} producto(s)");
            }

            // Top 3 productos más caros
            var top3 = productos.OrderByDescending(p => p.Precio).Take(3).ToList();
            if (top3.Any())
            {
                Console.WriteLine("\n  Top 3 productos más caros:");
                Console.WriteLine(new string('─', 40));
                foreach (var p in top3)
                    Console.WriteLine($"  {p.Nombre,-25} {p.Precio:C2}");
            }
        }

        // ─────────────────────────────────────────────
        // DATOS INICIALES DE EJEMPLO
        // ─────────────────────────────────────────────

        private void CargarDatosIniciales()
        {
            // Creamos categorías
            var electronica = new Categoria(1, "Electrónica");
            var ropa = new Categoria(2, "Ropa");
            var alimentos = new Categoria(3, "Alimentos");
            var hogar = new Categoria(4, "Hogar");

            repositorio.AgregarCategoria(electronica);
            repositorio.AgregarCategoria(ropa);
            repositorio.AgregarCategoria(alimentos);
            repositorio.AgregarCategoria(hogar);

            // Creamos productos de ejemplo
            repositorio.AgregarProducto(new Producto("Laptop HP 15\"",    1299.99, 8,  electronica));
            repositorio.AgregarProducto(new Producto("Mouse inalámbrico",   25.50, 45, electronica));
            repositorio.AgregarProducto(new Producto("Teclado mecánico",    89.99, 3,  electronica));
            repositorio.AgregarProducto(new Producto("Monitor 24\"",       349.99, 5,  electronica));
            repositorio.AgregarProducto(new Producto("Camiseta básica",     19.99, 60, ropa));
            repositorio.AgregarProducto(new Producto("Jeans slim fit",      49.99, 30, ropa));
            repositorio.AgregarProducto(new Producto("Arroz 5kg",            8.50, 2,  alimentos));
            repositorio.AgregarProducto(new Producto("Aceite de oliva 1L",  12.99, 20, alimentos));
            repositorio.AgregarProducto(new Producto("Silla de oficina",   199.99, 4,  hogar));
            repositorio.AgregarProducto(new Producto("Lámpara LED",         34.99, 15, hogar));
        }

        // ─────────────────────────────────────────────
        // MÉTODOS AUXILIARES DE UI
        // ─────────────────────────────────────────────

        // Muestra un encabezado visual consistente en todas las pantallas
        private void MostrarEncabezado(string titulo)
        {
            Console.WriteLine(new string('═', 50));
            Console.WriteLine($"  {titulo}");
            Console.WriteLine(new string('═', 50));
            Console.WriteLine();
        }

        // Mensajes de éxito en verde
        private void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  ✅ {mensaje}");
            Console.ResetColor();
        }

        // Mensajes de error en rojo
        private void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  ❌ {mensaje}");
            Console.ResetColor();
        }

        // Avisos neutrales en amarillo
        private void MostrarAviso(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n  ⚠️  {mensaje}");
            Console.ResetColor();
        }

        // Lee un texto del usuario, reintentando si está vacío
        private string LeerTexto(string etiqueta)
        {
            string valor;
            do
            {
                Console.Write($"  {etiqueta}: ");
                valor = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(valor))
                    MostrarError("Este campo no puede estar vacío.");
            }
            while (string.IsNullOrWhiteSpace(valor));

            return valor;
        }

        // Lee un número entero, reintentando si la entrada no es válida
        private int LeerEntero(string etiqueta)
        {
            int valor;
            while (true)
            {
                Console.Write($"  {etiqueta}: ");
                string entrada = Console.ReadLine()?.Trim() ?? "";

                // TryParse intenta convertir el texto a int
                // Si lo logra, devuelve true y guarda el resultado en "valor"
                // Si falla (el usuario escribió letras), devuelve false
                if (int.TryParse(entrada, out valor))
                    return valor;

                MostrarError("Debes ingresar un número entero válido.");
            }
        }

        // Lee un número decimal, reintentando si la entrada no es válida
        private double LeerDouble(string etiqueta)
        {
            double valor;
            while (true)
            {
                Console.Write($"  {etiqueta}: ");
                string entrada = Console.ReadLine()?.Trim() ?? "";

                if (double.TryParse(entrada, out valor))
                    return valor;

                MostrarError("Debes ingresar un número válido (usa punto o coma para decimales).");
            }
        }
    }
}
