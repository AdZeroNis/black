using IbulakStoreServer.Data.Domain;
using IbulakStoreServer.Data.Entities;
using IbulakStoreServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IbulakStoreServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriController : ControllerBase
    {
        private readonly CategoriService _categoriService;
        public CategoriController(CategoriService categoriService)
        {
            _categoriService = categoriService;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _categoriService.GetAsync(id);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> Gets()
        {
            var result = await _categoriService.GetsAsync();
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Add(Categori categori)
        {
            await _categoriService.AddAsync(categori);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Edit([FromBody]Categori categori)
        {
            await _categoriService.EditAsync(categori);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoriService.DeleteAsync(id);
            return Ok();
        }
    }
}
