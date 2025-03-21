using System.ComponentModel.DataAnnotations;

namespace PROGETTO_U5_S2_L5.ViewModels {
    public class AddClienteViewModel {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Nome obbligatorio")]
        public required string Nome {
            get; set;
        }

        [Display(Name = "Cognome")]
        [Required(ErrorMessage = "Cognome obbligatorio")]
        public required string Cognome {
            get; set;
        }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email obbligatoria")]
        [EmailAddress(ErrorMessage = "Inserisci un'email valida")]
        public required string Email {
            get; set;
        }

        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "Telefono obbligatorio")]
        public required string Telefono {
            get; set;
        }
    }
}
