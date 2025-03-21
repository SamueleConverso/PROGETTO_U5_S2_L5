using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S2_L5.ViewModels {
    public class EditPrenotazioneViewModel {
        public Guid PrenotazioneId {
            get; set;
        }

        [Display(Name = "Cliente")]
        [Required(ErrorMessage = "Cliente obbligatorio")]
        public required Guid ClienteId {
            get; set;
        }

        [Display(Name = "Camera")]
        [Required(ErrorMessage = "Camera obbligatoria")]
        public required Guid CameraId {
            get; set;
        }

        [Display(Name = "Data inizio")]
        [Required(ErrorMessage = "Data inizio obbligatoria")]
        public required DateOnly DataInizio {
            get; set;
        }

        [Display(Name = "Data fine")]
        [Required(ErrorMessage = "Data fine obbligatoria")]
        public required DateOnly DataFine {
            get; set;
        }

        [Display(Name = "Pagamento confermato?")]
        [Required(ErrorMessage = "Stato obbligatorio")]
        public required bool Stato {
            get; set;
        }
    }
}
