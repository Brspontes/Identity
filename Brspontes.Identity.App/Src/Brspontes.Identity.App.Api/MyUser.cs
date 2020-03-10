using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brspontes.Identity.App.Api
{
    public class MyUser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormilezedUserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
