using System;
using System.Collections.Generic;

class Program
{
    // Lista para almacenar usuarios
    static List<User> users = new List<User>();
    // Lista para almacenar repartidores
    static List<Repartidor> repartidores = new List<Repartidor>();

    static void Main(string[] args)
    {
        // Agregar usuarios predefinidos a la lista
        users.Add(new User("User1", "abcd@gmail.com", "abcd1234"));
        users.Add(new User("User2", "1234@gmail.com", "1234abcd"));

        // Agregar repartidores predefinidos a la lista
        repartidores.Add(new Repartidor("Gabrielex", "Gabrielex@gmail.com", "0999999999", 8000, 70));
        repartidores.Add(new Repartidor("Repartidor2", "repartidor2@gmail.com", "0888888888", 2000, 60));
        repartidores.Add(new Repartidor("Repartidor3", "repartidor3@gmail.com", "0777777777", 15000, 80));

        MostrarInterfaz();
    }

    static void MostrarInterfaz() // Interfaz del proxy de seguridad con funcionalidades añadidas Registro/Login
    {
        Console.WriteLine("Envíos de paquetes a nivel nacional");
        Console.WriteLine("1. Registrarse");
        Console.WriteLine("2. Iniciar sesión");

        int opcion = LeerEntero("Ingrese su opción: ");

        switch (opcion)
        {
            case 1:
                RegistrarUsuario();
                break;
            case 2:
                IniciarSesion();
                break;
            default:
                Console.WriteLine("Opción no válida");
                MostrarInterfaz();
                break;
        }
    }

    static void RegistrarUsuario()
    {
        Console.WriteLine("==== Registro de Usuario ====");
        Console.Write("Ingrese su nombre de usuario: ");
        string username = Console.ReadLine(); // Leer el nombre de usuario ingresado por el usuario
        Console.Write("Ingrese su correo electrónico: ");
        string correo = Console.ReadLine(); // Leer el correo electrónico ingresado por el usuario
        Console.Write("Ingrese su contraseña: ");
        string contraseña = Console.ReadLine(); // Leer la contraseña ingresada por el usuario

        // Crear un nuevo objeto User con los datos ingresados y agregarlo a la lista de usuarios
        users.Add(new User(username, correo, contraseña));

        Console.WriteLine("Registro exitoso. Ahora puede iniciar sesión.");
        Console.WriteLine();
        MostrarInterfaz(); // Volver a mostrar la interfaz principal
    }

    static void IniciarSesion()
    {
        Console.WriteLine("==== Iniciar Sesión ====");
        Console.Write("Ingrese su correo electrónico: ");
        string correo = Console.ReadLine(); // Leer el correo electrónico ingresado por el usuario
        Console.Write("Ingrese su contraseña: ");
        string contraseña = Console.ReadLine(); // Leer la contraseña ingresada por el usuario

        User usuario = null;
        foreach (User user in users)
        {
            // Verificar si el correo y la contraseña ingresados coinciden con los valores de algún usuario en la lista
            if (user.Correo == correo && user.Contraseña == contraseña)
            {
                usuario = user;
                break;
            }
        }

        if (usuario != null)
        {
            Console.WriteLine("Inicio de sesión exitoso. ¡Bienvenido, " + usuario.Username + "!");
            Console.WriteLine();

            Console.WriteLine("¿Va a hacer un envío? (Si/No)");
            string respuestaEnvio = Console.ReadLine();

            if (respuestaEnvio.ToLower() == "si")
            {
                string duroFragil, objeto, lugarRemitente, lugarReceptor;
                int distancia, tamañoPaquete;

                Console.WriteLine("==== Datos del Envío ====");
                Console.Write("Duro/Frágil: ");
                duroFragil = Console.ReadLine(); // Leer la opción de Duro/Frágil
                Console.Write("Objeto: ");
                objeto = Console.ReadLine(); // Leer el objeto del envío
                Console.Write("Lugar Remitente: ");
                lugarRemitente = Console.ReadLine(); // Leer el lugar del remitente
                Console.Write("Lugar Receptor: ");
                lugarReceptor = Console.ReadLine(); // Leer el lugar del receptor
                distancia = LeerEntero("Distancia: "); // Leer la distancia
                tamañoPaquete = LeerEntero("Tamaño del Paquete: "); // Leer el tamaño del paquete

                Console.WriteLine();
                Console.WriteLine("==== Lista de Repartidores Disponibles ====");
                MostrarRepartidoresDisponibles(tamañoPaquete); // Mostrar la lista de repartidores disponibles

                Console.WriteLine();

                bool repartidorElegido = false;
                while (!repartidorElegido)
                {
                    Console.Write("Elija un repartidor: ");
                    int indiceRepartidor = LeerEntero(""); // Leer el índice del repartidor seleccionado

                    if (indiceRepartidor >= 0 && indiceRepartidor < repartidores.Count)
                    {
                        Repartidor repartidorSeleccionado = repartidores[indiceRepartidor];
                        if (tamañoPaquete < repartidorSeleccionado.TamañoDisponible)
                        {
                            Console.WriteLine("Repartidor elegido: " + repartidorSeleccionado.Username);
                            CalcularTiempo(distancia, repartidorSeleccionado); // Calcular el tiempo estimado de entrega
                            repartidorElegido = true;
                        }
                        else
                        {
                            Console.WriteLine("El tamaño del paquete excede la capacidad del repartidor. Elija otro repartidor.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Índice de repartidor no válido. Intente nuevamente.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Proceso de envío terminado.");
            }
        }
        else
        {
            Console.WriteLine("Usuario no válido. Por favor, intente nuevamente.");
            Console.WriteLine();
            MostrarInterfaz();
        }
    }

    static void MostrarRepartidoresDisponibles(int tamañoPaquete)
    {
        for (int i = 0; i < repartidores.Count; i++)
        {
            Repartidor repartidor = repartidores[i];
            Console.WriteLine("Repartidor" + (i + 1));
            Console.WriteLine("Username: " + repartidor.Username);
            Console.WriteLine("Correo: " + repartidor.Correo);
            Console.WriteLine("Teléfono: " + repartidor.Telefono);
            Console.WriteLine("Tamaño Disponible: " + repartidor.TamañoDisponible);
            Console.WriteLine("Velocidad: " + repartidor.Velocidad);
            Console.WriteLine();
        }
    }

    static void CalcularTiempo(int distancia, Repartidor repartidor)
    {
        int tiempo = distancia / repartidor.Velocidad; // Calcular el tiempo estimado de entrega
        Console.WriteLine("Su envío llegará en " + tiempo + " horas. Su repartidor fue " + repartidor.Username + ".");
    }

    static int LeerEntero(string mensaje)
    {
        int valor;
        bool esEntero;
        do
        {
            Console.Write(mensaje);
            esEntero = int.TryParse(Console.ReadLine(), out valor); // Leer un valor entero de la entrada del usuario
        } while (!esEntero);
        return valor;
    }
}

class User
{
    public string Username { get; set; }
    public string Correo { get; set; }
    public string Contraseña { get; set; }

    public User(string username, string correo, string contraseña)
    {
        Username = username;
        Correo = correo;
        Contraseña = contraseña;
    }
}

class Repartidor
{
    public string Username { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }
    public int TamañoDisponible { get; set; }
    public int Velocidad { get; set; }

    public Repartidor(string username, string correo, string telefono, int tamañoDisponible, int velocidad)
    {
        Username = username;
        Correo = correo;
        Telefono = telefono;
        TamañoDisponible = tamañoDisponible;
        Velocidad = velocidad;
    }
}
