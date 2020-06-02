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
    public class EpisodeController : ApiController
    {
        private readonly EpisodeRepository _episodeRepository;

        public EpisodeController(IEpisodeRepository episodeRepository)
        {
            _episodeRepository = (EpisodeRepository)episodeRepository;
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var episode = _episodeRepository.GetAll().Cast<Episode>();
            if (episode == null || !episode.Any())
            {
                return NotFound();
            }

            return Ok(episode);
        }

        [HttpGet]
        public IHttpActionResult Get(int Id)
        {
            var episode = _episodeRepository.GetById(Id);

            if (episode == null)
            {
                return NotFound();
            }

            return Ok(episode);
        }

        [HttpGet]
        public IHttpActionResult Get(string title)
        {
            var episode = _episodeRepository.GetByTitle(title);

            if (episode == null)
            {
                return NotFound();
            }

            return Ok(episode);
        }

        [HttpPost]
        public IHttpActionResult Post(Episode episode)
        {
            _episodeRepository.Save(episode);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Episode episode)
        {
            if (_episodeRepository.Delete(episode))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            string message = $"Unable to delete Episode ID {episode.Id}";
            return BadRequest(message);
        }
    }
}
