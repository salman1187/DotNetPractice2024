using ContactManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public class ContactsDBRepository : IContactsRepository
    {
        private ContactsDbContext db = new ContactsDbContext();
        public void DeleteContactbyId(int id)
        {
            Contact c = db.Contacts.Find(id);
            db.Contacts.Remove(c);
            db.SaveChanges();
        }

        public List<Contact> GetAllContacts()
        {
            var contacts = (from c in db.Contacts
                            select c).ToList();
            return contacts;
        }

        public Contact GetContactbyId(int id)
        {
            Contact c = db.Contacts.Find(id);
            return c;
        }

        public void Save(Contact contact)
        {
            db.Contacts.Add(contact);
            db.SaveChanges();
        }

        public void UpdateContact(Contact contact, int ContactId)
        {
            Contact c = db.Contacts.Find(ContactId);
            c.ContactID = contact.ContactID;
            c.Name = contact.Name;
            c.Mobile = contact.Mobile;
            c.Email = contact.Email;
            c.Location = contact.Location;

            db.SaveChanges();
        }
    }
}
