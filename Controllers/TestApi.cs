using FBWeb.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FBWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestApi : ControllerBase
    {
        FlutterbookContext _context;
        public TestApi(FlutterbookContext dbcontext)
        {
            _context = dbcontext;
        }


        // GET: api/<TestApi>
        [HttpGet]
        public IActionResult Get()
        {

            return Ok(_context.User.Include(x=>x.EventTable).Include(x=>x.ContactTable).Include(x=>x.NoteTable).Include(x=>x.TaskTable).ToList());
        }

        // GET api/<TestApi>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TestApi>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TestApi>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TestApi>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
