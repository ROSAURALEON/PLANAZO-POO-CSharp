
using System;
using System.Collections.Generic;
using System.Linq;
using PLANAZO_POO_CSharp.Modelos;

namespace PLANAZO_POO_CSharp.Servicios
{
    public class GestorReservas
    {
        private readonly List<Evento> eventos = new List<Evento>();
        private readonly List<Usuario> usuarios = new List<Usuario>();
        private readonly List<Reserva> reservas = new List<Reserva>();

        private int siguienteIdReserva = 1;
        private int siguienteIdUsuario = 1;

        public GestorReservas()
        {
            RegistrarEventosIniciales();
        }

        private void RegistrarEventosIniciales()
        {
            eventos.Add(new Cine(1, "Avatar 3", new DateTime(2026, 11, 15), 15000m, 100));
            eventos.Add(new Cine(2, "Lilo y Stitch", new DateTime(2026, 11, 20), 12000m, 80));

            eventos.Add(new Concierto(3, "Noche de Vallenato", new DateTime(2026, 12, 5), 50000m, 200));
            eventos.Add(new Concierto(4, "Festival de Rock", new DateTime(2026, 12, 12), 70000m, 150));

            eventos.Add(new Taller(5, "Taller de Programación", new DateTime(2026, 11, 25), 40000m, 30));
            eventos.Add(new Taller(6, "Taller de Fotografía", new DateTime(2026, 12, 1), 35000m, 25));

            eventos.Add(new EventoDeportivo(7, "Campeonato de Fútbol", new DateTime(2026, 12, 15), 50000m, 300));
        }

        public List<Evento> ObtenerEventos()
        {
            return eventos.ToList();
        }

        public Evento BuscarEvento(int id)
        {
            return eventos.FirstOrDefault(e => e.Id == id);
        }

        public Usuario RegistrarUsuario(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("Debes escribir el nombre de la persona.");

            Usuario usuario = new Usuario(siguienteIdUsuario++, nombre.Trim());
            usuarios.Add(usuario);
            return usuario;
        }

        public Reserva CrearReserva(int idEvento, Usuario usuario, int cantidadEntradas)
        {
            Evento evento = BuscarEvento(idEvento);

            if (evento == null)
                throw new KeyNotFoundException("El evento solicitado no existe.");

            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (cantidadEntradas <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");

            if (cantidadEntradas > evento.CuposDisponibles)
                throw new InvalidOperationException("No hay suficientes entradas disponibles.");

            evento.ReservarCupos(cantidadEntradas);

            Reserva reserva = new Reserva(siguienteIdReserva, usuario, evento, cantidadEntradas);
            reservas.Add(reserva);
            siguienteIdReserva++;

            return reserva;
        }

        public List<Reserva> ObtenerReservas()
        {
            return reservas.ToList();
        }

        public List<Reserva> ObtenerReservasActivas()
        {
            return reservas.Where(r => r.EstaActiva).ToList();
        }

        public List<Reserva> ObtenerReservasDeUsuario(int idUsuario)
        {
            return reservas.Where(r => r.Usuario.Id == idUsuario).ToList();
        }

        public void CancelarReserva(int idReserva)
        {
            Reserva reserva = reservas.FirstOrDefault(r => r.Id == idReserva);

            if (reserva == null)
                throw new KeyNotFoundException("La reserva no existe.");

            reserva.Cancelar();
        }

        public int ContarReservasActivas()
        {
            return reservas.Count(r => r.EstaActiva);
        }

        public int ContarEntradasReservadas()
        {
            return reservas.Where(r => r.EstaActiva).Sum(r => r.CantidadEntradas);
        }

        public decimal CalcularValorTotalReservas()
        {
            return reservas.Where(r => r.EstaActiva).Sum(r => r.ValorTotal);
        }

        public Usuario BuscarUsuario(int id)
        {
            return usuarios.FirstOrDefault(u => u.Id == id);
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return usuarios.ToList();
        }
    }
}
