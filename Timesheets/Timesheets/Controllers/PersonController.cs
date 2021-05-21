using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Timesheets.Domain.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Controllers
{
    [ApiController]
    [Route("persons")]
    public class PersonController : ControllerBase
    {
        private IPersonManager _personManager;

        public PersonController(IPersonManager personManager)
        {
            _personManager = personManager;
        }
        [HttpGet("person/get/{id:int}")]
        public IActionResult GetPersonById([FromRoute] int id)
        {
            var person = _personManager.GetPersonById(id);
            if (person == null)
            {
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
            var result = _personManager.GetPersonsByName(term);
            if (result == null)
            {
                throw new ArgumentNullException("Person not found");
            }
            PersonDto personDto = new PersonDto();
            foreach (var person in result)
            {
                personDto.FirstName = person.FirstName;
                personDto.LastName = person.LastName;
                personDto.Email = person.Email;
                personDto.Company = person.Company;
                personDto.Age = person.Age;
            }
            return Ok(personDto);
        }

        [HttpGet("skip={skip}&take={take}")]
        public IActionResult TakePersons([FromRoute] int skip, [FromRoute] int take)
        {
            var result = _personManager.TakePersons(skip, take);
            if (result == null)
            {
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
        
        [HttpPost]
        public IActionResult Create([FromBody] PersonDto person)
        {
            _personManager.CreatePerson(person);
            return Ok();
        }
        
        [HttpPut]
        public IActionResult Update([FromBody] Person person)
        {
            _personManager.UpdatePerson(person);
            return Ok();
        }
        
        [HttpDelete("id/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _personManager.DeletePerson(id);
            return Ok();
        }
    }
}