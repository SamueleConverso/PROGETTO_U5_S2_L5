using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROGETTO_U5_S2_L5.Models {
    [Table("Camere")]
    public class Camera {
        [Key]
        public Guid CameraId {
            get; set;
        }

        [Required]
        public int Numero {
            get; set;
        }

        [Required]
        public string Tipo {
            get; set;
        }

        [Required]
        public decimal Prezzo {
            get; set;
        }

        public ICollection<Prenotazione> Prenotazioni {
            get; set;
        }
    }
}
