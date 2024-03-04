using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
}
