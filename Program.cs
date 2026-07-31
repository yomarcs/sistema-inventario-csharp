using SistemaInventario.Services;
using SistemaInventario.UI;

// ─────────────────────────────────────────────────────────────
// PUNTO DE ENTRADA DEL PROGRAMA
// ─────────────────────────────────────────────────────────────
// Aquí creamos las dependencias y arrancamos el sistema.
// Esto es la raíz de la Dependency Injection manual:
// 1. Creamos el repositorio (la capa de datos)
// 2. Se lo inyectamos al Menu (la capa de UI)
// 3. El Menu nunca sabe cómo se guardan los datos — solo los usa
// ─────────────────────────────────────────────────────────────

// Configuración de la consola para que muestre correctamente
// los caracteres especiales (tildes, ñ, emojis)
Console.OutputEncoding = System.Text.Encoding.UTF8;
Console.Title = "Sistema de Gestión de Inventario";

// Creamos el repositorio — aquí es donde viven los datos
var repositorio = new InventarioRepositorio();

// Creamos el menú y le pasamos el repositorio
// Si mañana queremos usar base de datos, solo cambiamos esta línea
var menu = new Menu(repositorio);

// Arrancamos el sistema
menu.Iniciar(); 
