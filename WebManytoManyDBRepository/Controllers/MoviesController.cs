using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebManytoManyDBRepository.DBManagement;
using WebManytoManyDBRepository.Entities;

namespace WebManytoManyDBRepository.Controllers
{
    [ApiController]
    [Route("Movies")]
    public class MoviesController : ControllerBase
    {
        private IDbContextFactory<MovieContext> _dbContextFactory;

        private GenericRepository<Movie, long> _moviesRepo;

        public MoviesController(IDbContextFactory<MovieContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;

            _moviesRepo = new GenericRepository<Movie, long>();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var result = _moviesRepo.GetAll(context, "Genres");

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
                var result = await _moviesRepo.GetByIdAsync(context, id, "Genres");

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
                var result = await _moviesRepo.DeleteByIdAsync(context, id);

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
                var result = await _moviesRepo.DeleteAllAsync(context);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Movie newMovie)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var result = await _moviesRepo.InsertAsync(context, newMovie);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Movie updateMovie)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                var result = await _moviesRepo.Update(context, updateMovie, "Genres");

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}