using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebManytoManyDBRepository.DBManagement;
using WebManytoManyDBRepository.Entities;

namespace WebManytoManyDBRepository.Controllers
{
    [ApiController]
    [Route("Genres")]
    public class GenreController : ControllerBase
    {
        private IDbContextFactory<MovieContext> _dbContextFactory;

        private GenericRepository<Genre, long> _genresRepo;

        public GenreController(IDbContextFactory<MovieContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;

            _genresRepo = new GenericRepository<Genre, long>();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var result = _genresRepo.GetAll(context, "Movies");

                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var result = await _genresRepo.GetByIdAsync(context, id, "Movies");

                return Ok(result);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById(long id)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var result = await _genresRepo.DeleteByIdAsync(context, id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var result = await _genresRepo.DeleteAllAsync(context);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Genre newGenre)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var result = await _genresRepo.InsertAsync(context, newGenre);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Genre updateGenre)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var result = await _genresRepo.Update(context, updateGenre, "Movies");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}