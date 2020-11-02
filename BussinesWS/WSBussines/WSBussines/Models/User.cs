using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSBussines.Models
{
    public class User
    {
        public string id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string usertype { get; set; }

        public User(){}
    }
}