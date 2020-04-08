﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTest.Models;

namespace ApiTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PeopleContext _context;

        public PeopleController(PeopleContext context)
        {
            _context = context;
        }

        // GET: api/People
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<People>>> GetPeoples()
        //{
        //    return await _context.Peoples.ToListAsync();
        //}

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<People>> GetPeople(int id)
        {
            var people = await _context.Peoples.FindAsync(id);

            if (people == null)
            {
                return NotFound();
            }

            return people;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPeople(int id, People people)
        {
            if (id != people.Id)
            {
                return BadRequest();
            }

            _context.Entry(people).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PeopleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<People>> PostPeople(People people)
        {
            var peoples = new People()
            {
                Name = people.Name,
                Surname = people.Surname,
                Age = people.Age,
                Relation = people.Relation
            };
            _context.Peoples.Add(peoples);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetPeople", new { id = people.Id }, people);
            return CreatedAtAction(nameof(GetPeople), new { id = people.Id }, people);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<People>> DeletePeople(int id)
        {
            var people = await _context.Peoples.FindAsync(id);
            if (people == null)
            {
                return NotFound();
            }

            _context.Peoples.Remove(people);
            await _context.SaveChangesAsync();

            return people;
        }

        private bool PeopleExists(int id)
        {
            return _context.Peoples.Any(e => e.Id == id);
        }
    }
}
