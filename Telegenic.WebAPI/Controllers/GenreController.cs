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
    public class GenreController : ApiController
    {
        private readonly GenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = (GenreRepository)genreRepository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var genres = _genreRepository.GetAll().Cast<Genre>();
            if (genres == null || !genres.Any())
            {
                return NotFound();
            }
            return Ok(genres);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var genre = _genreRepository.GetById(id);
            if (genre == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No Genre with ID {id}"),
                    ReasonPhrase = "Genre ID not found"
                };
                throw new HttpResponseException(response);
            }

            return Ok(genre);
        }

        [HttpGet]
        public IHttpActionResult Get(string title)
        {
            var genre = _genreRepository.GetByTitle(title);

            if (genre == null)
            {
                return NotFound();
            }

            return Ok(genre);
        }

        [HttpPost]
        public IHttpActionResult Post(Genre genre)
        {
            _genreRepository.Save(genre);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Genre genre)
        {
            if (_genreRepository.Delete(genre))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            string message = $"Unable to delete Genre ID {genre.Id}";

            return BadRequest(message);
        }
    }
}
