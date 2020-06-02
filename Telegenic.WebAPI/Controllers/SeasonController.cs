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
    public class SeasonController : ApiController
    {
        private readonly SeasonRepository _seasonRepository;

        public SeasonController(ISeasonRepository seasonRepository)
        {
            _seasonRepository = (SeasonRepository)seasonRepository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var season = _seasonRepository.GetAll().Cast<Season>();
            if (season == null || !season.Any())
            {
                return NotFound();
            }

            return Ok(season);
        }

        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            var season = _seasonRepository.GetById(Id);

            if (season == null)
            {
                return NotFound();
            }

            return Ok(season);
        }

        [HttpGet]
        public IHttpActionResult Get(string title)
        {
            var season = _seasonRepository.GetByTitle(title);

            if (season == null)
            {
                return NotFound();
            }

            return Ok(season);
        }

        [HttpPost]
        public IHttpActionResult Post(Season season)
        {
            _seasonRepository.Save(season);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Season season)
        {
            if (_seasonRepository.Delete(season))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            string message = $"Unable to delete Season ID {season.Id}";
            return BadRequest(message);
        }

    }
}
