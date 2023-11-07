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
    public class NewsController : ControllerBase
    {
        private readonly SocialNetworkDbContext dbContext;

        public NewsController(SocialNetworkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET ALL REGIONS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Domain models
            var newsDomain = await dbContext.News.ToListAsync();

            //Map Domain Models to DTOs
            var newsDto = newsDomain.Select(newsDomain => new News
            {
                Id = newsDomain.Id,
                Title = newsDomain.Title,
                Content = newsDomain.Content,
                Email = newsDomain.Email,
                Image = newsDomain.Image,
                IsActive = newsDomain.IsActive,
                IsApproved = newsDomain.IsApproved
            }).ToList();

            //Return DTOs
            return Ok(newsDto);
        }

        //GET SINGLE REGION (Get Region By ID)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Region Domain Model From DataBase
            var newsDomain = await dbContext.News.FirstOrDefaultAsync(x => x.Id == id);

            if (newsDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Region Dto
            var newsDto = new NewsDto
            {
                Id = newsDomain.Id,
                Title = newsDomain.Title,
                Content = newsDomain.Content,
                Email = newsDomain.Email,
                Image = newsDomain.Image,
                IsActive = newsDomain.IsActive,
                IsApproved = newsDomain.IsApproved
            };
            return Ok(newsDto);
        }

        //Post To Create New Region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddNewsRequestDto addNewsRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var newsDomainModel = new News
            {
                Title = addNewsRequestDto.Title,
                Content = addNewsRequestDto.Content,
                Email = addNewsRequestDto.Email,
                Image = addNewsRequestDto.Image,
                IsActive = addNewsRequestDto.IsActive,
                IsApproved = addNewsRequestDto.IsApproved
            };

            //Use Domain Model to Create Region
            await dbContext.News.AddAsync(newsDomainModel);
            await dbContext.SaveChangesAsync();

            //Map Domain model back to DTO
            var newsDto = new NewsDto
            {
                Id = newsDomainModel.Id,
                Title = newsDomainModel.Title,
                Content = newsDomainModel.Content,
                Email = newsDomainModel.Email,
                Image = newsDomainModel.Image,
                IsActive = newsDomainModel.IsActive,
                IsApproved = newsDomainModel.IsApproved
            };

            return CreatedAtAction(nameof(GetById), new { id = newsDto.Id }, newsDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] News updateNewsRequestDto)
        {
            // Verifica se a região com o ID especificado existe
            var newsDomainModel = await dbContext.News.FirstOrDefaultAsync(x => x.Id == id);

            if (newsDomainModel == null)
            {
                return NotFound();
            }

            // Atualiza as propriedades da região com base nos valores fornecidos no DTO de atualização
            newsDomainModel.Title = updateNewsRequestDto.Title;;
            newsDomainModel.Content = updateNewsRequestDto.Content;
            newsDomainModel.Email = updateNewsRequestDto.Email;
            newsDomainModel.Image = updateNewsRequestDto.Image;
            newsDomainModel.IsActive = updateNewsRequestDto.IsActive;
            newsDomainModel.IsApproved = updateNewsRequestDto.IsApproved;

            dbContext.News.Update(newsDomainModel);
            await dbContext.SaveChangesAsync();

            // Mapeia o modelo de domínio de volta para um DTO
            var newsDto = new NewsDto
            {
                Id = newsDomainModel.Id,
                Title = newsDomainModel.Title,
                Content = newsDomainModel.Content,
                Email = newsDomainModel.Email,
                Image = newsDomainModel.Image,
                IsActive = newsDomainModel.IsActive,
                IsApproved = newsDomainModel.IsApproved
            };

            return Ok(newsDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var newsDomainModel = await dbContext.News.FindAsync(id);

            if (newsDomainModel == null)
            {
                return NotFound();
            }

            dbContext.News.Remove(newsDomainModel);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
