using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.API.Data;
using SocialNetwork.API.Models.Domain;
using SocialNetwork.API.Models.DTO;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly SocialNetworkDbContext dbContext;

        public RegistrationController(SocialNetworkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET ALL REGIONS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Domain models
            var registrationDomain = await dbContext.Registration.ToListAsync();

            //Map Domain Models to DTOs
            var registrationDto = registrationDomain.Select(registrationDomain => new Registration
            {
                Id = registrationDomain.Id,
                Name = registrationDomain.Name,
                Email = registrationDomain.Email,
                Password = registrationDomain.Password,
                PhoneNo = registrationDomain.PhoneNo,
                IsActive = registrationDomain.IsActive,
                IsApproved = registrationDomain.IsApproved
            }).ToList();

            //Return DTOs
            return Ok(registrationDto);
        }

        //GET SINGLE REGION (Get Region By ID)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Region Domain Model From DataBase
            var registrationDomain = await dbContext.Registration.FirstOrDefaultAsync(x => x.Id == id);

            if (registrationDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Region Dto
            var registrationDto = new RegistrationDto
            {
                Id = registrationDomain.Id,
                Name = registrationDomain.Name,
                Email = registrationDomain.Email,
                Password = registrationDomain.Password,
                PhoneNo = registrationDomain.PhoneNo,
                IsActive = registrationDomain.IsActive,
                IsApproved = registrationDomain.IsApproved
            };
            return Ok(registrationDto);
        }

        //Post To Create New Region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegistrationRequestDto addRegistrationRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var registrationDomainModel = new Registration
            {
                Name = addRegistrationRequestDto.Name,
                Email = addRegistrationRequestDto.Email,
                Password = addRegistrationRequestDto.Password,
                PhoneNo = addRegistrationRequestDto.PhoneNo,
                IsActive = addRegistrationRequestDto.IsActive,
                IsApproved = addRegistrationRequestDto.IsApproved
            };

            //Use Domain Model to Create Region
            await dbContext.Registration.AddAsync(registrationDomainModel);
            await dbContext.SaveChangesAsync();

            //Map Domain model back to DTO
            var registrationDto = new RegistrationDto
            {
                Id = registrationDomainModel.Id,
                Name = registrationDomainModel.Name,
                Email = registrationDomainModel.Email,
                Password = registrationDomainModel.Password,
                PhoneNo = registrationDomainModel.PhoneNo,
                IsActive = registrationDomainModel.IsActive,
                IsApproved = registrationDomainModel.IsApproved
            };

            return CreatedAtAction(nameof(GetById), new { id = registrationDto.Id }, registrationDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Registration updateRegistrationRequestDto)
        {
            // Verifica se a região com o ID especificado existe
            var registrationDomainModel = await dbContext.Registration.FirstOrDefaultAsync(x => x.Id == id);

            if (registrationDomainModel == null)
            {
                return NotFound();
            }

            // Atualiza as propriedades da região com base nos valores fornecidos no DTO de atualização
            registrationDomainModel.Name = updateRegistrationRequestDto.Name;
            registrationDomainModel.Email = updateRegistrationRequestDto.Email;
            registrationDomainModel.Password = updateRegistrationRequestDto.Password;
            registrationDomainModel.PhoneNo = updateRegistrationRequestDto.PhoneNo;
            registrationDomainModel.IsActive = updateRegistrationRequestDto.IsActive;
            registrationDomainModel.IsApproved = updateRegistrationRequestDto.IsApproved;

            dbContext.Registration.Update(registrationDomainModel);
            await dbContext.SaveChangesAsync();

            // Mapeia o modelo de domínio de volta para um DTO
            var registrationDto = new RegistrationDto
            {
                Id = registrationDomainModel.Id,
                Name = registrationDomainModel.Name,
                Email = registrationDomainModel.Email,
                Password = registrationDomainModel.Password,
                PhoneNo = registrationDomainModel.PhoneNo,
                IsActive = registrationDomainModel.IsActive,
                IsApproved = registrationDomainModel.IsApproved
            };

            return Ok(registrationDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var registrationDomainModel = await dbContext.Registration.FindAsync(id);

            if (registrationDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Registration.Remove(registrationDomainModel);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}