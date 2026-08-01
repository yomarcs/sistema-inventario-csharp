// ========================================================
// MINI PROYECTO - SISTEMA DE INVENTARIO BÁSICO
//
// Objetivo:
// Crear un sistema sencillo para registrar productos,
// consultar inventario y calcular valores.
//
// Conceptos utilizados:
//
// ✔ Variables
// ✔ Arrays
// ✔ Métodos
// ✔ Parámetros
// ✔ Condicionales
// ✔ Switch
// ✔ Ciclos
// ========================================================


using System;

class Program
{

    // Arrays donde almacenaremos los productos

    static string[] productos = new string[10];

    static double[] precios = new double[10];

    static int[] cantidades = new int[10];


    // Cantidad actual de productos registrados

    static int cantidadProductos = 0;



    static void Main()
    {

        int opcion;


        do
        {

            MostrarMenu();


            Console.Write("Seleccione una opción: ");

            opcion = Convert.ToInt32(Console.ReadLine());



            switch(opcion)
            {

                case 1:

                    RegistrarProducto();

                    break;



                case 2:

                    MostrarProductos();

                    break;



                case 3:

                    BuscarProducto();

                    break;



                case 4:

                    CalcularValorInventario();

                    break;



                case 5:

                    Console.WriteLine(
                        "Saliendo del sistema..."
                    );

                    break;



                default:

                    Console.WriteLine(
                        "Opción incorrecta."
                    );

                    break;

            }



            Console.WriteLine();


        } while(opcion != 5);


    }





    // =====================================================
    // MENÚ PRINCIPAL
    // =====================================================

    static void MostrarMenu()
    {

        Console.WriteLine("==============================");

        Console.WriteLine(
            " SISTEMA DE INVENTARIO BÁSICO"
        );

        Console.WriteLine("==============================");


        Console.WriteLine(
            "1. Registrar producto"
        );


        Console.WriteLine(
            "2. Mostrar productos"
        );


        Console.WriteLine(
            "3. Buscar producto"
        );


        Console.WriteLine(
            "4. Valor total inventario"
        );


        Console.WriteLine(
            "5. Salir"
        );

    }






    // =====================================================
    // REGISTRAR PRODUCTO
    // =====================================================

    static void RegistrarProducto()
    {

        if(cantidadProductos >= 10)
        {

            Console.WriteLine(
                "Inventario lleno."
            );

            return;

        }



        Console.Write("Nombre producto: ");

        string nombre = Console.ReadLine();



        Console.Write("Precio: ");

        double precio =
            Convert.ToDouble(Console.ReadLine());



        Console.Write("Cantidad: ");

        int cantidad =
            Convert.ToInt32(Console.ReadLine());




        productos[cantidadProductos] = nombre;

        precios[cantidadProductos] = precio;

        cantidades[cantidadProductos] = cantidad;



        cantidadProductos++;



        Console.WriteLine(
            "Producto registrado correctamente."
        );

    }







    // =====================================================
    // MOSTRAR PRODUCTOS
    // =====================================================

    static void MostrarProductos()
    {

        if(cantidadProductos == 0)
        {

            Console.WriteLine(
                "No existen productos registrados."
            );

            return;

        }



        Console.WriteLine(
            "===== PRODUCTOS ====="
        );



        for(int i = 0; i < cantidadProductos; i++)
        {

            Console.WriteLine(
                $"{i + 1}. {productos[i]} | Precio: {precios[i]} | Stock: {cantidades[i]}"
            );

        }

    }







    // =====================================================
    // BUSCAR PRODUCTO
    // =====================================================

    static void BuscarProducto()
    {

        Console.Write(
            "Ingrese nombre del producto:"
        );


        string buscar =
            Console.ReadLine();



        bool encontrado = false;



        for(int i = 0; i < cantidadProductos; i++)
        {

            if(productos[i].ToLower()
                == buscar.ToLower())
            {

                Console.WriteLine(
                    $"Encontrado: {productos[i]}"
                );


                Console.WriteLine(
                    $"Precio: {precios[i]}"
                );


                Console.WriteLine(
                    $"Stock: {cantidades[i]}"
                );


                encontrado = true;

                break;

            }

        }



        if(!encontrado)
        {

            Console.WriteLine(
                "Producto no encontrado."
            );

        }

    }








    // =====================================================
    // CALCULAR VALOR DEL INVENTARIO
    // =====================================================

    static void CalcularValorInventario()
    {

        double total = 0;



        for(int i = 0; i < cantidadProductos; i++)
        {

            total += 
                precios[i] * cantidades[i];

        }



        Console.WriteLine(
            $"Valor total del inventario: {total}"
        );

    }


}