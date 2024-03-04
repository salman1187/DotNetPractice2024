using ContactManager.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Data
{
    public class ContactsFileRepository : IContactsRepository
    {
        private readonly string file = "contacts.txt";
        public void DeleteContactbyId(int id)
        {
            bool idExist = false;
            StreamReader reader = new StreamReader(file);
            string[] contactlist = reader.ReadToEnd().Split('\n');
            reader.Close();

            if (contactlist.Length == 0)
                throw new NoDataException("The file is empty");

            using (StreamWriter writer = new StreamWriter(file))
            {
                if (!contactlist[0].StartsWith($"{id},"))
                    writer.WriteLine(contactlist[0]);
                else idExist = true;
            }
            using (StreamWriter writer = new StreamWriter(file, true))
            {
                for (int i = 1; i < contactlist.Length - 1; i++)
                {
                    if (!contactlist[i].StartsWith($"{id},"))
                        writer.WriteLine(contactlist[i]);
                    else idExist = true;
                }
            }
            if (idExist == false)
                throw new NoExistingIDException("No record with that ID exists");
        }

        public List<Contact> GetAllContacts()
        {
            List<Contact> allcontacts = new List<Contact>();
            using (StreamReader reader = new StreamReader(file))
            {
                string[] contactlist = reader.ReadToEnd().Split('\n');
                if (contactlist.Length == 0)
                    throw new NoDataException("The file is empty");
                for (int i = 0; i < contactlist.Length - 1; i++)
                {
                    string[] parts = contactlist[i].Split(',');
                    Contact temp = new Contact();
                    temp.ContactID = int.Parse(parts[0]);
                    temp.Name = parts[1];
                    temp.Mobile = parts[2];
                    temp.Email = parts[3];
                    temp.Location = parts[4];
                    allcontacts.Add(temp);
                }
            }
            return allcontacts;
        }

        public Contact GetContactbyId(int id)
        {
            bool idExist = false;
            Contact temp = new Contact();
            using (StreamReader reader = new StreamReader(file))
            {
                string[] contactlist = reader.ReadToEnd().Split('\n');
                if (contactlist.Length == 0)
                    throw new NoDataException("The file is empty");
                for (int i = 0; i < contactlist.Length - 1; i++)
                {
                    if (contactlist[i].StartsWith($"{id},"))
                    {
                        idExist = true;
                        string[] parts = contactlist[i].Split(',');
                        temp.ContactID = int.Parse(parts[0]);
                        temp.Name = parts[1];
                        temp.Mobile = parts[2];
                        temp.Email = parts[3];
                        temp.Location = parts[4];
                        break;
                    }
                }
            }
            if (idExist == false)
                throw new NoExistingIDException("No record with that ID exists");
            return temp;
        }

        public void Save(Contact contact)
        {
            using(StreamWriter writer = new StreamWriter(file, true))
            {
                string contactCsv = $"{contact.ContactID},{contact.Name},{contact.Mobile},{contact.Email},{contact.Location}";
                writer.WriteLine(contactCsv);
            }
        }

        public void UpdateContact(Contact contact, int ContactId)
        {
            bool idExist = false;
            StreamReader reader = new StreamReader(file);
            string[] contactlist = reader.ReadToEnd().Split('\n');
            reader.Close();
            if (contactlist.Length == 0)
                throw new NoDataException("The file is empty");

            for (int i = 0; i < contactlist.Length - 1; i++)
            {
                if (contactlist[i].StartsWith($"{ContactId},"))
                {
                    contactlist[i] = $"{contact.ContactID},{contact.Name},{contact.Mobile},{contact.Email},{contact.Location}";
                    idExist = true;
                }
            }
            using (StreamWriter writer = new StreamWriter(file))
            {
                    writer.WriteLine(contactlist[0]);
            }
            using (StreamWriter writer = new StreamWriter(file, true))
            {
                for (int i = 1; i < contactlist.Length - 1; i++)
                    writer.WriteLine(contactlist[i]);
            }
            if (idExist == false)
                throw new NoExistingIDException("No record with that ID exists");
        }
    }
}
