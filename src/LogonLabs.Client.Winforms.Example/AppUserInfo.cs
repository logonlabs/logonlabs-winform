using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogonLabs.Client.Winforms.Example
{
    /// <summary>
    /// This is an example of the information that may be contained in the payload received from callback server
    /// when navigation is redirected to the destination URI (final step).
    /// </summary>
    public class AppUserInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Provider { get; set; }
        public string Email { get; set; }
    }
}
