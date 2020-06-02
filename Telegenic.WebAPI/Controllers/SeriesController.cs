using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Telegenic.Entities.Interfaces;
using Telegenic.Entities.Models;
using Telegenic.Repository;
using Telegenic.Repository.Interfaces;

namespace Telegenic.WebAPI.Controllers
{
    public class SeriesController : ApiController
    {
        private readonly SeriesRepository _seriesRepository;

        public SeriesController(ISeriesRepository seriesRepository)
        {
            _seriesRepository = (SeriesRepository)seriesRepository;
        }


        [HttpGet]
        public IEnumerable<Series> Get()
        {
            var series = _seriesRepository.GetFeaturedItems().Cast<Series>();
            return series;
        }

        [HttpGet]
        public Series Get(int id)
        {
            var series = _seriesRepository.GetById(id);

            if (series == null)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No Series with ID {id}"),
                    ReasonPhrase = "Series ID not found"
                };
                throw new HttpResponseException(response);
            }

            return series;
        }

        [HttpGet]
        public IEnumerable<Series> Get(string _Title)
        {
            var series = _seriesRepository.GetByTitle(_Title).Cast<Series>().ToList();

            if (series == null || !series.Any())
            {
                var response = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent($"No Series found starting with title {_Title}"),
                    ReasonPhrase = "Series matching title not found"
                };
                throw new HttpResponseException(response);
            }

            return series;
        }

        [HttpPost]
        public void Post(Series series)
        {            
            _seriesRepository.Save(series);
        }

        [HttpDelete]
        public IHttpActionResult Delete(Series series)
        {
            if (_seriesRepository.Delete(series))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            return BadRequest("Series not deleted");
        }


    }
}
