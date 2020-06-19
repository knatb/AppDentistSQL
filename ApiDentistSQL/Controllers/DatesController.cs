using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDentistSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDentistSQL.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class DatesController : ControllerBase
    {
        // GET: api/Dates
        [HttpGet]
        public List<DateModel> Get()
        {
            return new DateModel().GetAll();
        }

        // GET: api/Dates/5
        [HttpGet("{id}", Name = "GetDateById")]
        public DateModel Get(int id)
        {
            return new DateModel().Get(id);
        }

        // POST: api/Dates
        [HttpPost]
        public ApiResponse Post([FromBody] DateModel date)
        {
            return date.Insert();
        }

        // PUT: api/Dates/5
        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] DateModel date)
        {
            return date.Update(id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new DateModel().Delete(id);
        }
    }
}
