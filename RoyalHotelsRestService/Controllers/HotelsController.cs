using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HotelLibrary;
using RoyalHotelsRestService.DBUtil;

namespace RoyalHotelsRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        HotelTempsManager manager = new HotelTempsManager();

        // GET: api/Hotels
        [HttpGet]
        public IEnumerable<Temperaturmaaling> Get()
        {
            return manager.Get();
        }

        // GET: api/Hotels/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // GET: api/Hotels/5
        [HttpGet("/Recent", Name = "GetRecent")]
        public Temperaturmaaling GetRecent()
        {
            return manager.GetRecent();
        }

        // POST: api/Hotels
        [HttpPost]
        public bool Post([FromBody] TemperaturData maaling)
        {
            return manager.Post(maaling);
        }

        // PUT: api/Hotels/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
