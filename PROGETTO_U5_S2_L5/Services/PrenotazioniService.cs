using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using PROGETTO_U5_S2_L5.Data;
using PROGETTO_U5_S2_L5.Models;
using PROGETTO_U5_S2_L5.ViewModels;

namespace PROGETTO_U5_S2_L5.Services {
    public class PrenotazioniService {
        private readonly AppDbContext _context;

        public PrenotazioniService(AppDbContext context) {
            _context = context;
        }
        private async Task<bool> SaveAsync() {
            try {
                var rows = await _context.SaveChangesAsync();

                if (rows > 0) {
                    return true;
                } else {
                    return false;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<PrenotazioniViewModel> GetAllPrenotazioniAsync() {
            var prenotazioni = new PrenotazioniViewModel();

            try {
                prenotazioni.Prenotazioni = await _context.Prenotazioni.Include(p => p.Camera).Include(p => p.Cliente).ToListAsync();
            } catch (Exception ex) {
                prenotazioni.Prenotazioni = null;
                Console.WriteLine(ex.Message);
                throw;
            }

            return prenotazioni;
        }

        public async Task<bool> AddPrenotazioneAsync(AddPrenotazioneViewModel addPrenotazioneViewModel) {
            //var applicationUser = await _userManager.FindByEmailAsync(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value);

            try {
                var prenotazione = new Prenotazione() {
                    ClienteId = addPrenotazioneViewModel.ClienteId,
                    CameraId = addPrenotazioneViewModel.CameraId,
                    DataInizio = addPrenotazioneViewModel.DataInizio,
                    DataFine = addPrenotazioneViewModel.DataFine,
                    Stato = addPrenotazioneViewModel.Stato
                    //ApplicationUserId = applicationUser.Id,
                };

                _context.Prenotazioni.Add(prenotazione);

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Prenotazione> GetPrenotazioneByIdAsync(Guid id) {
            var prenotazione = await _context.Prenotazioni.FindAsync(id);

            if (prenotazione == null) {
                return null;
            }

            return prenotazione;
        }

        public async Task<bool> EditPrenotazioneAsync(EditPrenotazioneViewModel editPrenotazioneViewModel) {
            try {
                var prenotazione = await _context.Prenotazioni.FindAsync(editPrenotazioneViewModel.PrenotazioneId);

                if (prenotazione == null) {
                    return false;
                }

                prenotazione.ClienteId = editPrenotazioneViewModel.ClienteId;
                prenotazione.CameraId = editPrenotazioneViewModel.CameraId;
                prenotazione.DataInizio = editPrenotazioneViewModel.DataInizio;
                prenotazione.DataFine = editPrenotazioneViewModel.DataFine;
                prenotazione.Stato = editPrenotazioneViewModel.Stato;

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeletePrenotazioneByIdAsync(Guid id) {
            try {
                var prenotazione = await _context.Prenotazioni.FindAsync(id);

                if (prenotazione == null) {
                    Console.WriteLine($"Prenotazione not found");
                    return false;
                }

                _context.Prenotazioni.Remove(prenotazione);

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<ClientiViewModel> GetAllClientiAsync() {
            var clienti = new ClientiViewModel();

            try {
                clienti.Clienti = await _context.Clienti.ToListAsync();
            } catch (Exception ex) {
                clienti.Clienti = null;
                Console.WriteLine(ex.Message);
                throw;
            }

            return clienti;
        }

        public async Task<bool> AddClienteAsync(AddClienteViewModel addClienteViewModel) {
            var cliente = new Cliente {
                Nome = addClienteViewModel.Nome,
                Cognome = addClienteViewModel.Cognome,
                Email = addClienteViewModel.Email,
                Telefono = addClienteViewModel.Telefono
            };
            try {
                _context.Clienti.Add(cliente);
                var result = await SaveAsync();
                return result;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Cliente> GetClienteByIdAsync(Guid id) {
            var cliente = await _context.Clienti.FindAsync(id);

            if (cliente == null) {
                return null;
            }

            return cliente;
        }

        public async Task<bool> EditClienteAsync(EditClienteViewModel editClienteViewModel) {
            try {
                var cliente = await _context.Clienti.FindAsync(editClienteViewModel.ClienteId);

                if (cliente == null) {
                    return false;
                }

                cliente.Nome = editClienteViewModel.Nome;
                cliente.Cognome = editClienteViewModel.Cognome;
                cliente.Email = editClienteViewModel.Email;
                cliente.Telefono = editClienteViewModel.Telefono;

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteClienteAsync(Guid id) {
            var cliente = await _context.Clienti.FindAsync(id);
            if (cliente == null) {
                return false;
            }
            try {
                _context.Clienti.Remove(cliente);
                var result = await SaveAsync();
                return result;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<CamereViewModel> GetAllCamereAsync() {
            var camere = new CamereViewModel();

            try {
                camere.Camere = await _context.Camere.ToListAsync();
            } catch (Exception ex) {
                camere.Camere = null;
                Console.WriteLine(ex.Message);
                throw;
            }

            return camere;
        }

        public async Task<bool> AddCameraAsync(AddCameraViewModel addCameraViewModel) {
            var camera = new Camera {
                Numero = addCameraViewModel.Numero,
                Tipo = addCameraViewModel.Tipo,
                Prezzo = addCameraViewModel.Prezzo
            };
            try {
                _context.Camere.Add(camera);
                var result = await SaveAsync();
                return result;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<Camera> GetCameraByIdAsync(Guid id) {
            var camera = await _context.Camere.FindAsync(id);

            if (camera == null) {
                return null;
            }

            return camera;
        }

        public async Task<bool> EditCameraAsync(EditCameraViewModel editCameraViewModel) {
            try {
                var camera = await _context.Camere.FindAsync(editCameraViewModel.CameraId);

                if (camera == null) {
                    return false;
                }

                camera.Numero = editCameraViewModel.Numero;
                camera.Tipo = editCameraViewModel.Tipo;
                camera.Prezzo = editCameraViewModel.Prezzo;

                return await SaveAsync();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteCameraAsync(Guid id) {
            var camera = await _context.Camere.FindAsync(id);
            if (camera == null) {
                return false;
            }
            try {
                _context.Camere.Remove(camera);
                var result = await SaveAsync();
                return result;
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
