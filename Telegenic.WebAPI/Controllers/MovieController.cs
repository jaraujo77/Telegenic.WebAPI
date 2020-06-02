using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Telegenic.Entities.Models;
using Telegenic.Repository;
using Telegenic.Repository.Interfaces;

namespace Telegenic.WebAPI.Controllers
{
    public class MovieController : ApiController
    {
        private readonly MovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = (MovieRepository)movieRepository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var movies = _movieRepository.GetFeaturedItems().Cast<Movie>();
            if (movies == null || !movies.Any())
            {
                return NotFound();
            }

            return Ok(movies);
        }

        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            var movie = _movieRepository.GetById(Id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpGet]
        public IHttpActionResult Get(string title)
        {
            var movie = _movieRepository.GetByTitle(title);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        [HttpPost]
        public IHttpActionResult Post(Movie movie)
        {
            _movieRepository.Save(movie);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Movie movie)
        {
            if (_movieRepository.Delete(movie))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            string message = $"Unable to delete Movie ID {movie.Id}";
            return BadRequest(message);
        }
    }
}
