using ContactManager.Common;
using ContactManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while(true)
            {
                Console.WriteLine("-----Contact Manager-----");
                Console.WriteLine("1. Create Contact");
                Console.WriteLine("2. Get All Contacts");
                Console.WriteLine("3. Get Contact by ID");
                Console.WriteLine("4. Edit Contact");
                Console.WriteLine("5. Delete Contact");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice [1-6]: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        try
                        {
                            CreateContact();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception caught: {e.Message}");
                        }
                        break;
                    case 2:
                        try
                        {
                            GetAllContacts();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception caught: {e.Message}");
                        }
                        break;
                    case 3:
                        try
                        {
                            GetContactbyId();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception caught: {e.Message}");
                        }
                        break;
                    case 4:
                        try
                        {
                            EditContact();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception caught: {e.Message}");
                        }
                        break;
                    case 5:
                        try
                        {
                            DeleteContact();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine($"Exception caught: {e.Message}");
                        }
                        break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Invalid Option"); break;
                }
            }
        }
        public static void CreateContact()
        {
            Contact c = new Contact();
            Console.Write("Contact ID: ");
            c.ContactID = int.Parse(Console.ReadLine());
            Console.Write("Contact Name: ");
            c.Name = Console.ReadLine();
            Console.Write("Mobile Number: ");
            c.Mobile = Console.ReadLine();
            Console.Write("Email: ");
            c.Email = Console.ReadLine();
            Console.Write("Location: ");
            c.Location = Console.ReadLine();

            IContactsRepository repository = new ContactsFileRepository(); //wrong //DIP //upper layer should never know concrete classes, they should only talk to interfaces
            repository.Save(c);
            Console.WriteLine("Contact Saved Successfully");
        }
        public static void GetAllContacts()
        {
            IContactsRepository repository = new ContactsFileRepository();
            List<Contact> contactlist = new List<Contact>();
            contactlist = repository.GetAllContacts();
            Console.WriteLine("All the available contacts: ");
            foreach (Contact contact in contactlist)
                Console.WriteLine($"{contact.ContactID},{contact.Name},{contact.Mobile},{contact.Email},{contact.Location}");
        }
        public static void GetContactbyId()
        {
            IContactsRepository repository = new ContactsFileRepository();
            Console.Write("Enter the ID to get the contact: ");
            int id = int.Parse(Console.ReadLine());
            Contact contact = repository.GetContactbyId(id);
            Console.WriteLine("The contact is: ");
            Console.WriteLine($"{contact.ContactID},{contact.Name},{contact.Mobile},{contact.Email},{contact.Location}");
        }
        public static void EditContact()
        {
            Console.Write("Enter the ID of contact to be edited: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the contact details: ");
            Contact c = new Contact();
            Console.Write("Contact ID: ");
            c.ContactID = int.Parse(Console.ReadLine());
            Console.Write("Contact Name: ");
            c.Name = Console.ReadLine();
            Console.Write("Mobile Number: ");
            c.Mobile = Console.ReadLine();
            Console.Write("Email: ");
            c.Email = Console.ReadLine();
            Console.Write("Location: ");
            c.Location = Console.ReadLine();

            IContactsRepository repository = new ContactsFileRepository();
            repository.UpdateContact(c, id);
            Console.WriteLine("Contact edited succesfully");
        }
        public static void DeleteContact()
        {
            IContactsRepository repository = new ContactsFileRepository();
            Console.Write("Enter the ID to get the contact: ");
            int id = int.Parse(Console.ReadLine());
            repository.DeleteContactbyId(id);
            Console.WriteLine("Contact deleted succesfully");
        }
    }
}
