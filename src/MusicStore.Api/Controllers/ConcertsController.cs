using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Dto.Request;
using MusicStore.Dto.Response;
using MusicStore.Entities;
using MusicStore.Repositories.Abstractions;
using MusicStore.Services.Abstractions;

namespace MusicStore.Api.Controllers
{
    [ApiController]
    [Route("api/concerts")]
    public class ConcertsController : ControllerBase
    {
        private readonly IConcertRepository repository;
        private readonly IGenreRepository genreRepository;
        private readonly ILogger<ConcertsController> logger;
        private readonly IConcertService concertService;

        public ConcertsController(IConcertService service)
        {
            this.concertService = service;
        }

        [HttpGet("title")]
        public async Task<IActionResult> Get(string? title)
        {
            var response = await concertService.GetAsync(title);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await concertService.GetAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(ConcertRequestDto request)
        {
            var response = await concertService.AddAsync(request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, ConcertRequestDto request)
        {
            var response = await concertService.UpdateAsync(id, request);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await concertService.DeleteAsync(id);
            return Ok(response);
        }
        [HttpPatch("{id:int}")]
        public async Task<IActionResult> Patch(int id)
        {
            return Ok(await concertService.FinalizeAsync(id));
        }
    }
}
