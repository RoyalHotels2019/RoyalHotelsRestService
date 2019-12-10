using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelLibrary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoyalHotelsRestService.DBUtil;

namespace RoyalHotelsRestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelTempsController : ControllerBase
    {
        HotelTempsManager manager = new HotelTempsManager();

        // GET: api/Hotels
        [HttpGet]
        public IEnumerable<Temperaturmaaling> Get()
        {
            return manager.Get();
        }


        // GET: api/Hotels/5
        [HttpGet("/Recent", Name = "GetRecent")]
        public Temperaturmaaling GetRecent()
        {
            return manager.GetRecent();
        }
        // POST: api/HotelTemps
        [HttpPost]
        public bool Post([FromBody] Temperaturmaaling maaling)
        {
            return manager.Post(maaling);
        }

        // PUT: api/HotelTemps/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{Tempe_Date}")]
        public bool Delete(DateTime date)
        {
            return manager.Delete(date);
        }
    }
}
