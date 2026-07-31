# 📦 Sistema de Gestión de Inventario

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)
![Status](https://img.shields.io/badge/Status-Completado-brightgreen?style=for-the-badge)

Aplicación de consola para gestionar el inventario de un negocio. Permite registrar productos por categoría, controlar el stock, buscar por distintos criterios y obtener reportes del estado del inventario.

---

## 📋 Descripción

Este proyecto fue construido como parte de mi camino de aprendizaje en **C# y .NET**, al terminar los fundamentos del lenguaje. El objetivo fue aplicar de forma integrada los conceptos aprendidos: clases, OOP, colecciones, interfaces y buenas prácticas de organización de código.

El sistema simula el backend de un sistema de inventario real, con operaciones CRUD completas, validaciones en el modelo y una interfaz de usuario de consola limpia y navegable.

---

## ✨ Funcionalidades

- ✅ Listar todos los productos con formato de tabla alineado
- ✅ Agregar productos con validación de datos en tiempo real
- ✅ Buscar producto por ID o por nombre (búsqueda parcial)
- ✅ Actualizar stock (entrada de mercadería / salida por venta)
- ✅ Eliminar producto con confirmación previa
- ✅ Filtrar productos por categoría
- ✅ Alertas de stock bajo configurables
- ✅ Resumen del inventario con valor total y top 3 más caros
- ✅ IDs autogenerados y fecha de ingreso automática
- ✅ Mensajes de error, éxito y aviso con colores diferenciados

---

## 🛠️ Tecnologías utilizadas

- **Lenguaje:** C# 12
- **Plataforma:** .NET 8
- **Tipo de proyecto:** Aplicación de consola

---

## 📁 Estructura del proyecto

```
SistemaInventario/
│
├── src/
│   ├── Models/
│   │   ├── Producto.cs        ← Modelo con encapsulación y validaciones
│   │   └── Categoria.cs       ← Modelo de categoría
│   │
│   ├── Interfaces/
│   │   └── IInventarioRepositorio.cs  ← Contrato del repositorio
│   │
│   ├── Services/
│   │   └── InventarioRepositorio.cs   ← Implementación en memoria
│   │
│   └── UI/
│       └── Menu.cs            ← Interfaz de usuario de consola
│
├── Program.cs                 ← Punto de entrada y composición
├── SistemaInventario.csproj
└── README.md
```

---

## 🚀 Cómo ejecutar el proyecto

### Requisitos previos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Pasos

1. Clona el repositorio
```bash
git clone https://github.com/tuusuario/sistema-inventario-csharp.git
```

2. Entra a la carpeta
```bash
cd sistema-inventario-csharp
```

3. Ejecuta el proyecto
```bash
dotnet run
```

---

## 🖥️ Capturas del sistema

```
══════════════════════════════════════════════════
  SISTEMA DE GESTIÓN DE INVENTARIO
══════════════════════════════════════════════════

  1. 📋 Ver todos los productos
  2. ➕ Agregar producto
  3. 🔍 Buscar producto
  4. 📦 Actualizar stock
  5. 🗑️  Eliminar producto
  6. 🏷️  Ver por categoría
  7. ⚠️  Stock bajo
  8. 📊 Resumen del inventario
  9. 🚪 Salir
──────────────────────────────────────────────────
  Selecciona una opción:
```

---

## 🏗️ Decisiones de diseño

### ¿Por qué una interfaz `IInventarioRepositorio`?
El menú depende de la interfaz, no de la implementación concreta. Hoy los datos viven en memoria — mañana se puede crear `InventarioRepositorioSQL` que implemente la misma interfaz y el resto del programa no cambia ni una línea.

### ¿Por qué `Dictionary` en vez de `List` para almacenar?
La búsqueda por ID es la operación más frecuente. `Dictionary<int, Producto>` la resuelve en O(1) — tiempo constante sin importar cuántos productos haya. Una `List` requeriría recorrer elemento por elemento.

### ¿Por qué validar en el modelo y no en el menú?
Las reglas de negocio (precio > 0, stock ≥ 0) pertenecen al modelo, no a la UI. Si mañana hay una API o un frontend distinto, las validaciones siguen funcionando sin duplicar código.

---

## 📚 Conceptos aplicados

| Concepto | Dónde se aplica |
|---|---|
| Clases y objetos | `Producto`, `Categoria` |
| Encapsulación | Setters con validación en `Producto` |
| Interfaces | `IInventarioRepositorio` |
| Colecciones | `Dictionary`, `List`, `IEnumerable` |
| LINQ | `Where`, `OrderBy`, `FirstOrDefault`, `Sum`, `Take` |
| Manejo de errores | `try/catch` en flujo de UI |
| Método estático | `contadorId` para autogenerar IDs |
| Dependency Injection | El menú recibe el repositorio por constructor |

---

## 🗺️ Próximos pasos (mejoras futuras)

- [ ] Persistencia en archivo JSON
- [ ] Persistencia en base de datos con Entity Framework Core
- [ ] Exportar reporte a CSV
- [ ] Historial de movimientos de stock

---

## 👤 Autor

**Yomar**
- GitHub: [@yomarcs](https://github.com/yomarcs)
- Proyecto parte del roadmap: [Full Stack .NET + Angular](https://github.com/yomarcs)

---

## 📄 Licencia

Este proyecto está bajo la licencia MIT.
