# 📦 Sistema de Inventario Básico

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C%23](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=csharp)
![Status](https://img.shields.io/badge/Status-Completado-brightgreen?style=for-the-badge)

Aplicación de consola desarrollada en C# para gestionar un inventario básico de productos.

Permite registrar productos, mostrar información almacenada, buscar productos y calcular el valor total del inventario.

- - -

# 📋 Descripción

Este proyecto fue construido como parte del aprendizaje del **Bloque 1 - Fundamentos de C#**.

El objetivo fue aplicar todos los conceptos fundamentales aprendidos durante los ejercicios anteriores y unirlos en un pequeño sistema funcional.

El programa simula un inventario sencillo utilizando estructuras básicas del lenguaje.

En este proyecto se aplican:

* Variables 
* Tipos de datos
* Arrays
* Métodos
* Parámetros
* Condicionales
* Switch
* Ciclos

- - -

# ✨ Funcionalidades

* ✅ Registrar productos
* ✅ Guardar nombre, precio y cantidad
* ✅ Mostrar todos los productos registrados
* ✅ Buscar productos por nombre
* ✅ Calcular valor total del inventario
* ✅ Menú interactivo por consola
* ✅ Controlar límite máximo de productos

- - -

# 🛠️ Tecnologías utilizadas

* **Lenguaje:** C# 12
* **Plataforma:** .NET 8
* **Tipo de proyecto:** Aplicación de consola

- - -

# 📁 Estructura del proyecto

```
MiniProyectoInventarioBasico/

│
├── Program.cs
│
├── README.md
│
├── notas.md
│
├── recursos/
│   └── conceptos.md
│
└── reto.md
```

- - -

# 🚀 Cómo ejecutar el proyecto

## Requisitos previos

Tener instalado:

* .NET SDK 8

Descarga:

https://dotnet.microsoft.com/download/dotnet/8.0

- - -

## Pasos

Clonar repositorio:

``` bash
git clone https://github.com/tuusuario/curso-csharp.git
```

Ingresar a la carpeta:

``` bash
cd mini-proyecto-inventario-basico
```

Ejecutar:

``` bash
dotnet run
```

- - -

# 🖥️ Ejecución del sistema

```
==============================

 SISTEMA DE INVENTARIO BÁSICO

==============================


1. Registrar producto

2. Mostrar productos

3. Buscar producto

4. Valor total inventario

5. Salir


Seleccione una opción:
```

- - -

# Ejemplo de registro

```
Seleccione una opción: 1


Nombre producto:
Laptop


Precio:
2500


Cantidad:
5


Producto registrado correctamente.
```

- - -

# Ejemplo mostrando inventario

```
===== PRODUCTOS =====


1. Laptop | Precio: 2500 | Stock: 5

2. Mouse | Precio: 50 | Stock: 20
```

- - -

# 🏗️ Decisiones de diseño

## ¿Por qué usar arrays?

En este proyecto utilizamos arrays porque es una de las primeras estructuras aprendidas en C#.

Ejemplo:

``` csharp
string[] productos;
```

Cada posición del array representa un producto.

```
Posición

0 → Laptop

1 → Mouse

2 → Teclado
```

- - -

## ¿Por qué separar el código en métodos?

El programa podría estar completamente dentro de `Main()`.

Sin embargo, separar responsabilidades permite crear código más organizado.

Ejemplo:

```
MostrarMenu()

RegistrarProducto()

MostrarProductos()

BuscarProducto()
```

Cada método tiene una función específica.

- - -

## ¿Por qué todavía no usamos clases?

Porque este proyecto pertenece al Bloque 1.

Todavía no conocemos:

* Clases
* Objetos
* Constructores
* Encapsulación

En el Bloque 2 este mismo proyecto evolucionará utilizando Programación Orientada a Objetos.

- - -

# 📚 Conceptos aplicados

| Concepto | Aplicación |
| -------- | ---------- |
| Variables | Datos del producto |
| Tipos de datos | string, int, double |
| Arrays | Almacenamiento de productos |
| if / else | Validaciones |
| switch | Menú principal |
| do while | Mantener funcionando el sistema |
| for | Recorrer productos |
| Métodos | Separar funcionalidades |
| Parámetros | Enviar información a métodos |

- - -

# 📈 Evolución del proyecto

Este proyecto continuará creciendo durante el roadmap:

```
BLOQUE 1

Inventario Básico

Arrays + Métodos


        ↓


BLOQUE 2

Inventario POO

Clases + Objetos + Encapsulación


        ↓


BLOQUE 3

Inventario Profesional

List<T> + LINQ + Interfaces


        ↓


ASP.NET CORE + ANGULAR

Aplicación Web completa
```

- - -

# 🗺️ Mejoras futuras

* [ ] Crear clase Producto
* [ ] Crear clase Categoría
* [ ] Agregar propiedades
* [ ] Implementar encapsulación
* [ ] Guardar datos en archivos
* [ ] Utilizar colecciones genéricas
* [ ] Crear API REST

- - -

# 👤 Autor

**Yomar**

GitHub:

[@yomarcs](https://github.com/yomarcs)

Proyecto parte del roadmap:

**Full Stack .NET + Angular**

- - -

# 📄 Licencia

Este proyecto está bajo licencia MIT.