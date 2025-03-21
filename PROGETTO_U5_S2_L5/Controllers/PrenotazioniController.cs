using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PROGETTO_U5_S2_L5.Services;
using PROGETTO_U5_S2_L5.ViewModels;

namespace PROGETTO_U5_S2_L5.Controllers {
    public class PrenotazioniController : Controller {
        private readonly PrenotazioniService _prenotazioniService;

        public PrenotazioniController(PrenotazioniService prenotazioniService) {
            _prenotazioniService = prenotazioniService;
        }

        [Authorize]
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
        public async Task<IActionResult> AddPrenotazione() { //Partial view form aggiungi prenotazione

            var clienti = await _prenotazioniService.GetAllClientiAsync();
            var camere = await _prenotazioniService.GetAllCamereAsync();

            ViewBag.Clienti = clienti;
            ViewBag.Camere = camere;

            return PartialView("_AddPrenotazionePartialView");
        }

        [HttpPost]
        [Route("/prenotazione/add")]
        public async Task<IActionResult> Add(AddPrenotazioneViewModel addPrenotazioneViewModel) { //Action per aggiungere prenotazione
            if (!ModelState.IsValid) {
                return Json(new {
                    success = false,
                    message = "Error while saving entity to database"
                });
            }

            var result = await _prenotazioniService.AddPrenotazioneAsync(addPrenotazioneViewModel);

            if (!result) {
                return Json(new {
                    success = false,
                    message = "Error while saving entity to database"
                });
            }

            Console.WriteLine("Entity saved successfully to database");

            return Json(new {
                success = true,
                message = "Entity saved successfully to database"
            });
        }

        [Route("/prenotazione/update/{id:guid}")]
        public async Task<IActionResult> EditPrenotazione(Guid id) { //Partial view form edit prenotazione
            var clienti = await _prenotazioniService.GetAllClientiAsync();
            var camere = await _prenotazioniService.GetAllCamereAsync();

            ViewBag.Clienti = clienti;
            ViewBag.Camere = camere;

            var prenotazione = await _prenotazioniService.GetPrenotazioneByIdAsync(id);

            if (prenotazione == null) {
                TempData["Error"] = "Error while finding entity on database";
                return RedirectToAction("Index");
            }

            var editPrenotazioneViewModel = new EditPrenotazioneViewModel() {
                PrenotazioneId = prenotazione.PrenotazioneId,
                ClienteId = prenotazione.ClienteId,
                CameraId = prenotazione.CameraId,
                DataInizio = prenotazione.DataInizio,
                DataFine = prenotazione.DataFine,
                Stato = prenotazione.Stato
            };

            return PartialView("_EditPrenotazionePartialView", editPrenotazioneViewModel);
        }

        [Route("/prenotazione/update/save")]
        public async Task<IActionResult> SaveEditPrenotazione(EditPrenotazioneViewModel editPrenotazioneViewModel) { //Action per editare prenotazione
            var result = await _prenotazioniService.EditPrenotazioneAsync(editPrenotazioneViewModel);

            if (!result) {
                return Json(new {
                    success = false,
                    message = "Error while updating entity on database"
                });
            }

            return Json(new {
                success = true,
                message = "Update done successfully"
            });
            ;
        }

        [HttpPost]
        [Route("/prenotazione/delete/{id:guid}")]
        public async Task<IActionResult> DeletePrenotazione(Guid id) { //Action per eliminare prenotazione
            var result = await _prenotazioniService.DeletePrenotazioneByIdAsync(id);

            if (!result) {
                return Json(new {
                    success = false,
                    message = "Error while deleting entity"
                });
            }

            string logmessage = "Entity deleted successfully";
            Console.WriteLine(logmessage);

            return Json(new {
                success = true,
                message = logmessage
            });
        }

        public async Task<IActionResult> Clienti() { //View lista clienti
            if (User.IsInRole("Dipendente")) {
                return RedirectToAction("Index", "Home");
            }

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
            if (User.IsInRole("Dipendente")) {
                return RedirectToAction("Index", "Home");
            }

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
