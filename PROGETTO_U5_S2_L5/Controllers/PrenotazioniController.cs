using Microsoft.AspNetCore.Mvc;
using PROGETTO_U5_S2_L5.Services;
using PROGETTO_U5_S2_L5.ViewModels;

namespace PROGETTO_U5_S2_L5.Controllers {
    public class PrenotazioniController : Controller {
        private readonly PrenotazioniService _prenotazioniService;

        public PrenotazioniController(PrenotazioniService prenotazioniService) {
            _prenotazioniService = prenotazioniService;
        }

        public IActionResult Index() { //View lista prenotazioni
            return View();
        }

        [HttpGet("/prenotazioni/get-all")]
        public async Task<IActionResult> GetPrenotazioni() { //Action per ottenere tutte le prenotazioni
            try {
                var prenotazioni = await _prenotazioniService.GetAllPrenotazioniAsync();

                return PartialView("_PrenotazioniListPartialView", prenotazioni);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(500);
            }
        }

        [Route("/prenotazione/add")]
        public IActionResult AddPrenotazione() { //Partial view form aggiungi prenotazione
            return PartialView("_AddPrenotazionePartialView");
        }

        public async Task<IActionResult> Clienti() { //View lista clienti
            try {
                var clienti = await _prenotazioniService.GetAllClientiAsync();

                return View(clienti);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public IActionResult AddCliente() { //View form aggiungi cliente
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCliente(AddClienteViewModel addClienteViewModel) { //Action per aggiungere cliente
            var result = await _prenotazioniService.AddClienteAsync(addClienteViewModel);

            if (result) {
                return RedirectToAction("Clienti");
            } else {
                return RedirectToAction("Clienti");
            }
        }

        public async Task<IActionResult> EditCliente(Guid id) { //View form edit cliente
            var cliente = await _prenotazioniService.GetClienteByIdAsync(id);

            if (cliente == null) {
                TempData["Error"] = "Error while finding entity on database";
                return RedirectToAction("Clienti");
            }

            var editClienteViewModel = new EditClienteViewModel() {
                ClienteId = cliente.ClienteId,
                Nome = cliente.Nome,
                Cognome = cliente.Cognome,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };

            return View(editClienteViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCliente(EditClienteViewModel editClienteViewModel) { //Action per editare cliente
            var result = await _prenotazioniService.EditClienteAsync(editClienteViewModel);

            if (result) {
                return RedirectToAction("Clienti");
            } else {
                return RedirectToAction("Clienti");
            }
        }

        public async Task<IActionResult> DeleteCliente(Guid id) { //Action per eliminare cliente
            var result = await _prenotazioniService.DeleteClienteAsync(id);

            if (result) {
                return RedirectToAction("Clienti");
            } else {
                return RedirectToAction("Clienti");
            }
        }

        public async Task<IActionResult> Camere() { //View lista camere
            try {
                var camere = await _prenotazioniService.GetAllCamereAsync();

                return View(camere);
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return RedirectToAction("Index");
            }
        }

        public IActionResult AddCamera() { //View form aggiungi camera
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddCamera(AddCameraViewModel addCameraViewModel) { //Action per aggiungere camera
            var result = await _prenotazioniService.AddCameraAsync(addCameraViewModel);

            if (result) {
                return RedirectToAction("Camere");
            } else {
                return RedirectToAction("Camere");
            }
        }

        public async Task<IActionResult> EditCamera(Guid id) { //View form edit camera
            var camera = await _prenotazioniService.GetCameraByIdAsync(id);

            if (camera == null) {
                TempData["Error"] = "Error while finding entity on database";
                return RedirectToAction("Camere");
            }

            var editCameraViewModel = new EditCameraViewModel() {
                CameraId = camera.CameraId,
                Numero = camera.Numero,
                Tipo = camera.Tipo,
                Prezzo = camera.Prezzo
            };

            return View(editCameraViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditCamera(EditCameraViewModel editCameraViewModel) { //Action per editare camera
            var result = await _prenotazioniService.EditCameraAsync(editCameraViewModel);

            if (result) {
                return RedirectToAction("Camere");
            } else {
                return RedirectToAction("Camere");
            }
        }

        public async Task<IActionResult> DeleteCamera(Guid id) { //Action per eliminare camera
            var result = await _prenotazioniService.DeleteCameraAsync(id);

            if (result) {
                return RedirectToAction("Camere");
            } else {
                return RedirectToAction("Camere");
            }
        }
    }
}
