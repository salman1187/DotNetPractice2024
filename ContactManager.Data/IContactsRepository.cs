using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.AccessControl;
using System.Configuration;
using System.Threading.Tasks;
using ContactManager.Common;

namespace ContactManager.Data
{
    public interface IContactsRepository
    {
        void Save(Contact contact);
        List<Contact> GetAllContacts();
        Contact GetContactbyId(int id);
        void DeleteContactbyId(int id);
        void UpdateContact(Contact contact, int ContactId);
    }
    public class RepositoryFactory
    {
        private RepositoryFactory() { }
        public static readonly RepositoryFactory Instance = new RepositoryFactory();
        public IContactsRepository CreateRepository()
        {
            string className = ConfigurationManager.AppSettings["Repo"];
        Type theType = Type.GetType(className);
            return (IContactsRepository) Activator.CreateInstance(theType);
    }
    }
}
