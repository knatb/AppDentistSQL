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
    public class PatientController : ControllerBase
    {
        // GET: api/Patient
        [HttpGet]
        public List<PatientModel> Get()
        {
            return new PatientModel().GetAll();
        }

        // GET: api/Patient/5
        [HttpGet("{id}", Name = "PatientById")]
        public PatientModel Get(int id)
        {
            return new PatientModel().Get(id);
        }

        // POST: api/Patient
        [HttpPost]
        public ApiResponse Post([FromBody] PatientModel patient)
        {
            return patient.Insert();
        }

        // PUT: api/Patient/5
        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] PatientModel patient)
        {
            return patient.Update(id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ApiResponse Delete(int id)
        {
            return new PatientModel().Delete(id);
        }
    }
}
