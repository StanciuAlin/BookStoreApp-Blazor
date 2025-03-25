using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Author;
using AutoMapper;
using BookStoreApp.API.Static;
using Microsoft.AspNetCore.Authorization;
using AutoMapper.QueryableExtensions;
using BookStoreApp.API.Repositories;
using BookStoreApp.API.Models;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository _authorsRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthorsController> _logger;

        public AuthorsController(IAuthorsRepository authorsRepository, IMapper mapper, ILogger<AuthorsController> logger)
        {
            this._authorsRepository = authorsRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        // GET: api/Authors/?startindex=0&pageindex=15
        [HttpGet]
        public async Task<ActionResult<VirtualizeResponse<AuthorReadOnlyDto>>> GetAuthors([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                //var authors = _mapper.Map<IEnumerable<AuthorReadOnlyDto>>(await _authorsRepository.GetAllAsync());
                var authorDtos = await _authorsRepository.GetAllAsync<AuthorReadOnlyDto>(queryParameters);
                return Ok(authorDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GET Error in {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }
        
        // GET: api/Authors/
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<AuthorReadOnlyDto>>> GetAuthors()
        {
            try
            {
                var authors = _mapper.Map<IEnumerable<AuthorReadOnlyDto>>(await _authorsRepository.GetAllAsync());
                return Ok(authors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GET Error in {nameof(GetAuthors)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDetailsDto>> GetAuthor(int id)
        {
            try
            {
                var author = _authorsRepository.GetAuthorDetailsAsync(id);

                if (author == null)
                {
                    _logger.LogWarning($"Record Not Found: {nameof(GetAuthor)} - Id: {id}");
                    return NotFound();
                }

                return Ok(author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"GET Error in {nameof(GetAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }

            
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAuthor(int id, AuthorUpdateDto authorDto)
        {
            if (id != authorDto.Id)
            {
                _logger.LogWarning($"Update Id invalid in {nameof(PutAuthor)} - Id: {id}");
                return BadRequest();
            }
            var author = await _authorsRepository.GetAsync(id);

            if (author == null)
            {
                _logger.LogWarning($"{nameof(Author)} record not found in {nameof(PutAuthor)} - Id: {id}");
                return NotFound();
            }

            _mapper.Map(authorDto, author);

            try
            {
                await _authorsRepository.UpdateAsync(author);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (! await AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, $"GET error in {nameof(PutAuthor)}");
                    return StatusCode(500, Messages.Error500Message);
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            //var author = new Author
            //{
            //    FirstName = authorDto.FirstName,
            //    LastName = authorDto.LastName,
            //    Bio = authorDto.Bio
            //};
            try
            {
                var author = _mapper.Map<Author>(authorDto);

                await _authorsRepository.AddAsync(author);

                return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"POST error in {nameof(PostAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var author = await _authorsRepository.GetAsync(id);
                if (author == null)
                {
                    _logger.LogWarning($"{nameof(Author)} record not found in {nameof(DeleteAuthor)} - Id: {id}");
                    return NotFound();
                }

                await _authorsRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"DELETE error in {nameof(DeleteAuthor)}");
                return StatusCode(500, Messages.Error500Message);
            }
        }

        private async Task<bool> AuthorExists(int id)
        {
            return await _authorsRepository.Exists(id);
        }
    }
}
