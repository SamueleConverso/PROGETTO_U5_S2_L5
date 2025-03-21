using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S2_L5.ViewModels {
    public class AddPrenotazioneViewModel {
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
    }
}
