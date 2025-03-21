using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROGETTO_U5_S2_L5.Models {
    [Table("Prenotazioni")]
    public class Prenotazione {
        [Key]
        public Guid PrenotazioneId {
            get; set;
        }

        [Required]
        public Guid ClienteId {
            get; set;
        }

        [Required]
        public Guid CameraId {
            get; set;
        }

        [Required]
        public DateOnly DataInizio {
            get; set;
        }

        [Required]
        public DateOnly DataFine {
            get; set;
        }

        [Required]
        public bool Stato {
            get; set;
        }

        [ForeignKey("ClienteId")]
        public Cliente Cliente {
            get; set;
        }

        [ForeignKey("CameraId")]
        public Camera Camera {
            get; set;
        }
    }
}
