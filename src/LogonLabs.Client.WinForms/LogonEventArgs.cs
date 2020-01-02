using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogonLabs.Client.WinForms
{
    public class LogonEventArgs : EventArgs
    {
        public string ErrorCode { get; set; }
        /// <summary>
        /// Payload from the destination URI. The Payload is decoded from base 64 and is ready to parse (i.e. it's just JSON). If Payload
        /// is not null then Token will be null.
        /// </summary>
        public string Payload { get; set; }

        /// <summary>
        /// Token from LogonLabs. If UseDestinationMode is false in the control then this will be set instead of Payload. It is then the client's
        /// responsibility to pass the token to the backend to be validated.
        /// </summary>
        public string Token { get; set; }
    public string ErrorText { get; internal set; }
  }
}
