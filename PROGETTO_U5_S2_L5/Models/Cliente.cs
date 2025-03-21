using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROGETTO_U5_S2_L5.Models {
    [Table("Clienti")]
    public class Cliente {
        [Key]
        public Guid ClienteId {
            get; set;
        }

        [Required]
        public string Nome {
            get; set;
        }

        [Required]
        public string Cognome {
            get; set;
        }

        [Required]
        public string Email {
            get; set;
        }

        [Required]
        public string Telefono {
            get; set;
        }

        public ICollection<Prenotazione> Prenotazioni {
            get; set;
        }
    }
}
