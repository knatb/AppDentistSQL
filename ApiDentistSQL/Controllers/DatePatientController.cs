using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDentistSQL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDentistSQL.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DatePatientController : ControllerBase
    {
        // GET: api/DatePatient
        [HttpGet]
        public List<DatePatientModel> Get()
        {
            return new DatePatientModel().GetAll();
        }

        // GET: api/DatePatient/5
        [HttpGet("{id}", Name = "GetDatesFromPatient")]
        public List<DateModel> Get(int id)
        {
            return new DatePatientModel().GetDates(id);
        }

        // POST: api/DatePatient
        [HttpPost]
        public ApiResponse Post([FromBody] DatePatientModel dp)
        {
            return dp.InsertDateToPatient();
        }

        // PUT: api/DatePatient/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        public ApiResponse Delete([FromBody] DatePatientModel dp)
        {
            return dp.DeleteDateFromPatient(dp);
        }
    }
}
