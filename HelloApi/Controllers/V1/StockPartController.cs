using HelloApi.Entities;
using HelloApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HelloApi.Controllers.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TStockPartController : ControllerBase
    {
        private readonly ITStockPartRepository _repository;


        public TStockPartController(ITStockPartRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        [SwaggerOperation(Summary = "Lister toutes les pièces", Tags = new[] { "TStockPart – CRUD" })]
        public async Task<ActionResult<IEnumerable<TStockPartEntity>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return Ok(entities);
        }


        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Récupérer une pièce par Id", Tags = new[] { "TStockPart – CRUD" })]
        public async Task<ActionResult<TStockPartEntity>> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(entity);
        }


        [HttpGet("by-code/{partCode}")]
        [SwaggerOperation(Summary = "Récupérer une pièce par code", Tags = new[] { "TStockPart – Lookup" })]
        public async Task<ActionResult<TStockPartEntity>> GetByPartCode(string partCode)
        {
            var entity = await _repository.GetByPartCodeAsync(partCode);
            if (entity == null) return NotFound();
            return Ok(entity);
        }


        [HttpPost]
        [SwaggerOperation(Summary = "Ajouter une pièce", Tags = new[] { "TStockPart – CRUD" })]
        public async Task<ActionResult<TStockPartEntity>> Create([FromBody] TStockPartEntity model)
        {
            var created = await _repository.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }


        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Modifier une pièce", Tags = new[] { "TStockPart – CRUD" })]
        public async Task<IActionResult> Update(int id, [FromBody] TStockPartEntity model)
        {
            if (id != model.Id) return BadRequest();
            var updated = await _repository.UpdateAsync(model);
            if (updated == null) return NotFound();
            return Ok(updated);
        }


        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Supprimer une pièce", Tags = new[] { "TStockPart – CRUD" })]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _repository.DeleteAsync(id);
            if (!ok) return NotFound();
            return NoContent();
        }

    }
}
