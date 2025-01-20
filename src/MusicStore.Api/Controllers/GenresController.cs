using System.Net;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories;
using MusicStore.Repositories.Abstractions;

namespace MusicStore.Api.Controllers
{
    [ApiController]
    [Route("/api/genres")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreRepository _repository;
        private readonly ILogger<GenresController> _logger;

        public GenresController(IGenreRepository repository, ILogger<GenresController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get() {
            //var data = 
            //return Ok(data);
            var response = new BaseResponseGeneric<ICollection<GenreResponseDto>>();

            try
            {
                response.Data = await _repository.GetAsync();
                response.Success = true;

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información";
                _logger.LogError(ex, $"{response.ErrorMessage} - {ex.Message}");
                return BadRequest(response);

            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id) {

            var response = new BaseResponseGeneric<GenreResponseDto>();

            try
            {
                response.Data = await _repository.GetAsync(id);
                response.Success = true;

                return response.Data is not null ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al obtener la información";
                _logger.LogError(ex, $"{response.ErrorMessage} - {ex.Message}");
                return BadRequest(response);              
            }

        }

        [HttpPost]
        public async Task<IActionResult> Post(GenreRequestDto genre) {

            var response = new BaseResponseGeneric<int>();

            
            try
            {
                var genreId = await _repository.AddAsync(genre);
                response.Success = true;
                response.Data = genreId;
                
                return StatusCode((int)HttpStatusCode.Created, response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al insertar";
                _logger.LogError(ex, $"{response.ErrorMessage} - {ex.Message}");
                return BadRequest(response);
                
            }

            
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, GenreRequestDto genre) {
            var response = new BaseResponse();

            try
            {
                await _repository.UpdateAsync(id, genre);
                response.Success = true;
                _logger.LogInformation($"Género musical con id {id} actualizado.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al actualizar.";
                _logger.LogError($"{response.ErrorMessage} - {ex.Message}");
                return BadRequest(response);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) {
            var response = new BaseResponse();

            try
            {
                await _repository.DeleteAsync(id);
                response.Success = true;
                _logger.LogInformation($"Deleted {id}");
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Ocurrió un error al eliminar.";
                _logger.LogError($"{response.ErrorMessage} - {ex.Message}");
                return BadRequest(response);
            }
            
        }
    }
}
