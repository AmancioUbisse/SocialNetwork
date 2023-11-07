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
    public class ArticleController : ControllerBase
    {
        private readonly SocialNetworkDbContext dbContext;

        public ArticleController(SocialNetworkDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //GET ALL REGIONS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Domain models
            var articlesDomain = await dbContext.Article.ToListAsync();

            //Map Domain Models to DTOs
            var articlesDto = articlesDomain.Select(articlesDomain => new ArticleDto
            {
                Id = articlesDomain.Id,
                Title = articlesDomain.Title,
                Content = articlesDomain.Content,
                Password = articlesDomain.Password,
                PhoneNo = articlesDomain.PhoneNo,
                IsActive = articlesDomain.IsActive,
                IsApproved = articlesDomain.IsApproved
            }).ToList();

            //Return DTOs
            return Ok(articlesDto);
        }

        //GET SINGLE REGION (Get Region By ID)
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Region Domain Model From DataBase
            var articlesDomain = await dbContext.Article.FirstOrDefaultAsync(x => x.Id == id);

            if (articlesDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Region Dto
            var articlesDto = new ArticleDto
            {
                Id = articlesDomain.Id,
                Title = articlesDomain.Title,
                Content = articlesDomain.Content,
                Password = articlesDomain.Password,
                PhoneNo = articlesDomain.PhoneNo,
                IsActive = articlesDomain.IsActive,
                IsApproved = articlesDomain.IsApproved
            };
            return Ok(articlesDto);
        }

        //Post To Create New Region
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddArticleRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var articleDomainModel = new Article
            {
                Title = addRegionRequestDto.Title,
                Content = addRegionRequestDto.Content,
                Password = addRegionRequestDto.Password,
                PhoneNo = addRegionRequestDto.PhoneNo,
                IsActive = addRegionRequestDto.IsActive,
                IsApproved = addRegionRequestDto.IsApproved
            };

            //Use Domain Model to Create Region
            await dbContext.Article.AddAsync(articleDomainModel);
            await dbContext.SaveChangesAsync();

            //Map Domain model back to DTO
            var articleDto = new ArticleDto
            {
                Id = articleDomainModel.Id,
                Title = articleDomainModel.Title,
                Content = articleDomainModel.Content,
                Password = articleDomainModel.Password,
                PhoneNo = articleDomainModel.PhoneNo,
                IsActive = articleDomainModel.IsActive,
                IsApproved = articleDomainModel.IsApproved
            };

            return CreatedAtAction(nameof(GetById), new { id = articleDto.Id }, articleDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Article updateArticleRequestDto)
        {
            // Verifica se a região com o ID especificado existe
            var articleDomainModel = await dbContext.Article.FirstOrDefaultAsync(x => x.Id == id);

            if (articleDomainModel == null)
            {
                return NotFound();
            }

            // Atualiza as propriedades da região com base nos valores fornecidos no DTO de atualização
            articleDomainModel.Title = updateArticleRequestDto.Title;
            articleDomainModel.Content = updateArticleRequestDto.Content;
            articleDomainModel.Password = updateArticleRequestDto.Password;
            articleDomainModel.PhoneNo = updateArticleRequestDto.PhoneNo;
            articleDomainModel.IsActive = updateArticleRequestDto.IsActive;
            articleDomainModel.IsApproved = updateArticleRequestDto.IsApproved;

            dbContext.Article.Update(articleDomainModel);
            await dbContext.SaveChangesAsync();

            // Mapeia o modelo de domínio de volta para um DTO
            var articleDto = new ArticleDto
            {
                Id = articleDomainModel.Id,
                Title = articleDomainModel.Title,
                Content = articleDomainModel.Content,
                Password = articleDomainModel.Password,
                PhoneNo = articleDomainModel.PhoneNo,
                IsActive = articleDomainModel.IsActive,
                IsApproved = articleDomainModel.IsApproved
            };

            return Ok(articleDto);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteById(Guid id)
        {
            var articleDomainModel = await dbContext.Article.FindAsync(id);

            if (articleDomainModel == null)
            {
                return NotFound();
            }

            dbContext.Article.Remove(articleDomainModel);
            await dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
