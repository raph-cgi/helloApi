using HelloApi.Data; // Change this to the correct namespace where TPerson and HelloApiContext are defined
using HelloApi.Entities;
using HelloApi.Models.V1;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace HelloApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TPersonController : ControllerBase
    {
        private readonly ITPersonRepository _repository;

        public TPersonController(ITPersonRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Liste toutes les personnes", Tags = new[] { "TPerson – CRUD" })]
        public async Task<ActionResult<IEnumerable<TPerson>>> GetAll()
        {
            var entities = await _repository.GetAllAsync();
            return entities.Select(e => MapToTPerson(e)).ToList();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Récupérer une personnes", Tags = new[] { "TPerson – CRUD" })]
        public async Task<ActionResult<TPerson>> GetById(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            return MapToTPerson(entity);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Ajouter une personne", Tags = new[] { "TPerson – CRUD" })]
        public async Task<ActionResult<TPerson>> Create(TPerson person)
        {
            var entity = MapToTPersonEntity(person);
            await _repository.AddAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, MapToTPerson(entity));
        }

        [SwaggerOperation(Summary = "Ajouter une personne par requête pour utilisation d'un QRCode", Tags = new[] { "TPerson – QRCode" })]
        [HttpPost("CreateTPersonFromQuery")]
        public async Task<IActionResult> CreateTPersonFromQuery(
        [FromQuery] string nom,
        [FromQuery] string prenom,
        [FromQuery] DateTime dateBorn,
        [FromQuery] DateTime? dateDead = null) // ✅ Nullable
        {
            if (string.IsNullOrWhiteSpace(nom) || string.IsNullOrWhiteSpace(prenom))
                return BadRequest("Nom et prénom sont obligatoires");

            var entity = new TPersonEntity
            {
                Nom = nom,
                Prenom = prenom,
                DateBorn = dateBorn,
                DateDead = dateDead // ✅ Affectation si présent
            };

            await _repository.AddAsync(entity);

            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Modifier une personne", Tags = new[] { "TPerson – CRUD" })]
        public async Task<IActionResult> Update(int id, TPerson person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            var entity = MapToTPersonEntity(person);
            var updatedEntity = await _repository.UpdateAsync(entity);

            if (updatedEntity == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Supprimer une personne", Tags = new[] { "TPerson – CRUD" })]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _repository.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound();
            }
            return NoContent();
        }






        private TPerson MapToTPerson(TPersonEntity entity)
        {
            return new TPerson
            {
                Id = entity.Id,
                Nom = entity.Nom,
                Prenom = entity.Prenom,
                DateBorn = entity.DateBorn,
                DateDead = entity.DateDead
            };
        }

        private TPersonEntity MapToTPersonEntity(TPerson person)
        {
            return new TPersonEntity
            {
                Id = person.Id,
                Nom = person.Nom,
                Prenom = person.Prenom,
                DateBorn = person.DateBorn,
                DateDead = person.DateDead,

            };
        }
    }
}