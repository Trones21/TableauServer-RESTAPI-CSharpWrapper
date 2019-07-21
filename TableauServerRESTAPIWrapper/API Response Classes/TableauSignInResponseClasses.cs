using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignInResponseClasses
{

    public class AuthUser
    {
        public string id { get; set; }
    }

    public class Credentials
    {
        public AuthUser user { get; set; }
        public string token { get; set; }
    }

    public class RootObject
    {
        public Credentials credentials { get; set; }
    }
}
