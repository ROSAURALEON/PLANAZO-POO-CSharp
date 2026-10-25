
using PLANAZO_POO_CSharp.Modelos;
using PLANAZO_POO_CSharp.Servicios;
using System.Globalization;

namespace PLANAZO_POO_CSharp;

internal class Program
{
    private static readonly GestorReservas gestor = new();

    static void Main(string[] args)
    {
        int opcion;

        do
        {
            MostrarMenu();
            opcion = LeerEntero("Seleccione una opción: ");

            try
            {
                switch (opcion)
                {
                    case 1:
                        VerEventos();
                        break;

                    case 2:
                        ConsultarEvento();
                        break;

                    case 3:
                        ReservarEntradas();
                        break;

                    case 4:
                        VerReservas();
                        break;

                    case 5:
                        CancelarReserva();
                        break;

                    case 6:
                        VerResumen();
                        break;

                    case 7:
                        ConsultarReservasUsuario();
                        break;

                    case 0:
                        Console.WriteLine(
                            "Gracias por utilizar PLANAZO.");
                        break;

                    default:
                        Console.WriteLine(
                            "Opción no válida. Intente nuevamente.");
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error en los datos: {ex.Message}");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Operación no permitida: {ex.Message}");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"No encontrado: {ex.Message}");
            }
            catch (OverflowException)
            {
                Console.WriteLine("El número ingresado es demasiado grande.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error: {ex.Message}");
            }

            if (opcion != 0)
            {
                Console.WriteLine();
                Console.WriteLine("Presione ENTER para continuar...");
                Console.ReadLine();
            }

        } while (opcion != 0);
    }

    static void MostrarMenu()
    {
        Console.Clear();

        Console.WriteLine("================================");
        Console.WriteLine("       PLANAZO - EVENTOS");
        Console.WriteLine("================================");
        Console.WriteLine("1. Ver eventos disponibles");
        Console.WriteLine("2. Consultar evento");
        Console.WriteLine("3. Reservar entradas");
        Console.WriteLine("4. Ver reservas realizadas");
        Console.WriteLine("5. Cancelar una reserva");
        Console.WriteLine("6. Ver resumen de reservas");
        Console.WriteLine("7. Consultar reservas por usuario");
        Console.WriteLine("0. Salir");
        Console.WriteLine("================================");
    }

    static int LeerEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string? entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int numero))
                return numero;

            Console.WriteLine("Debe ingresar un número entero válido.");
        }
    }

    static void VerEventos()
    {
        Console.WriteLine("\n--- EVENTOS DISPONIBLES ---");

        foreach (Evento evento in gestor.ObtenerEventos())
        {
            Console.WriteLine(
                $"ID: {evento.Id} | {evento.Nombre} | " +
                $"Fecha: {evento.Fecha:dd/MM/yyyy} | " +
                $"Precio: {evento.CalcularPrecioFinal():C0} | " +
                $"Cupos: {evento.CuposDisponibles}");
        }
    }

    static void ConsultarEvento()
    {
        int id = LeerEntero("Ingrese el ID del evento: ");

        Evento? evento = gestor.BuscarEvento(id);

        if (evento == null)
        {
            Console.WriteLine("El evento no existe.");
            return;
        }

        Console.WriteLine("\n--- DETALLE DEL EVENTO ---");
        Console.WriteLine($"Nombre: {evento.Nombre}");
        Console.WriteLine($"Fecha: {evento.Fecha:dd/MM/yyyy}");
        Console.WriteLine($"Precio base: {evento.PrecioBase:C0}");
        Console.WriteLine($"Precio final: {evento.CalcularPrecioFinal():C0}");
        Console.WriteLine($"Capacidad máxima: {evento.CapacidadMaxima}");
        Console.WriteLine($"Cupos disponibles: {evento.CuposDisponibles}");
    }

    static void ReservarEntradas()
    {
        VerEventos();

        int idEvento = LeerEntero("\nIngrese el ID del evento: ");

        Evento? evento = gestor.BuscarEvento(idEvento);

        if (evento == null)
        {
            Console.WriteLine("El evento no existe.");
            return;
        }

        Console.Write("Nombre de la persona: ");
        string nombre = Console.ReadLine() ?? "";

        Usuario usuario = gestor.RegistrarUsuario(nombre);

        int cantidad = LeerEntero("Cantidad de entradas: ");

        Reserva reserva = gestor.CrearReserva(
            evento.Id, usuario, cantidad);

        Console.WriteLine("\n¡Reserva realizada correctamente!");
        Console.WriteLine($"Número de reserva: {reserva.Id}");
        Console.WriteLine($"Usuario: {reserva.Usuario.Nombre}");
        Console.WriteLine($"Evento: {reserva.Evento.Nombre}");
        Console.WriteLine($"Entradas: {reserva.CantidadEntradas}");
        Console.WriteLine($"Total: {reserva.ValorTotal:C0}");
        Console.WriteLine($"Estado: {reserva.Estado}");
    }

    static void VerReservas()
    {
        Console.WriteLine("\n--- RESERVAS REALIZADAS ---");

        List<Reserva> reservas = gestor.ObtenerReservas();

        if (reservas.Count == 0)
        {
            Console.WriteLine("Todavía no hay reservas.");
            return;
        }

        foreach (Reserva reserva in reservas)
        {
            Console.WriteLine(
                $"ID: {reserva.Id} | " +
                $"Persona: {reserva.Usuario.Nombre} | " +
                $"Evento: {reserva.Evento.Nombre} | " +
                $"Entradas: {reserva.CantidadEntradas} | " +
                $"Total: {reserva.ValorTotal:C0} | " +
                $"Estado: {reserva.Estado}");
        }
    }

    static void CancelarReserva()
    {
        VerReservas();

        int id = LeerEntero(
            "\nIngrese el ID de la reserva que desea cancelar: ");

        gestor.CancelarReserva(id);

        Console.WriteLine("Reserva cancelada correctamente.");
        Console.WriteLine("Los cupos fueron devueltos al evento.");
    }

    static void VerResumen()
    {
        Console.WriteLine("\n--- RESUMEN DE RESERVAS ACTIVAS ---");
        Console.WriteLine(
            $"Reservas activas: {gestor.ContarReservasActivas()}");

        Console.WriteLine(
            $"Entradas reservadas: {gestor.ContarEntradasReservadas()}");

        Console.WriteLine(
            $"Valor total: {gestor.CalcularValorTotalReservas():C0}");
    }

    static void ConsultarReservasUsuario()
    {
        int idUsuario = LeerEntero("Ingrese el ID del usuario: ");

        Usuario? usuario = gestor.BuscarUsuario(idUsuario);

        if (usuario == null)
        {
            Console.WriteLine("El usuario no existe.");
            return;
        }

        Console.WriteLine($"\nReservas de {usuario.Nombre}:");

        List<Reserva> reservas =
            gestor.ObtenerReservasDeUsuario(idUsuario);

        if (reservas.Count == 0)
        {
            Console.WriteLine("Este usuario no tiene reservas.");
            return;
        }

        foreach (Reserva reserva in reservas)
        {
            Console.WriteLine(
                $"Reserva {reserva.Id} | {reserva.Evento.Nombre} | " +
                $"Entradas: {reserva.CantidadEntradas} | " +
                $"Estado: {reserva.Estado} | " +
                $"Total: {reserva.ValorTotal:C0}");
        }
    }
}
