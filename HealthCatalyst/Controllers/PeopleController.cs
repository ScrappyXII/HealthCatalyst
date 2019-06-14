using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Manza.HealthCatalyst.Models;

namespace Manza.HealthCatalyst.Controllers
{
    // Handle /People API
    [Route("[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleContext _context;

        public PeopleController(PeopleContext context)
        {
            _context = context;
        }

        // Get All People
        [HttpGet]
        public IEnumerable<Person> GetPeople()
        {
            return _context.People;
        }
        
        // Get results of search on People based on input criteria
        [HttpGet("{search}", Name = "Search")]
        public async Task<IActionResult> GetPeopleByName([FromRoute] string search)
        {
            //Thread.Sleep(5000);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // look for any Person whose first name or last name contains the search string
            //var people = await _context.People.Where(x => x.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
            //                                              x.LastName.Contains(search, StringComparison.OrdinalIgnoreCase))
            //                                              .ToListAsync();

            var people = await _context.People.Where(x => x.FullName.Contains(search, StringComparison.OrdinalIgnoreCase))
                                                          .ToListAsync();

            if (people == null)
            {
                // something went wrong
                return BadRequest();
            } 
            else if (people.Count == 0)
            {
                // no results found
                return NotFound(people);
            }
            else
            {
                // matches were found
                return Ok(people);
            }
            
        }
    }
}