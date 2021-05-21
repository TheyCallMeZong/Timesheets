using System.Collections.Generic;
using Timesheets.Models;
using Timesheets.Models.Dto;

namespace Timesheets.Domain.Interfaces
{
    public interface IPersonManager
    {
        Person GetPersonById(int id);
        List<Person> GetPersonsByName(string name);
        List<Person> TakePersons(int skip, int take);
        void CreatePerson(PersonDto item);
        void UpdatePerson(Person item);
        void DeletePerson(int id);
    }
}