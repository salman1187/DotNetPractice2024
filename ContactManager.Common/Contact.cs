using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Common
{
    public class Contact
    {
        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }    
        public string Location { get; set; }
    }
    public class NoExistingIDException : ApplicationException
    {
        public NoExistingIDException(string msg) : base(msg) { }
    }
    public class NoDataException : ApplicationException
    {
        public NoDataException(string msg) : base(msg) { }
    }
}
