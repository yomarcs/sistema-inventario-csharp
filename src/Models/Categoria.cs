namespace SistemaInventario.Models
{
    // Clase que representa una categoría de producto
    // Ejemplo: "Electrónica", "Ropa", "Alimentos"
    public class Categoria
    {
        // Propiedad de solo lectura después de creada
        // Una categoría no cambia su ID una vez asignado
        public int Id { get; }

        // El nombre sí puede actualizarse
        private string nombre = "";
        public string Nombre
        {
            get { return nombre; }
            set
            {
                // Validamos que el nombre no sea vacío ni nulo
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("El nombre de la categoría no puede estar vacío.");

                nombre = value.Trim(); // Trim elimina espacios al inicio y al final
            }
        }

        public Categoria(int id, string nombre)
        {
            Id = id;
            Nombre = nombre; // Pasa por el setter, con su validación
        }

        // Sobreescribimos ToString para mostrar la categoría de forma legible
        // cuando la usamos dentro de un Console.WriteLine o interpolación
        public override string ToString() => $"[{Id}] {Nombre}";
    }
}
