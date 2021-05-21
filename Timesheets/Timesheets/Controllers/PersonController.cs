using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Timesheets.Data.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonController : ControllerBase
    {
        private ILogger<PersonController> _logger;
        private IPersonManager _personManager;

        public PersonController(IPersonManager personManager, ILogger<PersonController> logger)
        {
            _personManager = personManager;
            _logger = logger;
        }
        [HttpGet("person/get/id/{id:int}")]
        public IActionResult GetPersonById([FromRoute] int id)
        {
            _logger.LogInformation("Search person by id");
            var person = _personManager.GetPersonById(id);
            if (person == null)
            {
                _logger.LogError("Person not found");
                throw new ArgumentNullException("Person not found");
            }
            PersonDto personDto = new PersonDto()
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
                Email = person.Email,
                Company = person.Email,
                Age = person.Age
            };
            return Ok(personDto);
        }
        [HttpGet("searchTerm={term}")]
        public IActionResult GetPersonByName([FromRoute] string term)
        {
            _logger.LogInformation("Search person by name");
            var result = _personManager.GetPersonsByName(term);
            if (result == null)
            {
                _logger.LogError("Persons not found");
                throw new ArgumentNullException("Person not found");
            }
            List<PersonDto> personDto = new List<PersonDto>();
            foreach (var person in result)
            {
                personDto.Add(new PersonDto()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    Company = person.Company,
                    Age = person.Age
                });
            }
            return Ok(personDto);
        }

        [HttpGet("skip={skip}&take={take}")]
        public IActionResult TakePersons([FromRoute] int skip, [FromRoute] int take)
        {
            _logger.LogInformation("Take persons");
            var result = _personManager.TakePersons(skip, take);
            if (result == null)
            {
                _logger.LogError("Error");
                throw new ArgumentNullException();
            }
            List<PersonDto> personDto = new List<PersonDto>();
            foreach (var person in result)
            {
                personDto.Add(new PersonDto()
                {
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    Company = person.Company,
                    Age = person.Age
                });
            }
            return Ok(personDto);
        }
        
        [HttpPost("person/create")]
        public IActionResult Create([FromBody] Person person)
        {
            _logger.LogInformation("Create new person");
            _personManager.CreatePerson(person);
            return Ok();
        }
        
        [HttpPut("person/update")]
        public IActionResult Update([FromBody] Person person)
        {
            _logger.LogInformation("Update person");
            _personManager.UpdatePerson(person);
            return Ok();
        }
        
        [HttpDelete("person/delete/id/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _logger.LogInformation("Delete person");
            _personManager.DeletePerson(id);
            return Ok();
        }
    }
}