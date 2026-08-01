# Notas del Proyecto

## Objetivo principal

Este proyecto representa el paso de ejercicios pequeños hacia una aplicación completa.


Hasta ahora aprendimos conceptos individuales:

```
Variables

Condicionales

Ciclos

Métodos

Arrays
```


Ahora todos trabajan juntos dentro de un sistema.


---

# Organización del código


Aunque solamente tenemos un archivo:

```
Program.cs
```


internamente ya estamos separando responsabilidades.


Ejemplo:


```
Main()

 |
 |
 +-- MostrarMenu()

 |
 |
 +-- RegistrarProducto()

 |
 |
 +-- MostrarProductos()

 |
 |
 +-- BuscarProducto()
```


---

# Pensamiento profesional


Un programador no solamente escribe código que funciona.


También piensa:

- ¿Cómo organizarlo?
- ¿Cómo hacerlo fácil de modificar?
- ¿Cómo evitar repetir código?


---

# Próxima evolución


Actualmente:


```
Producto = datos separados

Nombre[]
Precio[]
Cantidad[]
```


Problema:


Si tenemos 100 datos diferentes sería difícil manejarlo.


Más adelante:


```
Producto

 ├── Nombre

 ├── Precio

 └── Stock
```


Todo estará agrupado en un objeto.


Eso será Programación Orientada a Objetos.

# Conceptos utilizados


# Variables


Son espacios donde almacenamos información.


Ejemplo:


```csharp
int cantidad = 10;
```


---

# Arrays


Permiten guardar varios valores del mismo tipo.


Ejemplo:


```csharp
string[] productos;
```


Cada posición tiene un dato.


---

# Métodos


Son bloques de código reutilizables.


Ejemplo:


```csharp
static void MostrarMenu()
{

}
```


Permiten organizar el programa.


---

# Parámetros


Permiten enviar información a un método.


Ejemplo:


```csharp
MostrarProducto(nombre);
```


---

# Condicionales


Permiten tomar decisiones.


Ejemplo:


```csharp
if(stock > 0)
{

}
```


---

# Switch


Permite manejar diferentes opciones.


Ejemplo:


```csharp
switch(opcion)
{

}
```


---

# Ciclos


Permiten repetir instrucciones.


Ejemplo:


```csharp
for()
{

}
```


---

# Fin del proyecto


Este proyecto representa la base antes de comenzar:

- Programación Orientada a Objetos
- Colecciones
- Arquitectura de aplicaciones
