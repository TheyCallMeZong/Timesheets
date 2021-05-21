using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.SignalR.Protocol;
using Timesheets.Data.Interfaces;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Data.Implementations
{
    public class PersonManager : IPersonManager
    {
        private PersonsDb _persons;

        public PersonManager()
        {
            _persons = new PersonsDb();
        }
        public Person GetPersonById(int id)
        {
            Person result = _persons.Persons.FirstOrDefault(person => person.Id == id);
            return result;
        }

        public List<Person> GetPersonsByName(string name)
        {
            var result = _persons.Persons.Where(person => person.FirstName == name).ToList();
            return result;
        }

        public List<Person> TakePersons(int skip, int take)
        {
            var result = _persons.Persons.Skip(skip).Take(take).ToList();
            return result;
        }

        public void CreatePerson(Person item)
        {
            var newPerson = new Person()
            {
                Id = item.Id,
                FirstName = item.FirstName,
                LastName = item.LastName,
                Email = item.Email,
                Company = item.Company,
                Age = item.Age
            };
            _persons.Persons.Add(newPerson);
        }

        public void UpdatePerson(Person item)
        {
            var updatePerson = _persons.Persons.FirstOrDefault(person => person.Id == item.Id);
            if (updatePerson != null)
            {
                updatePerson.FirstName = item.FirstName;
                updatePerson.LastName = item.LastName;
                updatePerson.Email = item.Email;
                updatePerson.Company = item.Company;
                updatePerson.Age = item.Age;
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        public void DeletePerson(int id)
        {
            _persons.Persons.RemoveAll(person => person.Id == id);
        }
    }
}