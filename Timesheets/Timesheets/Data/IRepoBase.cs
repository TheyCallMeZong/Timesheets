using System.Collections.Generic;

namespace Timesheets.Data
{
    public interface IRepoBase<T>
    {
        T GetPersonById(int id);
        List<T> GetPersonsByName(string name);
        List<T> TakePersons(int skip, int take);
        void CreatePerson(T item);
        void UpdatePerson(T item);
        void DeletePerson(int id);
    }
}