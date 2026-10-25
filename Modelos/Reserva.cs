
namespace PLANAZO_POO_CSharp.Modelos
{
    public class Reserva
    {
        public int Id { get; }
        public Usuario Usuario { get; }
        public Evento Evento { get; }
        public int CantidadEntradas { get; }
        public decimal ValorTotal { get; }
        public bool EstaActiva { get; private set; }

        public string Estado
        {
            get { return EstaActiva ? "Activa" : "Cancelada"; }
        }

        public Reserva(int id, Usuario usuario, Evento evento, int cantidadEntradas)
        {
            if (id <= 0)
                throw new ArgumentException("El identificador debe ser positivo.");

            if (usuario == null)
                throw new ArgumentNullException(nameof(usuario));

            if (evento == null)
                throw new ArgumentNullException(nameof(evento));

            if (cantidadEntradas <= 0)
                throw new ArgumentException("La cantidad debe ser mayor que cero.");

            Id = id;
            Usuario = usuario;
            Evento = evento;
            CantidadEntradas = cantidadEntradas;
            ValorTotal = evento.CalcularPrecioFinal() * cantidadEntradas;
            EstaActiva = true;
        }

        public void Cancelar()
        {
            if (!EstaActiva)
                throw new InvalidOperationException("Esta reserva ya fue cancelada.");

            Evento.DevolverCupos(CantidadEntradas);
            EstaActiva = false;
        }
    }
}
