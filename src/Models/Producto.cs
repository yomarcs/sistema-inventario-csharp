namespace SistemaInventario.Models
{
    public class Producto
    {
        // Contador estático: pertenece a la CLASE, no a cada objeto
        // Se usa para generar IDs únicos automáticamente
        // Cada vez que se crea un producto, este número sube 1
        private static int contadorId = 1;

        // ID único del producto — solo lectura desde afuera
        public int Id { get; }

        // Nombre con validación en el setter
        private string nombre = "";
        public string Nombre
        {
            get { return nombre; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre del producto no puede estar vacío.");
                nombre = value.Trim();
            }
        }

        // Precio con validación — no puede ser negativo ni cero
        private double precio;
        public double Precio
        {
            get { return precio; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("El precio debe ser mayor a 0.");
                precio = value;
            }
        }

        // Stock con validación — no puede ser negativo
        private int stock;
        public int Stock
        {
            get { return stock; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("El stock no puede ser negativo.");
                stock = value;
            }
        }

        // Referencia a la categoría del producto
        // Es un objeto Categoria, no solo un número o texto
        public Categoria Categoria { get; set; }

        // Fecha en que fue agregado al inventario — se asigna automáticamente
        public DateTime FechaIngreso { get; }

        // Constructor: recibe todos los datos necesarios para crear un producto válido
        public Producto(string nombre, double precio, int stock, Categoria categoria)
        {
            Id = contadorId++;   // Asigna el ID actual y luego incrementa el contador
            Nombre = nombre;     // Pasa por el setter con validación
            Precio = precio;     // Pasa por el setter con validación
            Stock = stock;       // Pasa por el setter con validación
            Categoria = categoria;
            FechaIngreso = DateTime.Now;
        }

        // Método para agregar stock (cuando llega mercadería)
        public void AgregarStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad a agregar debe ser mayor a 0.");
            Stock += cantidad;
        }

        // Método para reducir stock (cuando se vende o consume)
        public bool ReducirStock(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad a reducir debe ser mayor a 0.");

            // Si no hay suficiente stock, devolvemos false en vez de lanzar excepción
            // Así el que llama a este método decide qué hacer
            if (cantidad > Stock)
                return false;

            Stock -= cantidad;
            return true;
        }

        // Calcula el valor total de este producto en el inventario
        // Precio unitario × cantidad en stock
        public double ValorTotal() => Precio * Stock;

        // ToString para mostrar el producto de forma legible en cualquier contexto
        public override string ToString() =>
            $"[{Id:D3}] {Nombre,-25} | {Categoria.Nombre,-15} | {Precio,10:C2} | Stock: {Stock,5}";
        //   D3 = 3 dígitos con ceros (001, 002...)
        //   -25 = alineado a la izquierda en 25 caracteres (para que quede en columna)
        //   ,10 = alineado a la derecha en 10 caracteres
    }
}
