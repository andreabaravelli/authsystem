namespace SeatsProject.Models
{
    public class Sede
    {
        public int Id { get; set; }

        public string Descrizione { get; set; }

        public string CodiceArea { get; set; }

        public int? IdPadre { get; set; }

        public bool Prenotabile { get; set; }

        public string? ImmagineSvg { get; set; }

    }
}
