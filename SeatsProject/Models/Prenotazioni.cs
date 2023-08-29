namespace SeatsProject.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class Prenotazioni
    {
        [Key]
        public int IdPrenotazine { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd\\-MM\\-yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Immettere la data aggiornata")]
        [DisplayName("Data della prenotazione")]
        public DateTime Data { get; set; }

        public string Utente { get; set; }

        public string CodicePostazione { get; set; }
    }
}
