// Controllers/MoviesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieProduct.Model;

[Route("api/[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieContext _context;

    public MoviesController(MovieContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAllMovies()
    {
        var movies = _context.movies.ToList();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public IActionResult GetMovieById(int id)
    {
        var movie = _context.movies.Find(id);
        if (movie == null)
            return NotFound();

        return Ok(movie);
    }

    [HttpPost]
    public IActionResult AddMovie([FromBody] Movie movie)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        _context.movies.Add(movie);
        _context.SaveChanges();

        return CreatedAtAction(nameof(GetMovieById), new { id = movie.id }, movie);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMovie(int id, [FromBody] Movie newMovie)
    {
        var oldMovie = _context.movies.Find(id);
        if (oldMovie == null)
            return NotFound();

        oldMovie.Title = newMovie.Title;
        oldMovie.Director = newMovie.Director;
        oldMovie.ReleaseYear = newMovie.ReleaseYear;

        _context.SaveChanges();

        return Ok(oldMovie);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMovie(int id)
    {
        var movie = _context.movies.Find(id);
        if (movie == null)
            return NotFound();

        _context.movies.Remove(movie);
        _context.SaveChanges();

        return NoContent();
    }
}
