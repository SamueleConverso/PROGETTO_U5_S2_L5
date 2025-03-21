using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S2_L5.ViewModels {
    public class EditCameraViewModel {
        public Guid CameraId {
            get; set;
        }

        [Display(Name = "Numero")]
        [Required(ErrorMessage = "Numero obbligatorio")]
        public required int Numero {
            get; set;
        }

        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "Tipo obbligatorio")]
        public required string Tipo {
            get; set;
        }

        [Display(Name = "Prezzo")]
        [Required(ErrorMessage = "Prezzo obbligatorio")]
        public required decimal Prezzo {
            get; set;
        }
    }
}
